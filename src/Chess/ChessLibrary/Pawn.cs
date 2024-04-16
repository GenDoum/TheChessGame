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
            if (x == x2 && y2 == y + 1)
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
