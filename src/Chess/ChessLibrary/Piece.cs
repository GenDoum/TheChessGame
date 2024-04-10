using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a piece
    /// </summary>
    
    public class Piece
    {
        /// <summary>
        /// Property that represents the color of the piece
        /// </summary>
        public Color Color
        {
            private set;
            get;
        }
        
        /// <summary>
        /// Property that represents the case of the piece
        /// </summary>
        public Case CurrentCase 
        { 
            private set;
            get; 
        }
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Piece(Color color, Case c)
        {
            Color = color;
            CurrentCase = c;
        }
        
        /// <summary>
        /// Method that checks if the piece can move to a specific case
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool canMove(Case targetCase)
        {
            return true;
        }
        
        /// <summary>
        /// Method that checks if the piece can kill another piece
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool canKill(Case targetCase)
        {
            return true;
        }
        
        /// <summary>
        /// Method that checks if the piece is eaten
        /// </summary>
        /// <returns></returns>
        public bool isKilled()
        {
            return false;
        }
        
    }
}
