using NUnit.Framework;

namespace NeuralNet.Tests
{
    [TestFixture]
    public class NeuralNetworkTests
    {
        [Test]
        public void FeedForward_ShouldReturnCorrectOneOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 1);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };

            // Act
            double[] output = neuralNetwork.FeedForward(input);

            // Assert
            Assert.AreEqual(1, output.Length);
            Assert.IsTrue(output[0] >= 0 && output[0] <= 1);
        }

        [Test]
        public void FeedForward_ShouldReturnCorrectTwoOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 2);
            neuralNetwork.InitializeWeights(222);

            double[] input = { 0.5, 0.8 };

            // Act
            double[] output = neuralNetwork.FeedForward(input);

            // Assert
            Assert.AreEqual(2, output.Length);
            Assert.IsTrue(output[0] >= 0 && output[0] <= 1);
            Assert.IsTrue(output[1] >= 0 && output[1] <= 1);
        }

        [Test]
        public void Train_ShouldUpdateWeightsAndBiasesOneOutput()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 1);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };
            double[] output = { 0.9 };

            // Act
            neuralNetwork.Train(100, null);

            // Assert
            double[] predictedOutput = neuralNetwork.FeedForward(input);
            Assert.AreEqual(1, predictedOutput.Length);
            Assert.IsTrue(predictedOutput[0] >= 0 && predictedOutput[0] <= 1);
            Assert.AreNotEqual(output[0], predictedOutput[0]);

            // Assert that both outputs are within 0.1 of the expected output using absolute value
            Assert.IsTrue(Math.Abs(output[0] - predictedOutput[0]) < 0.1);
        }        
        
        [Test]
        public void Train_ShouldUpdateWeightsAndBiasesTwoOutputs()
        {
            // Arrange
            NeuralNetwork neuralNetwork = new NeuralNetwork(2, 2, 2);
            neuralNetwork.InitializeWeights(123);

            double[] input = { 0.5, 0.8 };
            double[] output = { 0.9, 0.3 };

            // Act
            neuralNetwork.Train(100, null);

            // Assert
            double[] predictedOutput = neuralNetwork.FeedForward(input);
            Assert.AreEqual(2, predictedOutput.Length);
            Assert.IsTrue(predictedOutput[0] >= 0 && predictedOutput[0] <= 1);
            Assert.IsTrue(predictedOutput[1] >= 0 && predictedOutput[1] <= 1);
            Assert.AreNotEqual(output[0], predictedOutput[0]);
            Assert.AreNotEqual(output[1], predictedOutput[1]);

            // Assert that both outputs are within 0.1 of the expected output using absolute value
            Assert.IsTrue(Math.Abs(output[0] - predictedOutput[0]) < 0.1);
            Assert.IsTrue(Math.Abs(output[1] - predictedOutput[1]) < 0.1);
        }
    }
}