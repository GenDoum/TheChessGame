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
            if ((Math.Abs(x - x2) == 2 && Math.Abs(y - y2) == 1) || (Math.Abs(x - x2) == 1 && Math.Abs(y - y2) == 2))
            {
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                {
                    throw new InvalidOperationException("Invalid move for Knight: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidOperationException("Invalid move for Knight");
        }


        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }

            List<Case> result = new List<Case>();

            int[,] offsets = new int[,]
            {
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 },
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }
            };

            for (int i = 0; i < 8; i++)
            {
                int newColumn = caseInitial.Column + offsets[i, 0];
                int newLine = caseInitial.Line + offsets[i, 1];

                if (IsWithinBoardBoundaries(newColumn, newLine) && canMove(caseInitial.Column, caseInitial.Line, newColumn, newLine))
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];
                    AddPotentialMove(result, potentialCase);
                }
            }

            return result;
        }

        static bool IsWithinBoardBoundaries(int column, int line)
        {
            return column >= 0 && column < 8 && line >= 0 && line < 8;
        }

        private void AddPotentialMove(List<Case> result, Case potentialCase)
        {
            if (potentialCase.IsCaseEmpty() || potentialCase.Piece.Color != this.Color)
            {
                result.Add(potentialCase);
            }
        }
    }
}

