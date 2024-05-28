using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Interface pour le plateau de jeu
    /// </summary>
    internal interface IBoard
    {
        /// <summary>
        /// Initialiser le plateau de jeu
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        bool CanMovePiece(Piece? piece, Case? initial,Case? final);

        /// <summary>
        /// Vérifier si le mouvement est valide
        /// </summary>
        /// <param name="lcase"></param>
        /// <param name="final"></param>
        /// <returns></returns>
        bool IsMoveValid(List<Case?> lcase, Case? final);
        
        /// <summary>
        /// Vérifier si le roi est en échec
        /// </summary>
        /// <param name="king"></param>
        /// <param name="kingCase"></param>
        /// <returns></returns>
        bool Echec(King? king,Case? kingCase);

    }
}