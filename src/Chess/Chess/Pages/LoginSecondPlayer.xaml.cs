using ChessLibrary;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the login page for the second player.
    /// </summary>
    public partial class LoginSecondPlayer : ContentPage
    {
        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        public Manager MyManager => (App.Current as App)!.MyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSecondPlayer"/> class.
        /// </summary>
        public LoginSecondPlayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the click event for the connection button.
        /// Authenticates the second player and navigates to the chess board page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnConnexionButtonClicked(object sender, EventArgs e)
        {
            string entryPseudo = UsernameEntry.Text;
            string entryPassword = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(entryPseudo) || string.IsNullOrWhiteSpace(entryPassword))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            }
            else
            {
                var existingUser = MyManager.Users!.FirstOrDefault(u => u.Pseudo == entryPseudo);
                if (existingUser != null)
                {
                    if (User.HashPassword(entryPassword) == existingUser.Password)
                    {

                        MyManager.Games.First().Player2 = existingUser; // Ajoute le player 2 à la nouvelle game crée dans Login1

                        UsernameEntry.Text = string.Empty;
                        PasswordEntry.Text = string.Empty;
                        await Shell.Current.GoToAsync("//page/chessBoard");
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Mot de passe ou Utilisateur incorrect", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "Mot de passe ou Utilisateur incorrect", "OK");
                }
            }
        }

        /// <summary>
        /// Handles the click event for the back button.
        /// Navigates back to the first login page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/Login1");
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
