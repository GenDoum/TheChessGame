using System;
using System.Collections.Generic;
using System.Linq;
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
        public Rook(Color color, Case c) : base(color, c)
        {
        }
        
        /// <summary>
        /// Method that checks if the bishop can move to a specific case
        /// </summary>
        /// <param name="targetCase"></param>
        /// <returns></returns>
        public override bool CanMove(Case targetCase)
        {
            return true;
        }
        
        /// <summary>
        /// Method that checks if the bishop can kill another piece
        /// </summary>
        /// <param name="targetCase"></param>
        /// <returns></returns>
        public override bool CanKill(Case targetCase)
        {
            return true;
        }
    }
}
