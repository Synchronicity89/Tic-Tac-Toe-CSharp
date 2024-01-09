namespace NeuralNet
{
    public class NeuralNetwork
    {
        // The input layer
        public Layer inputLayer;

        // The hidden layers
        public List<Layer> hiddenLayers;

        // The output layer
        public Layer outputLayer;

        // The learning rate
        public double learningRate;

        public static Func<double, double> Linear = (x) => x;
        public static Func<double, double> Sigmoid = (x) => 1.0 / (1.0 + Math.Exp(-x));
        public static Func<double, double> LinearDerivative = (x) => 1.0;
        public static Func<double, double> SigmoidDerivative = (x) =>
        {
            var sigX = Sigmoid(x);
            return sigX * (1.0 - sigX);
        };

        // Creates a new neural network model with the given number of input, hidden, and output neurons
        public NeuralNetwork(int inputSize, int[] hiddenSizes, int outputSize, double? learnRate = 0.1)
        {
            // Create the input layer with the given size and a linear activation function
            inputLayer = new Layer(inputSize, inputSize, Linear, LinearDerivative);

            // Create the hidden layers with the given sizes and a sigmoid activation function
            hiddenLayers = new List<Layer>();
            for (int i = 0; i < hiddenSizes.Length; i++)
            {
                hiddenLayers.Add(new Layer(hiddenSizes[i], i == 0 ? inputSize : hiddenSizes[i - 1], Sigmoid, SigmoidDerivative));
            }

            // Create the output layer with the given size and a sigmoid activation function
            outputLayer = new Layer(outputSize, hiddenSizes.Length > 0 ? hiddenSizes[hiddenSizes.Length - 1] : inputSize, Sigmoid, SigmoidDerivative);

            // Set the learning rate to 0.1 or learnRate if it is not null
            learningRate = learnRate ?? 0.1;
        }

        // Initializes the weights of the neural network with random values from a normal distribution
        public void InitializeWeights(int seed)
        {
            // Create a random number generator with the given seed
            Random random = new Random(seed);

            // Loop through the hidden layers
            for (int layerIndex = 0; layerIndex < hiddenLayers.Count; layerIndex++)
            {
                var currentLayer = hiddenLayers[layerIndex];

                // Loop through the current hidden layer neurons
                for (int i = 0; i < currentLayer.Size; i++)
                {
                    // Loop through the previous layer neurons
                    for (int j = 0; j < (layerIndex == 0 ? inputLayer.Size : hiddenLayers[layerIndex - 1].Size); j++)
                    {
                        // Set the weight from the previous neuron to the current neuron to a random value
                        currentLayer.SetWeight(i, j, random.NextDouble());
                    }

                    // Set the bias of the current neuron to a random value
                    currentLayer.SetBias(i, random.NextDouble());
                }
            }

            // Loop through the output layer neurons
            for (int i = 0; i < outputLayer.Size; i++)
            {
                // Loop through the last hidden layer neurons
                for (int j = 0; j < (hiddenLayers.Count > 0 ? hiddenLayers[hiddenLayers.Count - 1].Size : inputLayer.Size); j++)
                {
                    // Set the weight from the last hidden neuron to the output neuron to a random value
                    outputLayer.SetWeight(i, j, random.NextDouble());
                }

                // Set the bias of the output neuron to a random value
                outputLayer.SetBias(i, random.NextDouble());
            }
        }

        // Feeds the input data forward through the neural network and returns the output
        public double[] FeedForward(double[] inputArray)
        {
            // Set the input layer values to the input array values
            inputLayer.SetValues(inputArray);

            // Calculate the hidden layer values by multiplying the input layer values with the weights and adding the biases
            for (int i = 0; i < hiddenLayers.Count; i++)
            {
                hiddenLayers[i].CalculateValues(i == 0 ? inputLayer : hiddenLayers[i - 1]);
            }

            // Calculate the output layer values by multiplying the last hidden layer values with the weights and adding the biases
            outputLayer.CalculateValues(hiddenLayers.Count > 0 ? hiddenLayers[hiddenLayers.Count - 1] : inputLayer);

            // Return the output layer values as an array
            return outputLayer.GetValues();
        }

        // Trains the neural network with the given number of epochs using backpropagation and gradient descent
        public void Train(int epochs, Action<double, double> progressCallback)
        {
            // Loop for the given number of epochs
            for (int i = 0; i < epochs; i++)
            {
                // TODO: Implement training logic here

                // Call the progress callback with the current epoch and loss
                progressCallback?.Invoke(i, 0.0);
            }
        }
    }
    // {
    //     // The input layer
    //     public Layer inputLayer;

    //     // The hidden layer
    //     public Layer hiddenLayer;

    //     // The output layer
    //     public Layer outputLayer;

    //     // The learning rate
    //     public double learningRate;

    //     public static Func<double, double> Linear = (x) => x;
    //     public static Func<double, double> Sigmoid = (x) => 1.0 / (1.0 + Math.Exp(-x));
    //     public static Func<double, double> LinearDerivative = (x) => 1.0;
    //     public static Func<double, double> SigmoidDerivative = (x) =>
    //     {
    //         var sigX = Sigmoid(x);
    //         return sigX * (1.0 - sigX);
    //     };

    //     // Creates a new neural network model with the given number of input, hidden, and output neurons
    //     public NeuralNetwork(int inputSize, int hiddenSize, int outputSize, double? learnRate = 0.1)
    //     {
    //         // Create the input layer with the given size and a linear activation function
    //         inputLayer = new Layer(inputSize, inputSize, Linear, LinearDerivative);

    //         // Create the hidden layer with the given size and a sigmoid activation function
    //         hiddenLayer = new Layer(hiddenSize, inputSize, Sigmoid, SigmoidDerivative);

    //         // Create the output layer with the given size and a sigmoid activation function
    //         outputLayer = new Layer(outputSize, hiddenSize, Sigmoid, SigmoidDerivative);

    //         // Set the learning rate to 0.1 or learnRate if it is not null
    //         learningRate = learnRate ?? 0.1;
    //     }

    //     // Initializes the weights of the neural network with random values from a normal distribution
    //     public void InitializeWeights(int seed)
    //     {
    //         // Create a random number generator with the given seed
    //         Random random = new Random(seed);

    //         // Loop through the hidden layer neurons
    //         for (int i = 0; i < hiddenLayer.Size; i++)
    //         {
    //             // Loop through the input layer neurons
    //             for (int j = 0; j < inputLayer.Size; j++)
    //             {
    //                 // Set the weight from the input neuron to the hidden neuron to a random value
    //                 hiddenLayer.SetWeight(i, j, random.NextDouble());
    //             }

    //             // Set the bias of the hidden neuron to a random value
    //             hiddenLayer.SetBias(i, random.NextDouble());
    //         }

    //         // Loop through the output layer neurons
    //         for (int i = 0; i < outputLayer.Size; i++)
    //         {
    //             // Loop through the hidden layer neurons
    //             for (int j = 0; j < hiddenLayer.Size; j++)
    //             {
    //                 // Set the weight from the hidden neuron to the output neuron to a random value
    //                 outputLayer.SetWeight(i, j, random.NextDouble());
    //             }

    //             // Set the bias of the output neuron to a random value
    //             outputLayer.SetBias(i, random.NextDouble());
    //         }

    //     }

    //     // Feeds the input data forward through the neural network and returns the output
    //     public double[] FeedForward(double[] inputArray)
    //     {
    //         // Set the input layer values to the input array values
    //         inputLayer.SetValues(inputArray);

    //         // Calculate the hidden layer values by multiplying the input layer values with the weights and adding the biases
    //         hiddenLayer.CalculateValues(inputLayer);

    //         // Calculate the output layer values by multiplying the hidden layer values with the weights and adding the biases
    //         outputLayer.CalculateValues(hiddenLayer);

    //         // Return the output layer values as an array
    //         return outputLayer.GetValues();

    //     }

    //     // Trains the neural network with the given number of epochs using backpropagation and gradient descent
    //     public void Train(int epochs, Action<double, double> progressCallback)
    //     {
    //         // Loop for the given number of epochs
    //         for (int i = 0; i < epochs; i++)
    //         {
    //             // Generate a random initial state as the input based on the size of the input layer
    //             double[] input = new double[inputLayer.Size];
    //             Random random = new Random();
    //             for (int j = 0; j < inputLayer.Size; j++)
    //             {
    //                 input[j] = random.NextDouble();
    //             }

    //             // Generate the optimal output for the input using the minimax algorithm
    //             //double[] output = GenerateOptimalOutput(input);
    //             double[] output = ScaledOptimalOutput(input);

    //             // Feed the input forward through the network and get the predicted output
    //             double[] predictedOutput = FeedForward(input);

    //             // use the progressCallback to report the error from the difference between the opimal ouput and the predicted output.
    //             // The error is the sum of the squares of the differences between the optimal output and the predicted output, the second parameter is i
    //             // which is the epoch number.
    //             progressCallback?.Invoke(i, output.Zip(predictedOutput, (a, b) => a - b).Sum(x => x * x));

    //             // Calculate the output layer errors by subtracting the predicted output from the optimal output
    //             outputLayer.CalculateErrors(outputLayer, predictedOutput);

    //             // Calculate the hidden layer errors by multiplying the output layer errors with the weights and applying the derivative function
    //             hiddenLayer.CalculateErrors(outputLayer);

    //             // Update the output layer weights and biases by multiplying the output layer errors with the hidden layer values and subtracting the product from the learning rate
    //             outputLayer.UpdateWeightsAndBiases(hiddenLayer, learningRate);

    //             // Update the hidden layer weights and biases by multiplying the hidden layer errors with the input layer values and subtracting the product from the learning rate
    //             hiddenLayer.UpdateWeightsAndBiases(inputLayer, learningRate);
    //         }

    //     }


    //     public double[] ScaledOptimalOutput(double[] input)
    //     {
    //         //double[] output = GenerateOptimalOutput(input);
    //         double[] output = input; //GenerateOptimalOutput(input);
    //         double[] optimalOutput = new double[output.GetLength(0)];
    //         // Compress the output to a range of -1 to 1.  First find the maximum value in the output array, and the minimum value in the output array,
    //         // then the difference between those two, and then divide each value in the output array by that difference.
    //         double max = output.Max();
    //         double min = output.Min();
    //         double difference = max - min;
    //         for (int i = 0; i < output.GetLength(0); i++)
    //         {
    //             optimalOutput[i] = output[i] / difference;
    //         }

    //         return optimalOutput;
    //     }
    // }
}