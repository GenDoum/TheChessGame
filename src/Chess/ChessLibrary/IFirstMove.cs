using System;

namespace ChessLibrary
{
    /// <summary>
    /// Interface for tracking the first move.
    /// </summary>
    public interface IFirstMove
    {
        /// <summary>
        /// Gets or sets a value indicating whether the piece has made its first move.
        /// </summary>
        bool FirstMove { get; set; }
    }
}
