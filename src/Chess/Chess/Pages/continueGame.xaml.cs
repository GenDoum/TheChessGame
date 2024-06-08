using CommunityToolkit.Maui.Views;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the popup that appears to continue or restart the game.
    /// </summary>
    public partial class continueGame : Popup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="continueGame"/> class.
        /// </summary>
        public continueGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event to continue the game.
        /// Closes the popup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnContinueGame(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the event to restart the game.
        /// Closes the popup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void OnRestartGame(object sender, EventArgs e)
        {
            Close();
        }
    }
}
