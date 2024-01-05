using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeEngine.Version3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using NeuralNet;

    // Represents the tic-tac-toe board
    public class Board
    {
        public const int Size = 3;
        public readonly Symbol[,] board;

        public Board()
        {
            board = new Symbol[Size, Size];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = Symbol.Empty;
                }
            }
        }

        public void UpdateBoard(Symbol symbol, int row, int column)
        {
            board[row, column] = symbol;
        }

        public Symbol[,] GetBoardState()
        {
            return board;
        }

    }

    // Represents the tic-tac-toe game
    public class Game
    {
        public Player currentPlayer;
        public Board board;
        public Status status;

        public Symbol[,] GetBoardState()
        {
            return board.GetBoardState();
        }

        public Game(Player firstPlayer)
        {
            currentPlayer = firstPlayer;
            board = new Board();
            status = Status.InProgress;
        }

        // Returns the symbol as a string
        public string GetSymbolString(Symbol symbol)
        {
            return symbol == Symbol.X ? "X" : symbol == Symbol.O ? "O" : " ";
        }

        public Status CheckStatus()
        {
            // Check the rows, columns, and diagonals for a win
            for (int i = 0; i < Board.Size; i++)
            {
                if (board.GetBoardState()[i, 0] != Symbol.Empty &&
                    board.GetBoardState()[i, 0] == board.GetBoardState()[i, 1] &&
                    board.GetBoardState()[i, 0] == board.GetBoardState()[i, 2])
                {
                    status = board.GetBoardState()[i, 0] == Symbol.X ? Status.XWon : Status.OWon;
                    return status;
                }

                if (board.GetBoardState()[0, i] != Symbol.Empty &&
                    board.GetBoardState()[0, i] == board.GetBoardState()[1, i] &&
                    board.GetBoardState()[0, i] == board.GetBoardState()[2, i])
                {
                    status = board.GetBoardState()[0, i] == Symbol.X ? Status.XWon : Status.OWon;
                    return status;
                }
            }

            if (board.GetBoardState()[0, 0] != Symbol.Empty &&
                board.GetBoardState()[0, 0] == board.GetBoardState()[1, 1] &&
                board.GetBoardState()[0, 0] == board.GetBoardState()[2, 2])
            {
                status = board.GetBoardState()[0, 0] == Symbol.X ? Status.XWon : Status.OWon;
                return status;
            }

            if (board.GetBoardState()[0, 2] != Symbol.Empty &&
                board.GetBoardState()[0, 2] == board.GetBoardState()[1, 1] &&
                board.GetBoardState()[0, 2] == board.GetBoardState()[2, 0])
            {
                status = board.GetBoardState()[0, 2] == Symbol.X ? Status.XWon : Status.OWon;
                return status;
            }

            // Check for a draw
            bool isFull = true;
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (board.GetBoardState()[i, j] == Symbol.Empty)
                    {
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
                status = Status.Draw;
                return status;
            }

            // The game is still in progress
            status = Status.InProgress;
            return status;
        }

        public bool ValidateMove(int row, int column)
        {
            if (row < 0 || row >= Board.Size || column < 0 || column >= Board.Size)
            {
                return false; // Out of bounds
            }

            if (board.GetBoardState()[row, column] != Symbol.Empty)
            {
                return false; // Cell is already occupied
            }

            return true; // Valid move
        }

        public void MakeMove(int row, int column)
        {
            if (ValidateMove(row, column))
            {
                board.UpdateBoard(currentPlayer == Player.X ? Symbol.X : Symbol.O, row, column);
                currentPlayer = currentPlayer.GetOppositePlayer();
                status = CheckStatus();
            }
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public Status GetStatus()
        {
            return status;
        }
    }

    // Represents the two players, X and O
    public enum Player { X, O }

    // Extension method to get the opposite player
    public static class PlayerExtensions
    {
        public static Player GetOppositePlayer(this Player player)
        {
            return player == Player.X ? Player.O : Player.X;
        }
    }

    // Represents the three symbols, X, O, and Empty
    public enum Symbol { X, O, Empty }

    // Represents the four possible game statuses
    public enum Status { InProgress, XWon, OWon, Draw }

    // Represents the neural network engine
    public class Engine
    {
        public readonly NeuralNetwork neuralNetwork;
        public readonly int difficulty;
        public readonly Symbol humanSymbol;
        public readonly Symbol computerSymbol;
        public readonly int seed;

        public Engine(int difficulty, Symbol humanSymbol, int seed)
        {
            if (difficulty < 1 || difficulty > 10)
            {
                throw new ArgumentException("Invalid difficulty level");
            }

            if (humanSymbol == Symbol.Empty)
            {
                throw new ArgumentException("Invalid human symbol");
            }

            this.difficulty = difficulty;
            this.humanSymbol = humanSymbol;
            this.computerSymbol = humanSymbol == Symbol.X ? Symbol.O : Symbol.X;
            this.seed = seed;
            neuralNetwork = new NeuralNetwork(9, 9, 9);
            Initialize();
        }

        public void Initialize()
        {
            neuralNetwork.InitializeWeights(seed);
        }

        public Status PlayGame()
        {
            var game = new Game(Player.X); // Computer always starts first
            var random = new Random(seed);

            while (game.GetStatus() == Status.InProgress)
            {
                if (game.GetCurrentPlayer() == Player.X) // Computer's turn
                {
                    int computerMove = MakeComputerMove(game, random);
                    int row = computerMove / Board.Size;
                    int column = computerMove % Board.Size;
                    game.MakeMove(row, column);
                }
                else // Human's turn
                {
                    // Implement human input here (e.g., from console)
                    // You can prompt the user for row and column
                    // and then call game.MakeMove(row, column) to make the move.
                }
            }

            return game.GetStatus();
        }

        public int MakeComputerMove(Game game, Random random)
        {
            int bestMove = -1;
            double bestScore = double.NegativeInfinity;

            var boardState = game.board.GetBoardState();

            for (int move = 0; move < Board.Size * Board.Size; move++)
            {
                int row = move / Board.Size;
                int column = move % Board.Size;

                if (boardState[row, column] == Symbol.Empty)
                {
                    boardState[row, column] = computerSymbol;

                    double score = MiniMax(boardState, 0, false);

                    boardState[row, column] = Symbol.Empty; // Undo the move

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                }
            }

            return bestMove;
        }

        public double MiniMax(Symbol[,] boardState, int depth, bool isMaximizing)
        {
            Status currentStatus = CheckBoardStatus(boardState);

            if (currentStatus == Status.XWon)
            {
                return -1;
            }
            else if (currentStatus == Status.OWon)
            {
                return 1;
            }
            else if (currentStatus == Status.Draw)
            {
                return 0;
            }

            if (isMaximizing)
            {
                double bestScore = double.NegativeInfinity;

                for (int move = 0; move < Board.Size * Board.Size; move++)
                {
                    int row = move / Board.Size;
                    int column = move % Board.Size;

                    if (boardState[row, column] == Symbol.Empty)
                    {
                        boardState[row, column] = computerSymbol;

                        double score = MiniMax(boardState, depth + 1, false);

                        boardState[row, column] = Symbol.Empty; // Undo the move

                        bestScore = Math.Max(score, bestScore);
                    }
                }

                return bestScore;
            }
            else
            {
                double bestScore = double.PositiveInfinity;

                for (int move = 0; move < Board.Size * Board.Size; move++)
                {
                    int row = move / Board.Size;
                    int column = move % Board.Size;

                    if (boardState[row, column] == Symbol.Empty)
                    {
                        boardState[row, column] = humanSymbol;

                        double score = MiniMax(boardState, depth + 1, true);

                        boardState[row, column] = Symbol.Empty; // Undo the move

                        bestScore = Math.Min(score, bestScore);
                    }
                }

                return bestScore;
            }
        }

        public Status CheckBoardStatus(Symbol[,] boardState)
        {
            var game = new Game(Player.X); // Temporary game instance for status checking
            game.board = new Board();
            Array.Copy(boardState, game.board.GetBoardState(), boardState.Length);

            return game.CheckStatus();
        }

        // ... (other methods)

        public void PrintBoard(Symbol[,] boardState)
        {
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    if (boardState[i, j] == Symbol.X)
                    {
                        Console.Write("X");
                    }
                    else if (boardState[i, j] == Symbol.O)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("_");
                    }

                    if (j < Board.Size - 1)
                    {
                        Console.Write(" | ");
                    }
                }

                Console.WriteLine();

                if (i < Board.Size - 1)
                {
                    Console.WriteLine("---------");
                }
            }
        }
    }
}
