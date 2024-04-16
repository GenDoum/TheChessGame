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
    public class King : Piece
    {
        private bool canCastleRight;
        private bool canCastleLeft;
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="initialCase"></param>
        public King(Color color, int id) : base(color, id)
        {
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
            throw new NotImplementedException();
        }
    }
}