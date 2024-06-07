namespace ChessLibrary
{
    /// <summary>
    /// Structure to facilitate the management of chess pieces.
    /// </summary>
    public struct CoPieces
    {
        /// <summary>
        /// Gets or sets the case associated with the piece.
        /// </summary>
        public Case? CaseLink { get; set; }

        /// <summary>
        /// Gets or sets the piece.
        /// </summary>
        public Piece? piece { get; set; }
    }
}
