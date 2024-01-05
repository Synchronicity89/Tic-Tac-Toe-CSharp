using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using NeuralNet;
using TicTacToeEngine;
using TicTacToeEngine.Version3;

namespace TicTacToeEngine
{
    // Represents the neural network engine

    public class Engine
    {
        // The neural network model
        public NeuralNetwork neuralNetwork;
        // The difficulty level of the computer player, from 1 (easy) to 10 (hard)
        int difficulty;

        // The symbol of the human player, either X or O
        Symbol humanSymbol;

        // The symbol of the computer player, either X or O
        Symbol computerSymbol;

        // The random seed for initializing the neural network weights and selecting the moves
        int seed;

        // Creates a new engine with the given difficulty level, human symbol, and seed
        Engine(int difficulty, Symbol humanSymbol, int seed)
        {
            // Validate the difficulty level
            if (difficulty < 1 || difficulty > 10)
            {
                throw new ArgumentException("Invalid difficulty level");
            }

            // Validate the human symbol
            if (humanSymbol == Symbol.Empty)
            {
                throw new ArgumentException("Invalid human symbol");
            }

            // Set the difficulty level, human symbol, and seed
            this.difficulty = difficulty;
            this.humanSymbol = humanSymbol;
            this.seed = seed;

            // Set the computer symbol as the opposite of the human symbol
            Symbol computerSymbol = GetOppositeSymbol(humanSymbol);

            // Create a new neural network model with 9 input neurons, 9 hidden neurons, and 9 output neurons
            neuralNetwork = new NeuralNetwork(9, 9, 9);

            // Initialize the neural network weights with the random seed
            neuralNetwork.InitializeWeights(seed);

            // Train the neural network with the given number of epochs, based on the difficulty level
            neuralNetwork.Train(difficulty * 1000, null);
        }

        public Symbol GetOppositeSymbol(Symbol aSymbol)
        {
            Symbol oppositeSymbol = aSymbol == Symbol.X ? Symbol.O : Symbol.X;
            return oppositeSymbol;
        }

        // Plays the game against the human player and returns the game status
        Status PlayGame()
        {
            // Create a new game with a random player as the first player
            Random random = new Random(seed);
            Player firstPlayer = random.Next(2) == 0 ? Player.X : Player.O;
            Game game = new Game(firstPlayer);

            // Loop until the game is over
            while (game.GetStatus() == Status.InProgress)
            {
                // Get the current player
                Player currentPlayer = game.GetCurrentPlayer();

                // Get the current board state
                Symbol[,] boardState = game.GetBoardState();

                // Declare the row and column variables for the move
                int row;
                int column;
                bool currentPlayerIsHuman = currentPlayer == Player.X && humanSymbol == Symbol.X || currentPlayer == Player.O && humanSymbol == Symbol.O;

                // If the current player is the human player
                if (currentPlayerIsHuman)
                {
                    // Ask the human player to enter the row and column for the move
                    Console.WriteLine("Your turn. Enter the row and column for your move (1-3):");

                    // Read the row and column from the console input
                    row = int.Parse(Console.ReadLine()) - 1;
                    column = int.Parse(Console.ReadLine()) - 1;

                    // Validate the move
                    if (!game.ValidateMove(row, column))
                    {
                        // If the move is invalid, print an error message and continue the loop
                        Console.WriteLine("Invalid move. Try again.");
                        continue;
                    }
                }
                // Else, if the current player is the computer player
                else
                {
                    // Print a message that the computer is making a move
                    Console.WriteLine("Computer's turn.");

                    // Get the output of the neural network for the current board state
                    double[] output = neuralNetwork.FeedForward(NeuralNetHelpers.InputToArray(boardState));

                    // Declare a list to store the indices of the optimal moves
                    List<int> optimalMoves = new List<int>();

                    // Declare a variable to store the maximum output value
                    double maxOutput = double.NegativeInfinity;

                    // Loop through the output array
                    for (int i = 0; i < output.Length; i++)
                    {
                        // If the output value is greater than the maximum output value
                        if (output[i] > maxOutput)
                        {
                            // Update the maximum output value
                            maxOutput = output[i];

                            // Clear the list of optimal moves
                            optimalMoves.Clear();

                            // Add the current index to the list of optimal moves
                            optimalMoves.Add(i);
                        }
                        // Else, if the output value is equal to the maximum output value
                        else if (output[i] == maxOutput)
                        {
                            // Add the current index to the list of optimal moves
                            optimalMoves.Add(i);
                        }
                    }

                    // Select a random index from the list of optimal moves
                    int index = optimalMoves[random.Next(optimalMoves.Count)];

                    // Convert the index to the row and column
                    row = index / Board.Size;
                    column = index % Board.Size;
                }

                // Make the move
                game.MakeMove(row, column);

                // Print the board state
                PrintBoard(boardState, game);
            }

            // Return the game status
            return game.GetStatus();
        }

        // Prints the board state to the console
        void PrintBoard(Symbol[,] boardState, Game game)
        {
            // Loop through the rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Print the symbol of the cell
                    Console.Write(  game.GetSymbolString(boardState[i, j]));

                    // Print a separator if not the last column
                    if (j < Board.Size - 1)
                    {
                        Console.Write("|");
                    }
                }

                // Print a newline
                Console.WriteLine();

                // Print a separator if not the last row
                if (i < Board.Size - 1)
                {
                    Console.WriteLine("-----");
                }
            }
        }

    }
}



