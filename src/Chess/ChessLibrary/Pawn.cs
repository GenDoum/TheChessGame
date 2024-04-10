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
        public Pawn(Color c, Case ca) : base(c, ca)
        {
        }
        
        /// <summary>
        /// Method that checks if the king can move to a specific case
        /// </summary>
        /// <param name="targetCase"></param>
        /// <returns></returns>
        public override bool CanMove(Case targetCase)
        {
            return true;
        }
        
        /// <summary>
        /// Method that checks if the king can kill another piece
        /// </summary>
        /// <param name="targetCase"></param>
        /// <returns></returns>
        public override bool CanKill(Case targetCase)
        {
            return true;
        }
    }
}
