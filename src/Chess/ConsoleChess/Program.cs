using System;
using ChessLibrary;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {

            User player1 = new User("Player 1", "mdp", Color.White, false, new List<Piece>(), 0);
            User player2 = new User("Player 2", "mdp", Color.Black, false, new List<Piece>(), 0);

            Game game = new Game(player1, player2);
            
            game.EvolveNotified += (sender, args) =>
            {
                ChoiceUser choice = GetUserChoice();
                Evolve(game, args.Pawn, args.Case, choice);
            };
            
            game.GameOverNotified += (sender, args) =>
            {
                Console.WriteLine($"Game over! {args.Winner.Pseudo} wins!");
            };
            
            int player = 1;
            bool isGameOver = false;
            DisplayBoard(game.Board);
            while (!isGameOver)
            {
                User actualPlayer = (player % 2 == 0) ? player2 : player1;
                Console.WriteLine($"{actualPlayer.Pseudo}'s turn");

                try
                {
                    (int startColumn, int startRow) = GetMoveCoordinates("Enter the position of the piece you want to move (a1, f7 ...):");
                    (int endColumn, int endRow) = GetMoveCoordinates("Enter the destination position (a1, f7 ...):");

                    game.MovePiece(game.Board.Board[startColumn, startRow], game.Board.Board[endColumn, endRow], game.Board, actualPlayer);
                    DisplayBoard(game.Board);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    player--; // Retry the same player's turn
                }

                if( game.CheckChec(game, actualPlayer)) 
                {
                    isGameOver = game.GameOver(actualPlayer);
                }
                player++;
            }

            game.GameOver(player % 2 == 0 ? player1 : player2);
        }


        static (int, int) GetMoveCoordinates(string prompt)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(prompt);
                    string pos = Console.ReadLine();
                    return ParseChessNotation(pos);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}. Please try again.");
                }
            }
        }

        /// <summary>
        /// Change a Pawn to another piece.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        static void Evolve(Game game, Pawn? P, Case C, ChoiceUser choiceUser)
        {
            Queen newQueen;
            Rook newRook;
            Knight newKnight;
            Bishop newBishop;
            
            switch (choiceUser)
            {
                case ChoiceUser.Queen:
                    newQueen = new Queen(P.Color, P.Id);
                    C.Piece = newQueen;
                    game.Board.ModifPawn(P, newQueen, C);
                    return;

                case ChoiceUser.Rook:
                    newRook = new Rook(P.Color, P.Id);
                    C.Piece = newRook;
                    game.Board.ModifPawn(P, newRook, C);
                    return;
                case ChoiceUser.Bishop:
                    newBishop = new Bishop(P.Color, P.Id);
                    C.Piece = newBishop;
                    game.Board.ModifPawn(P, newBishop, C);
                    return;
                case ChoiceUser.Knight:
                    newKnight = new Knight(P.Color, P.Id);
                    C.Piece = newKnight;
                    game.Board.ModifPawn(P, newKnight, C);
                    return;
                default:
                    break;
            }
        }
        
        static ChoiceUser GetUserChoice()
        {
            int choice;
            do
            {
                Console.WriteLine("Choose the piece to evolve to:");
                Console.WriteLine("1. Queen");
                Console.WriteLine("2. Rook");
                Console.WriteLine("3. Bishop");
                Console.WriteLine("4. Knight");

                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                }
            } while (choice < 1 || choice > 4);

            return (ChoiceUser)choice;
        }

        static void DisplayBoard(Chessboard chessboard)
        {
            Console.WriteLine("   a   b   c   d   e   f   g   h");
            Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            for (int row = 0; row < 8; row++)
            {
                Console.Write((8 - row) + " |");
                for (int column = 0; column < 8; column++)
                {
                    var square = chessboard.Board[column, row];
                    string pieceSymbol = square?.Piece != null ? GetPieceSymbol(square.Piece) : " ";
                    ConsoleColor originalColor = Console.ForegroundColor;
                    if (square?.Piece != null && square.Piece.Color != Color.White)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    }
                    Console.Write($" {pieceSymbol} ");
                    Console.ForegroundColor = originalColor;
                    Console.Write("|");
                }
                Console.WriteLine($" {8 - row}");
                Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            }
            Console.WriteLine("   a   b   c   d   e   f   g   h");
        }

        static string GetPieceSymbol(Piece piece)
        {
            return piece.GetType().Name switch
            {
                "Pawn" => "P",
                "Rook" => "R",
                "Knight" => "C",
                "Bishop" => "B",
                "Queen" => "Q",
                "King" => "K",
                _ => "?",
            };
        }

        static (int column, int row) ParseChessNotation(string notation)
        {
            if (notation.Length != 2 || notation[0] < 'a' || notation[0] > 'h' || notation[1] < '1' || notation[1] > '8')
            {
                throw new ArgumentException("Invalid chess notation.");
            }

            int column = notation[0] - 'a';
            int row = 8 - (notation[1] - '0'); 

            return (column, row);
        }
    }
}
