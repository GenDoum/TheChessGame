namespace ChessLibrary.Events
{
    /// <summary>
    /// Arguments for the game over notification event.
    /// </summary>
    public class GameOverNotifiedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the user who lost the game.
        /// </summary>
        public User? Loser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user who won the game.
        /// </summary>
        public User? Winner
        {
            get;
            set;
        }
    }
}
