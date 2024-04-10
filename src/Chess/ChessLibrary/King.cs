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
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public King(Color color, Case c) : base(color, c)
        {
        }
        
        /// <summary>
        /// Method that checks if the king can move to a specific case
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool canMove(Case c)
        {
            return true;
        }
        
        /// <summary>
        /// Method that checks if the king is eaten
        /// </summary>
        /// <returns></returns>
        public bool canCastle()
        {
            return true;
        }
    }
}
