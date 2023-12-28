using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using TicTacToeEngine;
using TicTacToeEngine.Version3;

namespace TicTacToeEngine
{
    // Represents the tic-tac-toe board

    // Represents the tic-tac-toe game
    //public class Game
    //{
    //    // The current player
    //    public Player currentPlayer;

    //    // The board
    //    public Board board;

    //    // The game status
    //    public Status status;

    //    // Creates a new game with the given player as the first player
    //    public Game(Player firstPlayer)
    //    {
    //        currentPlayer = firstPlayer;
    //        board = new Board();
    //        status = Status.InProgress;
    //    }

    //    // Checks the game status and updates it accordingly
    //    public void CheckStatus()
    //    {
    //        // Get the board state
    //        Symbol[,] boardState = board.GetBoardState();

    //        // Check the rows
    //        for (int i = 0; i < Board.Size; i++)
    //        {
    //            if (boardState[i, 0] != Symbol.Empty &&
    //                boardState[i, 0] == boardState[i, 1] &&
    //                boardState[i, 0] == boardState[i, 2])
    //            {
    //                // A row is filled with the same symbol
    //                status = boardState[i, 0] == Symbol.X ? Status.XWon : Status.OWon;
    //                return;
    //            }
    //        }

    //        // Check the columns
    //        for (int j = 0; j < Board.Size; j++)
    //        {
    //            if (boardState[0, j] != Symbol.Empty &&
    //                boardState[0, j] == boardState[1, j] &&
    //                boardState[0, j] == boardState[2, j])
    //            {
    //                // A column is filled with the same symbol
    //                status = boardState[0, j] == Symbol.X ? Status.XWon : Status.OWon;
    //                return;
    //            }
    //        }

    //        // Check the diagonals
    //        if (boardState[0, 0] != Symbol.Empty &&
    //            boardState[0, 0] == boardState[1, 1] &&
    //            boardState[0, 0] == boardState[2, 2])
    //        {
    //            // The main diagonal is filled with the same symbol
    //            status = boardState[0, 0] == Symbol.X ? Status.XWon : Status.OWon;
    //            return;
    //        }

    //        if (boardState[0, 2] != Symbol.Empty &&
    //            boardState[0, 2] == boardState[1, 1] &&
    //            boardState[0, 2] == boardState[2, 0])
    //        {
    //            // The secondary diagonal is filled with the same symbol
    //            status = boardState[0, 2] == Symbol.X ? Status.XWon : Status.OWon;
    //            return;
    //        }

    //        // Check if the board is full
    //        bool isFull = true;
    //        for (int i = 0; i < Board.Size; i++)
    //        {
    //            for (int j = 0; j < Board.Size; j++)
    //            {
    //                if (boardState[i, j] == Symbol.Empty)
    //                {
    //                    // There is an empty cell
    //                    isFull = false;
    //                    break;
    //                }
    //            }
    //            if (!isFull)
    //            {
    //                break;
    //            }
    //        }

    //        if (isFull)
    //        {
    //            // The board is full and there is no winner
    //            status = Status.Draw;
    //            return;
    //        }

    //        // The game is still in progress
    //        status = Status.InProgress;
    //    }

    //    // Validates the move and returns true if it is valid, false otherwise
    //    public bool ValidateMove(int row, int column)
    //    {
    //        // Get the board state
    //        Symbol[,] boardState = board.GetBoardState();


    //        // Check if the row and column are within the board range
    //        if (row < 0 || row >= Board.Size || column < 0 || column >= Board.Size)
    //        {
    //            return false;
    //        }

    //        // Check if the cell is empty
    //        if (boardState[row, column] != Symbol.Empty)
    //        {
    //            return false;
    //        }

    //        // The move is valid
    //        return true;
    //    }

    //    // Makes the move and updates the board and the game status
    //    public void MakeMove(int row, int column)
    //    {
    //        // Update the board with the current player's symbol
    //        board.UpdateBoard(currentPlayer == Player.X ? Symbol.X : Symbol.O, row, column);

