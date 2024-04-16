using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a pawn piece
    /// </summary>
    public class Pawn : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Pawn(Color c, int id) : base(c, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x2 < 1 || x2 > 8 || y2 < 1 || y2 > 8)
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
            throw new NotImplementedException();
        }
    }
}
