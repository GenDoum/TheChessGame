using ChessLibrary;

namespace Chess.Pages;


public partial class LoginSecondPlayer : ContentPage
{
    Game game;

    public LoginSecondPlayer()
	{
		InitializeComponent();
	}
	public LoginSecondPlayer(Game game)
    {
        InitializeComponent();
        BindingContext = game;
        this.game = game;
    }

    async void OnConnexionButtonClicked(object sender, EventArgs e)
	{
        string entryPseudo = UsernameEntry.Text;
        string entryPassword = PasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(entryPseudo) || string.IsNullOrWhiteSpace(entryPassword))
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
        }
        else
        {
            // Check if the user with the  pseeudo exists
            var existingUser = game.Users.Find(u => u.Pseudo == entryPseudo);
            if (existingUser != null)
            {
                // Verify if the password is correct
                if (User.HashPassword(entryPassword) == existingUser.Password)
                {
                        game.Player2 = existingUser;
                        existingUser.IsConnected = true;
                        Navigation.PushAsync(new chessBoard(game));
                }
                else
                {
                    DisplayAlert("Erreur", "Pseudo ou Mot de passe incorrect", "OK");
                }
            }
            else
            {
                DisplayAlert("Erreur", "Utilisateur introuvable", "OK");
            }
        }
    }

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        game.Player2.IsConnected = false;
        await Navigation.PopAsync();
    }
}