    //        // Check the game status
    //        CheckStatus();

    //        // Switch the current player
    //        currentPlayer = GetOppositePlayer(currentPlayer);
    //    }

    //    // Returns the current player
    //    public Player GetCurrentPlayer()
    //    {
    //        return currentPlayer;
    //    }

    //    // Returns the game status
    //    public Status GetStatus()
    //    {
    //        return status;
    //    }

    ////Represents the two players, X and O 
    ////public enum Player { X, O }

    //// Returns the opposite player
    //public Player GetOppositePlayer(Player player) { return player == Player.X ? Player.O : Player.X; }

    //// Represents the three symbols, X, O, and Empty
    ////public enum Symbol { X, O, Empty }

    //// Returns the symbol as a string
    //public string GetSymbolString(Symbol symbol)
    //{
    //    return symbol == Symbol.X ? "X" : symbol == Symbol.O ? "O" : " ";
    //}

    //// Represents the four possible game statuses, InProgress, XWon, OWon, and Draw
    //// public enum Status { InProgress, XWon, OWon, Draw }

    //// Returns the status as a string
    //public string GetStatusString(Status status)
    //{
    //    return status == Status.InProgress ? "In Progress" : status == Status.XWon ? "X Won" : status == Status.OWon ? "O Won" : "Draw";
    //}
    //}

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
                    double[] output = neuralNetwork.FeedForward(boardState);

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

    // Represents the neural network model
    public class NeuralNetwork
    { 
        // The input layer
        public Layer inputLayer;

        // The hidden layer
        public Layer hiddenLayer;

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
        public NeuralNetwork(int inputSize, int hiddenSize, int outputSize)
        {
            // Create the input layer with the given size and a linear activation function
            inputLayer = new Layer(inputSize, Linear, LinearDerivative);

            // Create the hidden layer with the given size and a sigmoid activation function
            hiddenLayer = new Layer(hiddenSize, Sigmoid, SigmoidDerivative);

            // Create the output layer with the given size and a sigmoid activation function
            outputLayer = new Layer(outputSize, Sigmoid, SigmoidDerivative);

            // Set the learning rate to 0.1
            learningRate = 0.1;
        }

        // Initializes the weights of the neural network with random values from a normal distribution
        public void InitializeWeights(int seed)
        {
            // Create a random number generator with the given seed
            Random random = new Random(seed);

            // Loop through the hidden layer neurons
            for (int i = 0; i < hiddenLayer.Size; i++)
            {
                // Loop through the input layer neurons
                for (int j = 0; j < inputLayer.Size; j++)
                {
                    // Set the weight from the input neuron to the hidden neuron to a random value
                    hiddenLayer.SetWeight(i, j, random.NextDouble());
                }

                // Set the bias of the hidden neuron to a random value
                hiddenLayer.SetBias(i, random.NextDouble());
            }

            // Loop through the output layer neurons
            for (int i = 0; i < outputLayer.Size; i++)
            {
                // Loop through the hidden layer neurons
                for (int j = 0; j < hiddenLayer.Size; j++)
                {
                    // Set the weight from the hidden neuron to the output neuron to a random value
                    outputLayer.SetWeight(i, j, random.NextDouble());
                }

                // Set the bias of the output neuron to a random value
                outputLayer.SetBias(i, random.NextDouble());
            }

        }

        // Feeds the input data forward through the neural network and returns the output
        public double[] FeedForward(Symbol[,] input)
        {
            // Convert the input board state to a one-dimensional array of doubles
            double[] inputArray = BoardToArray(input);

            // Set the input layer values to the input array values
            inputLayer.SetValues(inputArray);

            // Calculate the hidden layer values by multiplying the input layer values with the weights and adding the biases
            hiddenLayer.CalculateValues(inputLayer);

            // Calculate the output layer values by multiplying the hidden layer values with the weights and adding the biases
            outputLayer.CalculateValues(hiddenLayer);

            // Return the output layer values as an array
            return outputLayer.GetValues();

        }

