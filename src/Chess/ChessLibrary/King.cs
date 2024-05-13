using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a king piece
    /// </summary>
    public class King : Piece , IFirstMove.FirstMove
    {
        public bool FirstMove { get; set; }
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="initialCase"></param>
        public King(Color color, int id) : base(color, id)
        {
            this.FirstMove = true;
        }


        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (Math.Abs(x - x2) <= 1 && Math.Abs(y - y2) <= 1)
            {
                if (x2 < 1 || x2 > 8 || y2 < 1 || y2 > 8)
                {
                    throw new InvalidOperationException("Invalid move for King: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidOperationException("Invalid move for King");
        }


        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }
            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right ,Top Left, Top Right, Bot Left,Bot Right
            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial.Column + (colInc);
                int newLine = caseInitial.Line + (lineInc);
                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];

                    if (potentialCase.IsCaseEmpty() || chessboard.Echec(this, potentialCase))
                    {
                        result.Add(potentialCase);
                    }
                    else
                    {
                        if (potentialCase.Piece != null || potentialCase.Piece.Color != this.Color && chessboard.Echec(this, potentialCase))
                        {
                            result.Add(potentialCase);
                        }
                        break;
                    }
                }
                else
                {
                    break;
                }

            }
            return result;
        }
    }
}