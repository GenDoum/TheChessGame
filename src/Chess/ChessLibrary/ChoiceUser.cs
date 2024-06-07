namespace ChessLibrary
{
    /// <summary>
    /// Enumeration for the user's choice of piece during pawn promotion.
    /// </summary>
    public enum ChoiceUser
    {
        /// <summary>
        /// Promotes the pawn to a Queen.
        /// </summary>
        Queen = 1,

        /// <summary>
        /// Promotes the pawn to a Rook.
        /// </summary>
        Rook = 2,

        /// <summary>
        /// Promotes the pawn to a Bishop.
        /// </summary>
        Bishop = 3,

        /// <summary>
        /// Promotes the pawn to a Knight.
        /// </summary>
        Knight = 4
    }
}
