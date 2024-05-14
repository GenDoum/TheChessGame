using System;
using ChessLibrary;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {
            
            User player1 = new User("Player 1", Color.White);
            User player2 = new User("Player 2", Color.Black);
            Game game = new Game(player1, player2);
            game.Board.SetupEvolveEventHandler();
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
                    isGameOver = game.CheckGameOver(game);
                }
                game.checkEvolved();
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