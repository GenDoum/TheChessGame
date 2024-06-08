using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    /// <summary>
    /// Interface for the game board.
    /// </summary>
    internal interface IBoard
    {
        /// <summary>
        /// Initializes the game board.
        /// </summary>
        /// <param name="piece">The piece to be moved.</param>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <returns>True if the piece can be moved; otherwise, false.</returns>
        bool CanMovePiece(Piece? piece, Case? initial, Case? final);

        /// <summary>
        /// Checks if the move is valid.
        /// </summary>
        /// <param name="lcase">The list of cases to be checked.</param>
        /// <param name="final">The final case of the move.</param>
        /// <returns>True if the move is valid; otherwise, false.</returns>
        bool IsMoveValid(List<Case?> lcase, Case? final);

        /// <summary>
        /// Checks if the king is in check.
        /// </summary>
        /// <param name="king">The king piece to check.</param>
        /// <param name="kingCase">The case of the king.</param>
        /// <returns>True if the king is in check; otherwise, false.</returns>
        bool Echec(King? king, Case? kingCase);
    }
}
