using Chess.Pages;
using ChessLibrary;
using Persistance;
using Plugin.Maui.Audio;
using System.Diagnostics;

namespace Chess
{
    /// <summary>
    /// Represents the main page of the application.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// The current game instance.
        /// </summary>
        public Game game;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the connection button.
        /// Navigates to the login page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnConnexionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/Login1");
        }

        /// <summary>
        /// Handles the click event for the registration button.
        /// Navigates to the registration page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnInscriptionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/Register");
        }

        /// <summary>
        /// Handles the click event for the rules button.
        /// Navigates to the rules page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnRulesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/RulesPage");
        }

        /// <summary>
        /// Handles the click event for the leaderboard button.
        /// Navigates to the leaderboard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnLeaderBoardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/LeaderBoard");
        }

        /// <summary>
        /// Handles the click event for the invited players button.
        /// Navigates to the chess board page with invited players.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnInvitedPlayersCLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new chessBoard(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black)));  // Envoi une game en navigation pour ne pas en ajouter dans la persistance pour rien
        }
    }
}
