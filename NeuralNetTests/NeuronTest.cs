
using NUnit.Framework;

namespace NeuralNet.Tests
{
    [TestFixture]
    public class NeuronTests
    {
        [Test]
        public void SetValue_Should_SetValueOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);

            // Act
            neuron.SetValue(5);

            // Assert
            Assert.That(neuron.GetValue(), Is.EqualTo(5));
        }

        [Test]
        public void SetInput_Should_SetInputOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);

            // Act
            neuron.SetInput(2);

            // Assert
            Assert.That(neuron.GetInput(), Is.EqualTo(2));
        }

        [Test]
        public void SetError_Should_SetErrorOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);

            // Act
            neuron.SetError(0.5);

            // Assert
            Assert.That(neuron.GetError(), Is.EqualTo(0.5));
        }

        [Test]
        public void AddInput_Should_AddValueToInputOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);

            // Act
            neuron.AddInput(3);

            // Assert
            Assert.That(neuron.GetInput(), Is.EqualTo(3));
        }

        [Test]
        public void AddError_Should_AddValueToErrorOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);

            // Act
            neuron.AddError(0.2);

            // Assert
            Assert.That(neuron.GetError(), Is.EqualTo(0.2));
        }

        [Test]
        public void CalculateOutput_Should_ApplyActivationFunctionToInput()
        {
            // Arrange
            var neuron = new Neuron(x => x * 2, null);
            neuron.SetInput(3);

            // Act
            neuron.CalculateOutput();

            // Assert
            Assert.That(neuron.GetValue(), Is.EqualTo(6));
        }

        [Test]
        public void ApplyDerivative_Should_MultiplyErrorByDerivativeFunctionOfInput()
        {
            // Arrange
            var neuron = new Neuron(x => x/3, x => x * 3);
            neuron.SetInput(3);
            neuron.CalculateOutput(); // Calculate the output based on the input
            neuron.SetError(0.1);

            // Act
            neuron.ApplyDerivative();

            // Assert that the error is equal to 0.3 within a margin of 0.000001
            Assert.That(neuron.GetError(), Is.EqualTo(0.3).Within(0.000001));
        }

        [Test]
        public void ApplyDerivative_ShouldAdjustErrorCorrectly()
        {
            // Arrange
            var neuron = new Neuron(NeuralNetwork.Sigmoid, NeuralNetwork.SigmoidDerivative);
            neuron.SetInput(0.5); // Set an input value
            neuron.CalculateOutput(); // Calculate the output based on the input
            neuron.SetError(0.1); // Set an initial error
           

            var expectedError = 0.1 * NeuralNetwork.SigmoidDerivative(neuron.GetValue()); // Calculate the expected adjusted error

            // Act
            neuron.ApplyDerivative();

            // Assert
            Assert.That(neuron.GetError(), Is.EqualTo(expectedError).Within(0.0001)); // Check if the adjusted error is as expected
        }

        [Test]
        public void GetOutput_Should_ReturnValueOfNeuron()
        {
            // Arrange
            var neuron = new Neuron(null, null);
            neuron.SetValue(4);

            // Act
            var output = neuron.GetOutput();

            // Assert
            Assert.That(output, Is.EqualTo(4));
        }

        // #### 1. Neuron Class


// #### 4. Neuron Activation
//   - **Functionality**: Define activation function for neurons.
        // Tests that the value of the neuron is appropriately calculated
        [Test]
        public void CalculateValue_Should_CalculateValue()
        {
            // Arrange
            var neuron = new Neuron(NeuralNetwork.Sigmoid, null);
            neuron.SetInput(0.5); // Set an input value

            var expectedValue = NeuralNetwork.Sigmoid(neuron.GetInput()); // Calculate the expected value

            // Act
            neuron.CalculateOutput();

            // Assert
            Assert.That(neuron.GetValue(), Is.EqualTo(expectedValue)); // Check if the value is as expected
        }
//     - **Function Types**: Include common functions like Sigmoid, ReLU.
//       - **Extensibility**: Ability to plug in custom activation functions.
//       - **Default Option**: Set a default activation function.

// #### 5. Weight Initialization
//   - **Functionality**: Define strategy for initializing weights.
//   - **Sub-points**:
//     - **Strategies**: Include methods like Xavier, He initialization.
//       - **Random Initialization**: Default random weight initialization.
//       - **Custom Initialization**: Allow custom weight initialization methods.

    }
}
