namespace Chess.Pages
{
    using ChessLibrary;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents the leaderboard page that displays the users sorted by score.
    /// </summary>
    public partial class LeaderBoard : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderBoard"/> class.
        /// </summary>
        public LeaderBoard()
        {
            InitializeComponent();  // Ensure all XAML elements are loaded
            BindingContext = this;  // Assign the BindingContext
            Ondisplay();  // Load data
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

        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        private Manager MyManager => (App.Current as App)!.MyManager;

        /// <summary>
        /// Gets the collection of users to be displayed on the leaderboard.
        /// </summary>
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        /// <summary>
        /// Loads and displays the users sorted by score.
        /// </summary>
        private void Ondisplay()
        {
            MyManager.LoadData();
            if (MyManager.Users != null)
            {
                var sortedUsers = MyManager.Users.OrderByDescending(u => u.Score).ToList();
                foreach (User user in sortedUsers)
                {
                    Users.Add(user);
                }
            }
        }
    }
}
