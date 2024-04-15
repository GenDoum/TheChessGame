using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a knight piece
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Knight(Color c, int id) : base(c, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }

            List<Case> result = new List<Case>();

            // Toutes les positions possibles que le cavalier peut prendre en forme de "L"
            int[,] offsets = new int[,]
            {
        { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 },
        { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }
            };

            // Vérifiez chacun des déplacements possibles
            for (int i = 0; i < 8; i++)
            {
                int newColumn = caseInitial.Column + offsets[i, 0];
                int newLine = caseInitial.Line + offsets[i, 1];

                // Vérifiez si la nouvelle position est sur l'échiquier
                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];
                    // Ajouter la case si elle est vide ou contient une pièce adverse
                    if (potentialCase.IsCaseEmpty() || potentialCase.Piece.Color != this.Color)
                    {
                        result.Add(potentialCase);
                    }
                }
            }

            return result;
        }
    }
}

