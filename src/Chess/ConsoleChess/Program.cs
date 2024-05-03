using System;
using ChessLibrary;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Case[,] board = new Case[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Case(i, j, null);
                }
            }

            Chessboard chessboard = new Chessboard(board);

            while (true)
            {
                DisplayBoard(chessboard);
                Console.WriteLine("Enter the coordinates of the piece you want to move:");
                Console.Write("X: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Y: ");
                int y = int.Parse(Console.ReadLine());

                Case initialCase = chessboard.Board[x, y];
                if (initialCase.Piece == null)
                {
                    Console.WriteLine("There is no piece at this position.");
                    continue;
                }

            }

            static void DisplayBoard(Chessboard chessboard)
            {
                Console.WriteLine("   a   b   c   d   e   f   g   h");
                Console.WriteLine(" +---+---+---+---+---+---+---+---+");
                for (int i = 0; i < 8; i++)
                {
                    Console.Write((i + 1) + "|");
                    for (int j = 0; j < 8; j++)
                    {
                        Piece piece = chessboard.Board[j, i].Piece;
                        if (piece != null)
                        {
                            string pieceSymbol = GetPieceSymbol(piece);
                            Console.Write(" " + pieceSymbol + " |");
                        }
                        else
                        {
                            Console.Write("   |");
                        }
                    }

                    Console.WriteLine((i + 1));
                    Console.WriteLine(" +---+---+---+---+---+---+---+---+");
                }

                Console.WriteLine("   a   b   c   d   e   f   g   h");
            }

            static string GetPieceSymbol(Piece piece)
            {
                switch (piece.GetType().Name)
                {
                    case "Pawn": return "P";
                    case "Rook": return "R";
                    case "Knight": return "N";
                    case "Bishop": return "B";
                    case "Queen": return "Q";
                    case "King": return "K";
                    default: return "?";
                }
            }
        }
    }
}