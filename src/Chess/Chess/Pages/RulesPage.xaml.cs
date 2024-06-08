namespace Chess.Pages
{
    /// <summary>
    /// Represents the page that displays the rules of the chess game.
    /// </summary>
    public partial class RulesPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RulesPage"/> class.
        /// </summary>
        public RulesPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the back button.
        /// Navigates back to the main page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/MainPage");
        }
    }
}
