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
                if (x2 < 1 || x2 > 8 || y2 < 1 || y2 > 8)
                {
                    throw new InvalidOperationException("Invalid move for Knight: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidOperationException("Invalid move for Knight");
        }


        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            throw new NotImplementedException();
        }
    }
}
