using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    // Represents a neuron of the neural network
    public class Neuron
    {
        // The value of the neuron
        double value;

        // The input of the neuron
        double input;

        // The error of the neuron
        double error;

        // The activation function of the neuron
        Func<double, double> activationFunction;

        // The derivative function of the neuron
        Func<double, double> derivativeFunction;

        // Creates a new neuron with the given activation function and derivative function
        public Neuron(Func<double, double> activationFunction, Func<double, double> derivativeFunction)
        {
            // Set the activation function and derivative function
            this.activationFunction = activationFunction;
            this.derivativeFunction = derivativeFunction;
        }

        // Sets the value of the neuron
        public void SetValue(double value)
        {
            this.value = value;
        }

        // Sets the input of the neuron
        public void SetInput(double input)
        {
            this.input = input;
        }

        // Sets the error of the neuron
        public void SetError(double error)
        {
            this.error = error;
        }

        // Adds a value to the input of the neuron
        public void AddInput(double value)
        {
            input += value;
        }

        // Adds a value to the error of the neuron
        public void AddError(double value)
        {
            error += value;
        }

        // Calculates the output of the neuron by applying the activation function to the input
        public void CalculateOutput()
        {
            value = activationFunction(input);
        }

        // Multiplies the error of the neuron by the derivative function of the input
        public void ApplyDerivative()
        {
            error *= derivativeFunction(value);
        }

        // Gets the value of the neuron
        public double GetValue()
        {
            return value;
        }

        // Gets the input of the neuron
        public double GetInput()
        {
            return input;
        }

        // Gets the error of the neuron
        public double GetError()
        {
            return error;
        }

        // Gets the activation function of the neuron
        public Func<double, double> GetActivationFunction()
        {
            return activationFunction;
        }

        // Gets the derivative function of the neuron
        public Func<double, double> GetDerivativeFunction()
        {
            return derivativeFunction;
        }

        public double GetOutput()
        {
            return value;
        }
    }
}
