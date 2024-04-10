using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Chessboard
    {
        /// <summary>
        /// Creation de la matrice pour stocké les cases 
        /// </summary>

        private Case[,] Board = new Case[8, 8];

        /// <summary>
        /// Implémente les cases dans le plateau.
        /// </summary>
        /// <param name="board"></param>
        public Chessboard(Case[,] board)
        {
            Board = board;

            string[] colonne = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            int[] line = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int C = 0; C < colonne.Length; C++)
            {
                for (int l = 0; l < line.Length; l++)
                {
                    if ((C == 0 && l == 0) || (C == 7 && l == 0))
                    {
                        Rook rook = new Rook(Color.White, Board[C, l]);
                        Board[C, l] = new Case(colonne[C], line[l], rook);
                        List<Piece> list = new List<Piece>();
                        list.Add(rook);
                    }
                    else if ((C == 1 && l == 0) || (C == 6 && l == 0))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Knight(Color.White, Board[C, l]));
                    }
                    else if ((C == 2 && l == 0) || (C == 5 && l == 0))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Bishop(Color.White, Board[C, l]));
                    }
                    else if (C == 3 && l == 0)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Queen(Color.White, Board[C, l]));
                    }
                    else if (C == 4 && l == 0)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new King(Color.White, Board[C, l]));
                    }
                    else if (l == 1)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Pawn(Color.White, Board[C, l]));
                    }
                    else if ((C == 0 && l == 7) || (C == 7 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Rook(Color.Black, Board[C, l]));
                    }
                    else if ((C == 1 && l == 7) || (C == 6 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Knight(Color.Black, Board[C, l]));
                    }
                    else if ((C == 2 && l == 7) || (C == 5 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Bishop(Color.Black, Board[C, l]));
                    }
                    else if (C == 3 && l == 7)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Queen(Color.Black, Board[C, l]));
                    }
                    else if (C == 4 && l == 7)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new King(Color.Black, Board[C, l]));
                    }
                    else if (l == 6)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Pawn(Color.Black, Board[C, l]));
                    }
                    else
                    {
                        Board[C, l] = new Case(colonne[C], line[l], null);
                    }

                }
            }

            //King KingW = new King(Color.White, Board[4 , 0]);
            //Board[4, 0].piece = KingW;
            //Queen QueenW = new Queen(Color.White, new Case("D", 1));
            //Rook RookLW = new Rook(Color.White, new Case("A", 1));
            //Rook RookRW = new Rook(Color.White, new Case("H", 1));
            //Knight KnightLW = new Knight(Color.White, new Case("B", 1));
            //Knight KnightRW = new Knight(Color.White, new Case("G", 1));
            //Bishop BishopLW = new Bishop(Color.White, new Case("C", 1));
            //Bishop BishopRW = new Bishop(Color.White, new Case("F", 1));
            //Pawn PawnAW = new Pawn(Color.White, new Case("A", 2));
            //Pawn PawnBW = new Pawn(Color.White, new Case("B", 2));
            //Pawn PawnCW = new Pawn(Color.White, new Case("C", 2));
            //Pawn PawnDW = new Pawn(Color.White, new Case("D", 2));
            //Pawn PawnEW = new Pawn(Color.White, new Case("E", 2));
            //Pawn PawnFW = new Pawn(Color.White, new Case("F", 2));
            //Pawn PawnGW = new Pawn(Color.White, new Case("G", 2));
            //Pawn PawnHW = new Pawn(Color.White, new Case("H", 2));

            //King KingB = new King(Color.Black, new Case("D", 8));
            //Queen QueenB = new Queen(Color.Black, new Case("E", 8));
            //Rook RookLB = new Rook(Color.Black, new Case("A", 8));
            //Rook RookRB = new Rook(Color.Black, new Case("H", 8));
            //Knight KnightLB = new Knight(Color.Black, new Case("B", 8));
            //Knight KnightRB = new Knight(Color.Black, new Case("G", 8));
            //Bishop BishopLB = new Bishop(Color.Black, new Case("C", 8));
            //Bishop BishopRB = new Bishop(Color.Black, new Case("F", 8));
            //Pawn PawnAB = new Pawn(Color.Black, new Case("A", 7));
            //Pawn PawnBB = new Pawn(Color.Black, new Case("B", 7));
            //Pawn PawnCB = new Pawn(Color.Black, new Case("C", 7));
            //Pawn PawnDB = new Pawn(Color.Black, new Case("D", 7));
            //Pawn PawnEB = new Pawn(Color.Black, new Case("E", 7));
            //Pawn PawnFB = new Pawn(Color.Black, new Case("F", 7));
            //Pawn PawnGB = new Pawn(Color.Black, new Case("G", 7));
            //Pawn PawnHB = new Pawn(Color.Black, new Case("H", 7));
        }

        /// <summary>
        /// Creation d
        /// </summary>            
        /// <param name="piece"></param>
        /// <param name="cases"></param>

        public void DeplacerPiece(Piece piece, Case cases)
        {
            piece.Case = cases;
        }


    }
}
