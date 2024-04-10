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
    
    public abstract class Piece
    {
        /// <summary>
        /// Property that represents the color of the piece
        /// </summary>
        public Color Color { get; private set; }
        
        /// <summary>
        /// Property that represents the case of the piece
        /// </summary>
        public Case CurrentCase { get; private set; }
        
        /// <summary>
        /// Property that represents if the piece has moved
        /// </summary>
        public bool Moved { get; protected set; }
        
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
        /// Method that returns the possible moves of the piece
        /// </summary>
        /// <returns></returns>
        public abstract List<Case> GetPossibleMoves();
        
        /// <summary>
        /// Method that checks if the piece can move to a specific case
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public abstract void CanMove(Case targetCase);

        
        /// <summary>
        /// Method that checks if the piece can kill another piece
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public abstract bool CanKill(Case targetCase);
        
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