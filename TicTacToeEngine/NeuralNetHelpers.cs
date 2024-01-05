
namespace TicTacToeEngine.Version3
{
    public class NeuralNetHelpers
    {
        // Converts the initial state to a one-dimensional array of doubles
        public static double[] InputToArray(Symbol[,] initial)
        {
            // Declare an array of doubles with the same size as the initial
            double[] array = new double[Board.Size * Board.Size];

            // Loop through the initial rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the initial columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Convert the symbol at the current cell to a double and store it in the array
                    array[i * Board.Size + j] = SymbolToDouble(initial[i, j]);
                }
            }

            // Return the array
            return array;

        }

        // Converts a symbol to a double
        public static double SymbolToDouble(Symbol symbol)
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


        // Generates a random initial state
        public static Symbol[,] GenerateRandomInput()
        {
            // Create a new initial with all cells empty
            Symbol[,] initial = new Symbol[Board.Size, Board.Size];

            // Create a random number generator
            Random random = new Random();

            // Loop through the initial rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the initial columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Generate a random number between 0 and 2
                    int randomNumber = random.Next(3);

                    // If the random number is 0, set the cell to X
                    if (randomNumber == 0)
                    {
                        initial[i, j] = Symbol.X;
                    }

                    // If the random number is 1, set the cell to O
                    else if (randomNumber == 1)
                    {
                        initial[i, j] = Symbol.O;
                    }

                    // If the random number is 2, set the cell to Empty
                    else
                    {
                        initial[i, j] = Symbol.Empty;
                    }
                }
            }

            // Return the initial
            return initial;

        }

        // Generates the optimal output for the input using the minimax algorithm
        private double[] GenerateOptimalOutput(Symbol[,] input)
        {
            // Declare an array of doubles with the same size as the initial
            double[] output = new double[Board.Size * Board.Size];

            // Loop through the initial rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the initial columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // If the cell is empty, calculate the minimax score for that move
                    if (input[i, j] == Symbol.Empty)
                    {
                        // Make a copy of the input initial
                        Symbol[,] initialCopy = CopyInput(input);

                        // Mark the cell with X
                        initialCopy[i, j] = Symbol.X;

                        // Calculate the minimax score for that move, assuming X is the maximizing player and O is the minimizing player
                        output[i * Board.Size + j] = Minimax(initialCopy, false);
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

        // Copies the initial state
        public static Symbol[,] CopyInput(Symbol[,] initial)
        {
            // Declare a new initial with the same size as the original initial
            Symbol[,] initialCopy = new Symbol[Board.Size, Board.Size];

            // Loop through the initial rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the initial columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // Copy the symbol at the current cell to the new initial
                    initialCopy[i, j] = initial[i, j];
                }
            }

            // Return the new initial
            return initialCopy;

        }

        // Calculates the minimax score for a given initial state and player
        public double Minimax(Symbol[,] initial, bool isMaximizing)
        {
            // Check the game status for the initial state
            Status status = CheckStatus(initial);

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

            // Loop through the initial rows
            for (int i = 0; i < Board.Size; i++)
            {
                // Loop through the initial columns
                for (int j = 0; j < Board.Size; j++)
                {
                    // If the cell is empty, make a move and recurse
                    if (initial[i, j] == Symbol.Empty)
                    {
                        // Make a copy of the initial
                        Symbol[,] initialCopy = CopyInput(initial);

                        // Mark the cell with the current player's symbol
                        initialCopy[i, j] = isMaximizing ? Symbol.X : Symbol.O;

                        // Recurse to get the score for that move
                        double score = Minimax(initialCopy, !isMaximizing);

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


        // Checks the game status for a given initial state
        public static Status CheckStatus(Symbol[,] initial)
        {
            // Check the rows
            for (int i = 0; i < Board.Size; i++)
            {
                if (initial[i, 0] != Symbol.Empty && initial[i, 0] == initial[i, 1] && initial[i, 0] == initial[i, 2])
                {
                    // A row is filled with the same symbol
                    return initial[i, 0] == Symbol.X ? Status.XWon : Status.OWon;
                }
            }

            // Check the columns
            for (int j = 0; j < Board.Size; j++)
            {
                if (initial[0, j] != Symbol.Empty &&
                    initial[0, j] == initial[1, j] &&
                    initial[0, j] == initial[2, j])
                {
                    // A column is filled with the same symbol
                    return initial[0, j] == Symbol.X ? Status.XWon : Status.OWon;
                }
            }

            // Check the diagonals
            if (initial[0, 0] != Symbol.Empty &&
                initial[0, 0] == initial[1, 1] &&
                initial[0, 0] == initial[2, 2])
            {
                // The main diagonal is filled with the same symbol
                return initial[0, 0] == Symbol.X ? Status.XWon : Status.OWon;
            }

            if (initial[0, 2] != Symbol.Empty &&
                initial[0, 2] == initial[1, 1] &&
                initial[0, 2] == initial[2, 0])
            {
                // The secondary diagonal is filled with the same symbol
                return initial[0, 2] == Symbol.X ? Status.XWon : Status.OWon;
            }

            // Check if the initial is full
            bool isFull = true;
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (initial[i, j] == Symbol.Empty)
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
                // The initial is full and there is no winner
                return Status.Draw;
            }

            // The game is still in progress
            return Status.InProgress;

        }
    }
}