using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    /// <summary>
    /// Interface for the game rules.
    /// </summary>
    internal interface IRules
    {
        /// <summary>
        /// Moves a piece on the chessboard.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <param name="board">The chessboard on which the move is performed.</param>
        /// <param name="actualPlayer">The player making the move.</param>
        void MovePiece(Case? initial, Case? final, Chessboard board, User actualPlayer);

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        /// <param name="winner">The player who won the game.</param>
        /// <returns>True if the game is over; otherwise, false.</returns>
        bool GameOver(User winner);
    }
}
