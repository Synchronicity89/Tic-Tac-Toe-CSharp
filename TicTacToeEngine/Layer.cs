using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeEngine
{
    // Represents a layer of the neural network
    public class Layer
    {
        // The size of the layer
        private int size;

        // The neurons of the layer
        public Neuron[] neurons;

        // The weights of the layer
        public double[,] weights;

        // The biases of the layer
        public double[] biases;

        // The inputs of the layer
        public double[] inputs;

        // The outputs of the layer
        public double[] outputs;

        // The errors of the layer
        public double[] errors;

        // The activation function of the layer
        public Func<double, double> activationFunction;

        // The derivative function of the layer
        public Func<double, double> derivativeFunction;

        public int Size { get => size; set => size = value; }

        // Creates a new layer with the given size and activation function
        public Layer(int size, Func<double, double> activationFunction, Func<double, double> derivativeFunction)
        {
            // Set the size, activation function, and derivative function
            this.Size = size;
            this.activationFunction = activationFunction;
            this.derivativeFunction = derivativeFunction;

            // Create the neurons array with the given size
            neurons = new Neuron[size];

            // Loop through the neurons array
            for (int i = 0; i < size; i++)
            {
                // Create a new neuron with the activation function and derivative function
                neurons[i] = new Neuron(activationFunction, derivativeFunction);
            }
            weights = new double[size, size];
            biases = new double[size];
            inputs = new double[size];
            outputs = new double[size];
            errors = new double[size];
        }

        // Sets the weights of the layer from the previous layer
        public void SetWeights(double[,] weights)
        {
            // Validate the weights matrix dimensions
            if (weights.GetLength(0) != Size || weights.GetLength(1) != inputs.Length)
            {
                throw new ArgumentException("Invalid weights matrix");
            }

            // Set the weights matrix
            this.weights = weights;
        }

        // Sets the bias of the layer
        public void SetBiases(double[] biases)
        {
            // Validate the biases array length
            if (biases.Length != Size)
            {
                throw new ArgumentException("Invalid biases array");
            }

            // Set the biases array
            this.biases = biases;
        }

        // Sets the inputs of the layer
        public void SetInputs(double[] inputs)
        {
            // Set the inputs array
            this.inputs = inputs;
        }

        // Sets the outputs of the layer
        public void SetOutputs(double[] outputs)
        {
            // Set the outputs array
            this.outputs = outputs;
        }

        // Sets the errors of the layer
        public void SetErrors(double[] errors)
        {
            // Set the errors array
            this.errors = errors;
        }

        // Gets the size of the layer
        public int GetSize()
        {
            return Size;
        }

        // Gets the neurons of the layer
        public Neuron[] GetNeurons()
        {
            return neurons;
        }

        // Gets the weights of the layer
        public double[,] GetWeights()
        {
            return weights;
        }

        // Gets the bias of the layer
        public double[] GetBiases()
        {
            return biases;
        }

        // Gets the inputs of the layer
        public double[] GetInputs()
        {
            return inputs;
        }

        // Gets the outputs of the layer
        public double[] GetOutputs()
        {
            return outputs;
        }

        // Gets the errors of the layer
        public double[] GetErrors()
        {
            return errors;
        }

        // Gets the activation function of the layer
        public Func<double, double> GetActivationFunction()
        {
            return activationFunction;
        }

        // Gets the derivative function of the layer
        public Func<double, double> GetDerivativeFunction()
        {
            return derivativeFunction;
        }

        // Calculates the values of the layer by multiplying the inputs with the weights and adding the biases
        public void CalculateValues(Layer previousLayer)
        {
            // Get the inputs from the previous layer outputs
            inputs = previousLayer.GetOutputs();

            // Create an array to store the outputs
            outputs = new double[Size];

            // Loop through the neurons
            for (int i = 0; i < Size; i++)
            {
                // Get the current neuron
                Neuron neuron = neurons[i];

                // Set the neuron input to zero
                neuron.SetInput(0);

                // Loop through the inputs
                for (int j = 0; j < inputs.Length; j++)
                {
                    // Add the product of the input and the weight to the neuron input
                    neuron.AddInput(inputs[j] * weights[i, j]);
                }

                // Add the bias to the neuron input
                neuron.AddInput(biases[i]);

                // Calculate the neuron output by applying the activation function
                neuron.CalculateOutput();

                // Set the output array value to the neuron output
                outputs[i] = neuron.GetOutput();
            }
        }

        // Calculates the errors of the layer by multiplying the next layer errors with the weights and applying the derivative function
        public void CalculateErrors(Layer nextLayer, double[] predictedOuput = null)
        {
            if(predictedOuput != null)
            {
                // Loop through the neurons
                for (int i = 0; i < Size; i++)
                {
                    // Get the current neuron
                    Neuron neuron = neurons[i];

                    // Set the neuron error to the difference between the predicted output and the actual output
                    neuron.SetError(predictedOuput[i] - neuron.GetOutput());

                    // Multiply the neuron error by the derivative function
                    neuron.ApplyDerivative();

                    // Set the error array value to the neuron error
                    errors[i] = neuron.GetError();
                }

                return;
            }

            // Get the next layer errors
            double[] nextErrors = nextLayer.GetErrors();

            // Get the next layer weights
            double[,] nextWeights = nextLayer.GetWeights();

            // Create an array to store the errors
            errors = new double[Size];

            // Loop through the neurons
            for (int i = 0; i < Size; i++)
            {
                // Get the current neuron
                Neuron neuron = neurons[i];

                // Set the neuron error to zero
                neuron.SetError(0);

                // Loop through the next layer errors
                for (int j = 0; j < nextErrors.Length; j++)
                {
                    // Add the product of the next layer error and the weight to the neuron error
                    neuron.AddError(nextErrors[j] * nextWeights[j, i]);
                }

                // Multiply the neuron error by the derivative function
                neuron.ApplyDerivative();

                // Set the error array value to the neuron error
                errors[i] = neuron.GetError();
            }
        }

        // Updates the weights and biases of the layer by multiplying the errors with the previous layer values and subtracting the product from the learning rate
        public void UpdateWeightsAndBiases(Layer previousLayer, double learningRate)
        {
            // Get the previous layer outputs
            double[] previousOutputs = previousLayer.GetOutputs();

            // Loop through the neurons
            for (int i = 0; i < Size; i++)
            {
                // Get the current neuron
                Neuron neuron = neurons[i];

                // Get the neuron error
                double error = neuron.GetError();

                // Loop through the weights
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    // Update the weight by subtracting the product of the error, the previous layer output, and the learning rate
                    weights[i, j] -= error * previousOutputs[j] * learningRate;
                }

                // Update the bias by subtracting the product of the error and the learning rate
                biases[i] -= error * learningRate;
            }
        }

        public void SetWeight(int i, int j, double v)
        {
            weights[i, j] = v;
        }

        public void SetBias(int i, double v)
        {
            biases[i] = v;
        }

        public void SetValues(double[] inputArray)
        {
            for (int i = 0; i < Size; i++)
            {
                neurons[i].SetValue(inputArray[i]);
            }
        }

        public double[] GetValues()
        {
            double[] values = new double[Size];

            for (int i = 0; i < Size; i++)
            {
                values[i] = neurons[i].GetValue();
            }

            return values;
        }
    }

}
