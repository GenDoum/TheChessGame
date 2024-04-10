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
        /// <param name="initialCase"></param>
        public King(Color color, Case initialCase) : base(color, initialCase)
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
        /// Method that checks if the king can perform castling
        /// </summary>
        /// <returns></returns>
        public bool CanCastle()
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