        // Trains the neural network with the given number of epochs using backpropagation and gradient descent
        public void Train(int epochs, Action<double, double> progressCallback)
        {
            // Loop for the given number of epochs
            for (int i = 0; i < epochs; i++)
            {
                // Generate a random board state as the input
                Symbol[,] input = GenerateRandomBoard();

                // Generate the optimal output for the input using the minimax algorithm
                double[] output = GenerateOptimalOutput(input);

                // Feed the input forward through the network and get the predicted output
                double[] predictedOutput = FeedForward(input);
                
                // use the progressCallback to report the error from the difference between the opimal ouput and the predicted output.
                // The error is the sum of the squares of the differences between the optimal output and the predicted output, the second parameter is i
                // which is the epoch number.
                progressCallback?.Invoke(i, output.Zip(predictedOutput, (a, b) => a - b).Sum(x => x * x));

                // Calculate the output layer errors by subtracting the predicted output from the optimal output
                outputLayer.CalculateErrors(outputLayer, predictedOutput);

                // Calculate the hidden layer errors by multiplying the output layer errors with the weights and applying the derivative function
                hiddenLayer.CalculateErrors(outputLayer);

                // Update the output layer weights and biases by multiplying the output layer errors with the hidden layer values and subtracting the product from the learning rate
                outputLayer.UpdateWeightsAndBiases(hiddenLayer, learningRate);

                // Update the hidden layer weights and biases by multiplying the hidden layer errors with the input layer values and subtracting the product from the learning rate
                hiddenLayer.UpdateWeightsAndBiases(inputLayer, learningRate);
            }

        }

        // Converts the board state to a one-dimensional array of doubles
        public double[] BoardToArray(Symbol[,] board)
        {
            // Declare an array of doubles with the same size as the board
            double[] array = new double[Board.Size * Board.Size];

            // Loop through the board rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the board columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Convert the symbol at the current cell to a double and store it in the array
                    array[i * Board.Size + j] = SymbolToDouble(board[i, j]);
                }
            }

