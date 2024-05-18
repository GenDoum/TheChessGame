using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe abstraite pour les pièces
    /// </summary>
    
    public abstract class Piece
    {
        /// <summary>
        /// Couleur de la pièce
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Identifiant de la pièce
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Déplacement de la pièce
        /// </summary>
        public bool Moved { get; protected set; }
        
        /// <summary>
        /// Constructeur de la pièce
        /// </summary>
        /// <param name="color"></param>
        /// <param name="indentifiant"></param>
        protected Piece(Color color, int indentifiant)
        {
            Color = color;
            Id = indentifiant;

            if (color != Color.Black && color != Color.White)
            {
                throw new ArgumentException("Invalid color");
            }
        }
        
        /// <summary>
        /// Vérifier si la pièce peut bouger
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public abstract bool CanMove(int x, int y, int x2, int y2);
        
        /// <summary>
        /// Stocker tous les mouvements possibles de la pièce
        /// </summary>
        /// <param name="caseInitial"></param>
        /// <param name="chessboard"></param>
        /// <returns></returns>
        public abstract List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard);

        public virtual Piece Clone()
        {
            return (Piece)this.MemberwiseClone(); // Copie superficielle appropriée si aucune référence profonde n'est nécessaire
        }
    }
}