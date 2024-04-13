using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Chessboard : IRegles
    {
        /// <summary>
        /// Creation de la matrice pour stocké les cases 
        /// </summary>

        public Case[,] Board = new Case[8, 8];

        /// <summary>
        /// Implémente les cases dans le plateau.
        /// </summary>
        /// <param name="board"></param>
        public Chessboard(Case[,] board)
        {
            Board = board;

            int indentifiant = 1;
            for (int C = 0; C < 8; C++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if ((C == 0 && l == 0) || (C == 7 && l == 0))
                    {
                        //Rook rook = new Rook(Color.White, Board[C, l]);
                        //Board[C, l] = new Case(colonne[C], line[l], rook);
                        //List<Piece> list = new List<Piece>();
                        //list.Add(rook);
                        Board[C,l] = new Case(C, l,new Rook(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if ((C == 1 && l == 0) || (C == 6 && l == 0))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Knight(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if ((C == 2 && l == 0) || (C == 5 && l == 0))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Bishop(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if (C == 3 && l == 0)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Queen(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if (C == 4 && l == 0)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new King(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if (l == 1)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Pawn(Color.White, indentifiant));
                        indentifiant++;
                    }
                    else if ((C == 0 && l == 7) || (C == 7 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Rook(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else if ((C == 1 && l == 7) || (C == 6 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Knight(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else if ((C == 2 && l == 7) || (C == 5 && l == 7))
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Bishop(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else if (C == 3 && l == 7)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Queen(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else if (C == 4 && l == 7)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new King(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else if (l == 6)
                    {
                        Board[C, l] = new Case(colonne[C], line[l], new Pawn(Color.Black, indentifiant));
                        indentifiant++;
                    }
                    else
                    {
                        Board[C, l] = new Case(colonne[C], line[l], null);
                    }

                }
            }
        }

        /// <summary>
        /// Creation d
        /// </summary>            
        /// <param name="piece"></param>
        /// <param name="cases"></param>

        public void DeplacerPiece(Piece piece, Case depart , Case arrive)
        {
            arrive.Piece = piece;
            depart.Piece = null;
        }

        public bool IsCaseEmpty(Case @case)
        {
            if (@case.Piece == null) { 
                return false; }

           return true;
        }

        public bool IsMoveValid(List<Case> Lcase, Case Final)
        {
            foreach(var i in Lcase)
            {
                if (i == Final) { return true; }
            }
            return false;
        }



        public void MovePiece(Piece piece, Case Initial, Case Final)
        {
            List<Case> L = piece.PossibleMove(Initial, this);
            if (IsMoveValid(L, Final))
            {
                Initial.Piece = null;
                Final.Piece = piece;
            }
        }


        public User Turn()
        {
            throw new NotImplementedException();
        }
    }
}