            // Return the array
            return array;

        }

        // Converts a symbol to a double
        public double SymbolToDouble(Symbol symbol)
        {
            // If the symbol is X, return 1.0
            if (symbol == Symbol.X)
            {
                return 1.0;

            }
            // If the symbol is O, return -1.0
            if (symbol == Symbol.O)
            {
                return -1.0;
            }

            // If the symbol is Empty, return 0.0
            return 0.0;

        }


        // Generates a random board state
        public Symbol[,] GenerateRandomBoard()
        {
            // Create a new board with all cells empty
            Symbol[,] board = new Symbol[Board.Size, Board.Size];

            // Create a random number generator
            Random random = new Random();

            // Loop through the board rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the board columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Generate a random number between 0 and 2
                    int randomNumber = random.Next(3);

                    // If the random number is 0, set the cell to X
                    if (randomNumber == 0)
                    {
                        board[i, j] = Symbol.X;
                    }

                    // If the random number is 1, set the cell to O
                    else if (randomNumber == 1)
                    {
                        board[i, j] = Symbol.O;
                    }

                    // If the random number is 2, set the cell to Empty
                    else
                    {
                        board[i, j] = Symbol.Empty;
                    }
                }
            }

            // Return the board
            return board;

        }

        // Generates the optimal output for the input using the minimax algorithm
        public double[] GenerateOptimalOutput(Symbol[,] input)
        {
            // Declare an array of doubles with the same size as the board
            double[] output = new double[Board.Size * Board.Size];

            // Loop through the board rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the board columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // If the cell is empty, calculate the minimax score for that move
                    if (input[i, j] == Symbol.Empty)
                    {
                        // Make a copy of the input board
                        Symbol[,] boardCopy = CopyBoard(input);

                        // Mark the cell with X
                        boardCopy[i, j] = Symbol.X;

                        // Calculate the minimax score for that move, assuming X is the maximizing player and O is the minimizing player
                        output[i * Board.Size + j] = Minimax(boardCopy, false);
                    }

                    // If the cell is not empty, set the output to a large negative value
                    else
                    {
                        output[i * Board.Size + j] = -1000.0;
                    }
                }
            }

            // Return the output
            return output;

        }

        // Copies the board state
        public Symbol[,] CopyBoard(Symbol[,] board)
        {
            // Declare a new board with the same size as the original board
            Symbol[,] boardCopy = new Symbol[Board.Size, Board.Size];

            // Loop through the board rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the board columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Copy the symbol at the current cell to the new board
                    boardCopy[i, j] = board[i, j];
                }
            }

            // Return the new board
            return boardCopy;

        }

        // Calculates the minimax score for a given board state and player
        public double Minimax(Symbol[,] board, bool isMaximizing)
        {
            // Check the game status for the board state
            Status status = CheckStatus(board);

            // If the game is over, return a score based on the status
            if (status != Status.InProgress)
            {
                // If X won, return 10.0
                if (status == Status.XWon)
                {
                    return 10.0;
                }

                // If O won, return -10.0
                if (status == Status.OWon)
                {
                    return -10.0;
                }

                // If the game is a draw, return 0.0
                return 0.0;
            }

            // If the game is not over, declare a variable to store the best score
            double bestScore;

            // If the player is the maximizing player, set the best score to a large negative value
            if (isMaximizing)
            {
                bestScore = -1000.0;
            }

            // If the player is the minimizing player, set the best score to a large positive value
            else
            {
                bestScore = 1000.0;
            }

            // Loop through the board rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the board columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // If the cell is empty, make a move and recurse
                    if (board[i, j] == Symbol.Empty)
                    {
                        // Make a copy of the board
                        Symbol[,] boardCopy = CopyBoard(board);

                        // Mark the cell with the current player's symbol
                        boardCopy[i, j] = isMaximizing ? Symbol.X : Symbol.O;

                        // Recurse to get the score for that move
                        double score = Minimax(boardCopy, !isMaximizing);

                        // If the player is the maximizing player, update the best score if the score is greater
                        if (isMaximizing)
                        {
                            bestScore = Math.Max(bestScore, score);
                        }

                        // If the player is the minimizing player, update the best score if the score is smaller
                        else
                        {
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
            }

            // Return the best score
            return bestScore;

        }


        // Checks the game status for a given board state
        public Status CheckStatus(Symbol[,] board)
        {
            // Check the rows
            for (int i = 0; i < Board.Size; i++)
            {
                if (board[i, 0] != Symbol.Empty && board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2])
                {
                    // A row is filled with the same symbol
                    return board[i, 0] == Symbol.X ? Status.XWon : Status.OWon;
                }
            }

            // Check the columns
            for (int j = 0; j < Board.Size; j++)
            {
                if (board[0, j] != Symbol.Empty &&
                    board[0, j] == board[1, j] &&
                    board[0, j] == board[2, j])
                {
                    // A column is filled with the same symbol
                    return board[0, j] == Symbol.X ? Status.XWon : Status.OWon;
                }
            }

            // Check the diagonals
            if (board[0, 0] != Symbol.Empty &&
                board[0, 0] == board[1, 1] &&
                board[0, 0] == board[2, 2])
            {
                // The main diagonal is filled with the same symbol
                return board[0, 0] == Symbol.X ? Status.XWon : Status.OWon;
            }

            if (board[0, 2] != Symbol.Empty &&
                board[0, 2] == board[1, 1] &&
                board[0, 2] == board[2, 0])
            {
                // The secondary diagonal is filled with the same symbol
                return board[0, 2] == Symbol.X ? Status.XWon : Status.OWon;
            }

            // Check if the board is full
            bool isFull = true;
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (board[i, j] == Symbol.Empty)
                    {
                        // There is an empty cell
                        isFull = false;
                        break;
                    }
                }
                if (!isFull)
                {
                    break;
                }
            }

            if (isFull)
            {
                // The board is full and there is no winner
                return Status.Draw;
            }

            // The game is still in progress
            return Status.InProgress;

        }
    }
}



