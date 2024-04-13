using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a queen piece
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Queen(Color color, int id) : base(color, id)
        {
        }
        
        public override List<Case> GetPossibleMoves()
        {
            List<Case> possibleMoves = new List<Case>();
            return possibleMoves;
        }
        
        /// <summary>
        /// Method that checks if the king can move to a specific case
        /// </summary>
        /// <param name="targetCase"></param>
        /// <returns></returns>
        public override void CanMove(Case targetCase)
        {
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