using CommunityToolkit.Maui.Views;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the popup that appears at the end of the game.
    /// </summary>
    public partial class endGame : Popup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="endGame"/> class.
        /// </summary>
        public endGame()
        {
            InitializeComponent();
        }

        public endGame(string name)
        {
            InitializeComponent();
            WinnerLabel.Text = $"The winner is {name}";
        }


        /// <summary>
        /// Handles the event to quit the game.
        /// Navigates to the main page and closes the popup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private async void OnQuitGame(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
            Close();
        }
    }
}