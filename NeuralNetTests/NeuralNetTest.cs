using NUnit.Framework;

namespace NeuralNet.Tests
{
    [TestFixture]
    public class NeuralNetworkTests
    {

// #### 3. Layers Container
//   - **Functionality**: Manages multiple Layer instances.
//   - **Sub-points**:
//     - **Layer List**: A list of Layer instances.
//       - **Add Layer**: Method to add a new layer.
//       - **Remove Layer**: Method to remove an existing layer.
//     - **Iterative Access**: Ability to iterate through layers.
//       - **Forward Iteration**: Iterate from input to output layer.
//       - **Backward Iteration**: Iterate in reverse for backpropagation.

// #### 6. Input Handling
//   - **Functionality**: Mechanism to input data to the network.
//   - **Sub-points**:
//     - **Input Shape Validation**: Ensure compatibility with the first layer.
//       - **Error Handling**: Provide informative errors on mismatch.
//       - **Flexible Input Handling**: Adapt to varying input data formats.

// #### 7. Output Retrieval
//   - **Functionality**: Mechanism to retrieve output from the last layer.
//   - **Sub-points**:
//     - **Output Access**: Method to access the output of the last layer.
//       - **Format Consistency**: Ensure consistent output format.
//       - **Error Handling**: Handle cases with no output layer defined.

// #### 8. Network Configuration
//   - **Functionality**: Set up and configure the neural network.
//   - **Sub-points**:
//     - **Parameter Settings**: Options to set learning rate, regularization, etc.
//       - **Default Settings**: Provide default configuration values.
//       - **Custom Settings**: Allow users to customize settings.

// #### 9. Error Handling and Validation
//   - **Functionality**: Robust error handling and input validation.
//   - **Sub-points**:
//     - **Input Validation**: Check for valid input data types and formats.
//       - **Layer Compatibility**: Ensure consecutive layers are compatible.
//       - **Exception Handling**: Gracefully handle and report errors.


        [Test]
        public void FeedForward_ShouldReturnCorrectOneOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2,  new []{2,3}, 1);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };

            // Act
            double[] output = neuralNetwork.FeedForward(input);

            // Assert
            Assert.That(output.Length, Is.EqualTo(1));
            Assert.That(output[0] >= 0 && output[0] <= 1, Is.True);
        }

        [Test]
        public void FeedForward_ShouldReturnCorrectTwoOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, new []{2,3}, 2);
            neuralNetwork.InitializeWeights(222);

            double[] input = { 0.5, 0.8 };

            // Act
            double[] output = neuralNetwork.FeedForward(input);

            // Assert
            Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output[0] >= 0 && output[0] <= 1, Is.True);
            Assert.That(output[1] >= 0 && output[1] <= 1, Is.True);
        }

        [Test]
        public void Train_ShouldUpdateWeightsAndBiasesOneOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, new []{2,3}, 1, 0.001);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };
            double[] output = { 0.9 };

            // Act
            neuralNetwork.Train(100000, null);

            // Assert
            double[] predictedOutput = neuralNetwork.FeedForward(input);
            Assert.That(predictedOutput.Length, Is.EqualTo(1));
            Assert.That(predictedOutput[0] >= 0 && predictedOutput[0] <= 1, Is.True);
            Assert.That(predictedOutput[0], Is.Not.EqualTo(output[0]));

            // Assert that both outputs are within 0.1 of the expected output using absolute value
            Assert.That(Math.Abs(output[0] - predictedOutput[0]), Is.LessThan(0.1));
        }        
        
        [Test]
        public void Train_ShouldUpdateWeightsAndBiasesTwoOutputs()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, new []{2,3}, 2);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };
            double[] output = { 0.9, 0.3 };

            // Act
            neuralNetwork.Train(100, null);

            // Assert
            double[] predictedOutput = neuralNetwork.FeedForward(input);
            Assert.That(predictedOutput.Length, Is.EqualTo(2));
            Assert.That(predictedOutput[0], Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(1));
            Assert.That(predictedOutput[1], Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(1));
            Assert.That(predictedOutput[0], Is.Not.EqualTo(output[0]));
            Assert.That(predictedOutput[1], Is.Not.EqualTo(output[1]));

            // Assert that both outputs are within 0.1 of the expected output using absolute value
            Assert.That(Math.Abs(output[0] - predictedOutput[0]), Is.LessThan(0.1));
            Assert.That(Math.Abs(output[1] - predictedOutput[1]), Is.LessThan(0.1));
        }
    }
}