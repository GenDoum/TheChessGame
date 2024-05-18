using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Interface pour les règles du jeu
    /// </summary>
    internal interface IRules
    {
        /// <summary>
        /// Déplacer une pièce
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <param name="board"></param>
        /// <param name="actualPlayer"></param>
        void MovePiece(Case initial,Case final,Chessboard board,User actualPlayer);
        
        /// <summary>
        /// Vérifier si le mouvement est valide
        /// </summary>
        /// <param name="winner"></param>
        /// <returns></returns>
        bool GameOver(User winner);
        //appel a evenement
    }
}
