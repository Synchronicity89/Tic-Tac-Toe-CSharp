using NUnit.Framework;
using NeuralNet;

namespace NeuralNet.Tests
{
    [TestFixture]
    public class LayerTests
    {

// #### 2. Layer Class
//   - **Functionality**: Represents a layer of neurons.
//   - **Sub-points**:
//     - **Neurons**: A list of Neuron instances.
//       - **Initialization**: Create neurons based on specified layer size.
//       - **Access Mechanism**: Methods to access and modify neurons.
//     - **Layer Size**: Number of neurons in the layer.
//       - **Initialization**: Set during layer creation.
//       - **Validation**: Ensure non-negative and non-zero size.

        [Test]
        public void CalculateValues_ShouldSetOutputsCorrectly()
        {
            // Arrange
            Layer previousLayer = new Layer(2, 2, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            previousLayer.SetOutputs(new double[] { 0.5, 0.8 });

            Layer layer = new Layer(2, previousLayer.Size, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            layer.SetWeights(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });
            layer.SetBiases(new double[] { 0.5, 0.6 });

            // Act
            layer.CalculateValues(previousLayer);

            // Assert
            double[] expectedOutputs = { 0.68, 0.82 };
            double[] actualOutputs = layer.GetOutputs();
            Assert.That(actualOutputs.Length, Is.EqualTo(expectedOutputs.Length));
            for (int i = 0; i < expectedOutputs.Length; i++)
            {
                Assert.That(actualOutputs[i], Is.EqualTo(expectedOutputs[i]).Within(0.01));
            }
        }

        [Test]
        public void CalculateErrors_ShouldSetErrorsCorrectly()
        {
            // Arrange
            Layer nextLayer = new Layer(2, 2, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            nextLayer.SetErrors(new double[] { 0.1, 0.2 });

            Layer layer = new Layer(2, nextLayer.Size, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            layer.SetWeights(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });

            // Act
            layer.CalculateErrors(nextLayer);

            // Assert
            double[] expectedErrors = { 0.05, 0.11 };
            double[] actualErrors = layer.GetErrors();
            Assert.That(actualErrors.Length, Is.EqualTo(expectedErrors.Length));
            for (int i = 0; i < expectedErrors.Length; i++)
            {
                Assert.That(actualErrors[i], Is.EqualTo(expectedErrors[i]).Within(0.01));
            }
        }

        [Test]
        public void UpdateWeightsAndBiases_ShouldUpdateWeightsAndBiasesCorrectly()
        {
            // Arrange
            Layer previousLayer = new Layer(2, 2, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            previousLayer.SetOutputs(new double[] { 0.5, 0.8 });

            Layer layer = new Layer(2, previousLayer.Size, NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            layer.SetWeights(new double[,] { { 0.1, 0.2 }, { 0.3, 0.4 } });
            layer.SetBiases(new double[] { 0.5, 0.6 });

            double learningRate = 0.1;

            // Act
            layer.UpdateWeightsAndBiases(previousLayer, learningRate);

            // Assert
            double[,] expectedWeights = { { 0.095, 0.19 }, { 0.29, 0.38 } };
            double[] expectedBiases = { 0.495, 0.58 };
            double[,] actualWeights = layer.GetWeights();
            double[] actualBiases = layer.GetBiases();
            Assert.That(actualWeights.GetLength(0), Is.EqualTo(expectedWeights.GetLength(0)));
            Assert.That(actualWeights.GetLength(1), Is.EqualTo(expectedWeights.GetLength(1)));
            for (int i = 0; i < expectedWeights.GetLength(0); i++)
            {
                for (int j = 0; j < expectedWeights.GetLength(1); j++)
                {
                    Assert.That(actualWeights[i, j], Is.EqualTo(expectedWeights[i, j]).Within(0.01));
                }
            }
            Assert.That(actualBiases.Length, Is.EqualTo(expectedBiases.Length));
            for (int i = 0; i < expectedBiases.Length; i++)
            {
                Assert.That(actualBiases[i], Is.EqualTo(expectedBiases[i]).Within(0.01));
            }
        }
    }
}