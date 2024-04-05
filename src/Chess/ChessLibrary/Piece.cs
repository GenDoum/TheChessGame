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
        public Color Color { get; set; }
        
        /// <summary>
        /// Property that represents the case of the piece
        /// </summary>
        public Case Case { get; set; }
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Piece(Color color, Case c)
        {
            Color = color;
            Case = c;
        }
        
        /// <summary>
        /// Method that checks if the piece can move to a specific case
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool canMove(Case c)
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
