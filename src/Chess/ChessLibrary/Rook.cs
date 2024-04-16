using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a Rook piece
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Rook(Color color, int id) : base(color, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x == x2 || y == y2)
            {
                return true;
            }

            throw new InvalidOperationException("Invalid move for Rook");
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            throw new NotImplementedException();
        }
    }
}
