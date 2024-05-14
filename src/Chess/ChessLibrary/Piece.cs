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

        public int id { get; private set; }
            

        
        /// <summary>
        /// Property that represents if the piece has moved
        /// </summary>
        public bool Moved { get; protected set; }
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Piece(Color color, int indentifiant)
        {
            Color = color;
            id = indentifiant;

            if (color != Color.Black && color != Color.White)
            {
                throw new ArgumentException("Invalid color");
            }
        }
        
        /// <summary>
        /// Method that checks if the piece can move
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public abstract bool CanMove(int x, int y, int x2, int y2);

        public abstract List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard);
        
        // Méthode de clonage par défaut
        public virtual Piece Clone()
        {
            return (Piece)this.MemberwiseClone(); // Copie superficielle appropriée si aucune référence profonde n'est nécessaire
        }
    }
}