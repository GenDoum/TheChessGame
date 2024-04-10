using System;
using ChessLibrary;

class Program
{
    static void Main(string[] args)
    {
        Case[,] board = new Case[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = new Case("", 0, null);
            }
        }
        
        Chessboard chessboard = new Chessboard(board);

        // affiche échiquier
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j < 8; j++)
            {
                Console.Write(chessboard.Board[j, i].Piece?.GetType().Name ?? "Empty");
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}