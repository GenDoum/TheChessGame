// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection.Metadata;
using ChessLibrary;

class Program
{
    // static void Main(string[] args)
    // {
    //
    //
    //     Case[,] Board = new Case[8, 8];
    //     var chessboard = new Chessboard(Board, 8, 8);
    //     List<Case> Test = Board[4, 4].Piece.PossibleMoves(Board[4, 4], chessboard);
    //     foreach (var teste in Test)
    //     {
    //         teste.afficher();
    //     }
    //
    //     // Initialisation de l'échiquier
    //     /* Chessboard chessboard = new Chessboard(new Case[8, 8]);
    //      PrintChessboard(chessboard);
    //      while (Console.ReadLine() != "exit")
    //      {
    //          Console.WriteLine("Déplacement de la piece");
    //         // Chessboard.DeplacerPiece(Knight(Color.Black, Chessboard[1, 4]), Chessboard[1, 4], Chessboard[3, 5]);
    //      }*/
    //     testJoueurConsole();
    // }
    //
    // /*    static void PrintChessboard(Chessboard chessboard)
    //     {
    //         for (int i = 0; i < 8; i++)
    //         {
    //             for (int j = 0; j < 8; j++)
    //             {
    //                 Case c = chessboard.Board[i, j];
    //                 if (c.Piece == null)
    //                 {
    //                     Console.Write(". ");
    //                 }
    //                 else
    //                 {
    //                     char pieceSymbol = GetPieceSymbol(c.Piece);
    //                     if (c.Piece.Color == Color.Black)
    //                     {
    //                         pieceSymbol = char.ToLower(pieceSymbol);
    //                     }
    //                     Console.Write($"{pieceSymbol} ");
    //                 }
    //             }
    //             Console.WriteLine();
    //         }
    //     }*/
    //
    // static void PrintChessboard(Chessboard chessboard)
    // {
    //     // Inverse l'ordre de parcours pour les lignes
    //     for (int i = 7; i >= 0; i--)
    //     {
    //         for (int j = 0; j < 8; j++)
    //         {
    //             // Alterne la couleur de fond pour chaque case
    //             Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.DarkGray;
    //
    //             Case c = chessboard.Board[j, i]; // Ajustement de l'accès à Board pour inverser les colonnes si nécessaire
    //             if (c.Piece == null)
    //             {
    //                 Console.Write("  "); // Deux espaces pour une case vide
    //             }
    //             else
    //             {
    //                 char pieceSymbol = GetPieceSymbol(c.Piece);
    //                 // Assurez-vous que la couleur du texte diffère de celle du fond
    //                 Console.ForegroundColor = c.Piece.Color == Color.Black ? ConsoleColor.Black : ConsoleColor.White;
    //                 Console.Write($"{pieceSymbol} ");
    //             }
    //             Console.ResetColor(); // Réinitialise la couleur pour les autres éléments
    //         }
    //         Console.WriteLine();
    //     }
    // }
    //
    //
    // static char GetPieceSymbol(Piece piece)
    // {
    //     switch (piece)
    //     {
    //         case King _: return 'K';
    //         case Queen _: return 'Q';
    //         case Rook _: return 'R';
    //         case Bishop _: return 'B';
    //         case Knight _: return 'N';
    //         case Pawn _: return 'P';
    //         default: return '?';
    //     }
    // }
    //
    //
    // static void testJoueurConsole()
    // {
    //     User u = new User("Genkyy", "lolilol42", Color.White);
    //     u.isPasswdConsole();
    // }
    
    //hello world 
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

