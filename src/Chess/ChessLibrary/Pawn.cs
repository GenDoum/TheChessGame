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
    }
}
