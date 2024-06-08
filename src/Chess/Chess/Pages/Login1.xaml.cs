using ChessLibrary;
using Persistance;
using Plugin.Maui.Audio;
using System.Linq;
using Color = ChessLibrary.Color;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the first login page for the chess application.
    /// </summary>
    public partial class Login1 : ContentPage
    {
        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        public Manager MyManager => (App.Current as App)!.MyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Login1"/> class.
        /// </summary>
        public Login1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the login button.
        /// Authenticates the user and navigates to the appropriate page based on the user type.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string entryPseudo = UsernameEntry.Text;
            string entryPassword = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(entryPseudo) || string.IsNullOrWhiteSpace(entryPassword))
            {
                DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            }
            else
            {
                User existingUser = MyManager.Users.FirstOrDefault(u => u.Pseudo == entryPseudo);

                if (existingUser != null)
                {
                    if (User.HashPassword(entryPassword) == existingUser.Password)
                    {
                        if (checkInvitedPlayer.IsChecked)
                        {
                            // Player 1 is logged in but player 2 is not
                            UsernameEntry.Text = string.Empty;
                            PasswordEntry.Text = string.Empty;

                            await Navigation.PushAsync(new chessBoard(existingUser, new User(ChessLibrary.Color.Black)));
                        }
                        else
                        {
                            UsernameEntry.Text = string.Empty;
                            PasswordEntry.Text = string.Empty;
                            MyManager.Games.First().Player1 = existingUser;
                            Shell.Current.GoToAsync("//page/LoginSecondPlayer");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Mot de passe incorrect", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Utilisateur introuvable", "OK");
                }
            }
        }

        /// <summary>
        /// Handles the click event for the cancel button.
        /// Navigates back to the main page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/MainPage");
        }
    }
}
