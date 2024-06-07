using ChessLibrary;
using Persistance;
using Plugin.Maui.Audio;
using System.Linq;
using Color = ChessLibrary.Color;

namespace Chess.Pages;
public partial class Login1 : ContentPage
{
    public Manager MyMmanager => (App.Current as App).MyManager;


    public Login1()
    {
        InitializeComponent();
    }

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
            User existingUser = MyMmanager.Users.FirstOrDefault(u => u.Pseudo == entryPseudo);

            if (existingUser != null)
            {
                if (User.HashPassword(entryPassword) == existingUser.Password)
                {
                    if (checkInvitedPlayer.IsChecked)
                    {
                        // Le joueur 1 est connecté mais pas le joueur 2
                        Shell.Current.GoToAsync("//page/chessBoard");
                    }
                    else
                    {
                        MyMmanager.Games.First().Player1 = existingUser;
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


    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


}