namespace ChessLibrary.Events
{
    /// <summary>
    /// Arguments for the evolve notification event.
    /// </summary>
    public class EvolveNotifiedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the pawn that is evolving.
        /// </summary>
        public Pawn? Pawn { get; set; }

        /// <summary>
        /// Gets or sets the case where the evolution is taking place.
        /// </summary>
        public Case? Case { get; set; }
    }
}
