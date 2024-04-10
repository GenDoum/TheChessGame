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
        private Case[,] board = new Case[8, 8];

        /// <summary>
        /// Implémente les cases dans le plateau.
        /// </summary>
        /// <param name="board"></param>
        public Chessboard(Case[,] board)
        {
            string[] colonne = ["A", "B", "C", "D", "E", "F", "G", "H"];
            int[] line = [1, 2, 3, 4, 5, 6, 7, 8];
            for (int C = 0; C < colonne.Length; C++)
            {
                for (int l = 0; l < line.Length; l++)
                {

                    board[C, l] = new Case(colonne[C], line[l]);

                }
            }
        }


        /// <summary>
        /// Creation d
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="cases"></param>

        public void PlacerPiece(Piece piece, Case cases)
        {
            piece.case = cases;
                for ()
                {
                    for ()
                    { 
                    }
                }
        }

            /// <summary>
            /// Creation d
            /// </summary>            
            /// <param name="piece"></param>
            /// <param name="cases"></param>

            public void DeplacerPiece(Piece piece, Case cases)
            {
                piece.case = cases;
                
            
            } 
        }
    } 
}