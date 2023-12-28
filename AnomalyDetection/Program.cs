// Implement the main program logic here
// Initialize the Engine and start the game
using TicTacToeEngine.Version3;

Game game = new Game(Player.X);
Engine engine = new Engine(1, Symbol.X, 42);
Dictionary<double, double> errors = new Dictionary<double, double>();
Action<double, double> addError = (double x, double y) => errors.Add(x, y);
engine.neuralNetwork.Train(10000, addError);
for(int epoch = 0; epoch < 10000; epoch+=100)
{
    Console.WriteLine("Epoch: " + epoch + ", error: " + errors[epoch]);
}
//test the prediction of the Neural Network with some board states
Console.WriteLine("Prediction of the Neural Network with some board states");
Console.WriteLine("-------------------------------------------------------");

for (int j = 0; j < 9; j++)
{

    Symbol[,] input = engine.neuralNetwork.GenerateRandomBoard();

    // Generate the optimal output for the input using the minimax algorithm
    double[] output = engine.neuralNetwork.GenerateOptimalOutput(input);

    // Feed the input forward through the network and get the predicted output
    double[] predictedOutput = engine.neuralNetwork.FeedForward(input);

    // render the input as text and print it
    string inputText = "";
    // create the loop nesting for the 2D array to render to text
    for (int i = 0; i < input.GetLength(0); i++)
    {
        for (int k = 0; k < input.GetLength(1); k++)
        {
            inputText += input[i, k] + " ";
        }
    }
    Console.WriteLine("Input: " + inputText);

    // render predicted output as text
    string predictedOutputText = "";
    for (int i = 0; i < predictedOutput.Length; i++)
    {
        predictedOutputText += predictedOutput[i] + " ";
    }
    Console.WriteLine("Prediction: " + predictedOutputText);

    // render optimal output as text and print it
    string optimalOutputText = "";
    for (int i = 0; i < output.Length; i++)
    {
        optimalOutputText += output[i] + " ";
    }
    Console.WriteLine("Optimal Output: " + optimalOutputText);
    Console.WriteLine();

    // create a report about how close the prediction was to the optimal output, and print it
    string report = "";
    for (int i = 0; i < predictedOutput.Length; i++)
    {
        report += Math.Round(predictedOutput[i] - output[i], 2) + " ";
    }
    Console.WriteLine("Report: " + report);
    Console.WriteLine();
}

// Print the predicted output

Console.WriteLine("End of test");

// Play the game
//while (!game.status.Equals(.Over))

// Allow human input to play against the computer

