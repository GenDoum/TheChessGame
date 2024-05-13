using System;
using System.Linq.Expressions;
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
                if (player % 2 == 0)
                {
                    actualPlayer = player2;
                    Console.WriteLine("Player 2 turn");
                }
                else
                {
                    actualPlayer = player1;
                    Console.WriteLine("Player 1 turn");
                }
                try
                {
                    DisplayBoard(game.Board);
                    Console.WriteLine("Enter the column of the piece you want to move:");
                    int column = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the row of the piece you want to move:");
                    int row = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the column of the destination:");
                    int column2 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the row of the destination:");
                    int row2 = int.Parse(Console.ReadLine());
                    game.MovePiece(game.Board.Board[column, row], game.Board.Board[column2, row2], game.Board, actualPlayer);
                    DisplayBoard(game.Board);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    player -= 1;
                }
                var test = (actualPlayer.color == Color.White) ? game.Board.WhitePieces : game.Board.BlackPieces;
                foreach (var i in test)
                {
                    if (i.piece.GetType().Name == "King")
                    {
                        if (game.Board.Echec((King)i.piece, i.CaseLink))
                        {
                            IsGameOver = true;
                        }
                    }
                }
                player++;
            }
            game.GameOver(actualPlayer);

        }

        static void DisplayBoard(Chessboard chessboard)
        {
            Console.WriteLine("   0   1   2   3   4   5   6   7");
            Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            for (int column = 0; column < 8; column++)
            {
                Console.Write((column) + "|");
                for (int row = 0; row < 8; row++)
                {
                    Piece piece = null;
                    if (chessboard.Board[row, column] != null)
                    {
                        piece = chessboard.Board[row, column].Piece;
                        // rest of the code
                    }
                    if (piece != null)
                    {
                        string pieceSymbol = GetPieceSymbol(piece);
                        Console.Write(" ");
                        if (piece.Color != Color.White)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(pieceSymbol);
                            Console.ResetColor();
                            Console.Write(" |");

                        }
                        else
                        {
                            Console.Write(pieceSymbol + " |");
                        }
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
                Console.WriteLine((column));
                Console.WriteLine(" +---+---+---+---+---+---+---+---+");
            }
            Console.WriteLine("   0   1   2   3   4   5   6   7");
        }

        static string GetPieceSymbol(Piece piece)
        {
            switch (piece.GetType().Name)
            {
                case "Pawn": return "P";
                case "Rook": return "R";
                case "Knight": return "C";
                case "Bishop": return "B";
                case "Queen": return "Q";
                case "King": return "K";
                default: return "?";
            }
        }
    }
}