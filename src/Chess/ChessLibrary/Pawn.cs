using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a pawn piece
    /// </summary>
    public class Pawn : Piece, IFirstMove.FirstMove
    {
        virtual public bool FirstMove { get; set; }
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Pawn(Color c, int id) : base(c, id)
        {
            this.FirstMove = true;
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
            {
                throw new InvalidOperationException("Invalid move for Pawn: destination out of bounds.");
            }

            int direction = (Color == Color.White) ? 1 : -1;

            if (x == x2)
            {
                if (y2 == y + direction || (Color == Color.White && y == 1 || Color == Color.Black && y == 6) && y2 == y + 2 * direction)
                {
                    return true;
                }
            }

            if (Math.Abs(x2 - x) == 1 && y2 == y + direction)
            {
                return true;
            }

            throw new InvalidOperationException("Invalid move for Pawn");
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case> result = new List<Case>();
            int direction = this.Color == Color.White ? 1 : -1; // Blanc vers le haut (+1), Noir vers le bas (-1)

            // Mouvements normaux (1 ou 2 cases)
            AddNormalMoves(caseInitial, chessboard, direction, result);

            // Capture diagonale (gauche et droite)
            AddDiagonalCaptures(caseInitial, chessboard, direction, result);

            return result;
        }

        private void AddNormalMoves(Case caseInitial, Chessboard chessboard, int direction, List<Case> result)
        {
            for (int i = 1; i <= (FirstMove ? 2 : 1); i++)
            {
                int newLine = caseInitial.Line + direction * i;
                int newColumn = caseInitial.Column;
                if (IsWithinBoard(newLine, newColumn, chessboard))
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];
                    if (potentialCase.IsCaseEmpty())
                    {
                        result.Add(potentialCase);
                    }
                    else
                    {
                        break; // Blocage par une pièce, stopper ici
                    }
                }
            }
        }

        private void AddDiagonalCaptures(Case caseInitial, Chessboard chessboard, int direction, List<Case> result)
        {
            int[] captureColumns = new int[] { caseInitial.Column - 1, caseInitial.Column + 1 };
            foreach (int col in captureColumns)
            {
                int newLine = caseInitial.Line + direction;
                if (IsWithinBoard(newLine, col, chessboard))
                {
                    Case potentialCase = chessboard.Board[col, newLine];
                    if (!potentialCase.IsCaseEmpty() && potentialCase.Piece.Color != this.Color)
                    {
                        result.Add(potentialCase);
                    }
                }
            }
        }

        private bool IsWithinBoard(int line, int column, Chessboard chessboard)
        {
            return line >= 0 && line < chessboard.Board.GetLength(1) && column >= 0 && column < chessboard.Board.GetLength(0);
        }
    }

}
