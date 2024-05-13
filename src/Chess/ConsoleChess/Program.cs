using System;
using ChessLibrary;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            User player1 = new User("Player 1", Color.White);
            User player2 = new User("Player 2", Color.Black);
            Game game = new Game(player1, player2);
            int player = 1;
            User actualPlayer = player1;
            bool IsGameOver = false;

            while (!IsGameOver)
            {
                actualPlayer = (player % 2 == 0) ? player2 : player1;
                Console.WriteLine($"{actualPlayer.Pseudo}'s turn");

                try
                {
                    DisplayBoard(game.Board);

                    Console.WriteLine("Enter the position of the piece you want to move (a1,f7 ...):");
                    string startPos = Console.ReadLine();
                    (int startColumn, int startRow) = ParseChessNotation(startPos);

                    Console.WriteLine("Enter the destination position(a1,f7 ...):");
                    string endPos = Console.ReadLine();
                    (int endColumn, int endRow) = ParseChessNotation(endPos);

                    game.MovePiece(game.Board.Board[startColumn, startRow], game.Board.Board[endColumn, endRow], game.Board, actualPlayer);
                    DisplayBoard(game.Board);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    player -= 1;
                }

                var pieces = (actualPlayer.color == Color.White) ? game.Board.WhitePieces : game.Board.BlackPieces;
                foreach (var pieceInfo in pieces)
                {
                    if (pieceInfo.piece is King king)
                    {
                        if (game.Board.Echec(king, pieceInfo.CaseLink))
                        {
                            IsGameOver = true;
                            Console.WriteLine("Checkmate!");
                        }
                    }
                }
                player++;
            }
            game.GameOver(actualPlayer);
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
            if (notation.Length != 2 ||
                notation[0] < 'a' || notation[0] > 'h' ||
                notation[1] < '1' || notation[1] > '8')
            {
                throw new ArgumentException("Invalid chess notation.");
            }

            int column = notation[0] - 'a';
            int row = 8 - (notation[1] - '0'); // Row conversion (1 -> 7, 2 -> 6, ..., 8 -> 0)

            return (column, row);
        }
    }
}
