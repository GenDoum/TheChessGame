using ChessLibrary;
using Persistance;
using System.Linq;

namespace Chess.Pages;
public partial class Login1 : ContentPage
{
    Game game;

    public Login1()
    {
        InitializeComponent();
    }

    public Login1(Game game)
    {
        InitializeComponent();
        BindingContext = game;
        this.game = game;
    }

    void OnLoginButtonClicked(object sender, EventArgs e)
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
                    if(checkInvitedPlayer.IsChecked)
                    {
                        game.Player1 = existingUser;
                        existingUser.IsConnected = true;    
                        Navigation.PushAsync(new chessBoard());
                    }
                    else
                    {
                        Navigation.PushAsync(new LoginSecondPlayer(game));
                    }
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


    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        game.Player2.IsConnected = false;
        await Navigation.PopAsync();
    }


}