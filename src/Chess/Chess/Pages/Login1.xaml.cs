using ChessLibrary;
using Persistance;
using Plugin.Maui.Audio;
using System.Linq;
using Color = ChessLibrary.Color;

namespace Chess.Pages;
public partial class Login1 : ContentPage
{

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
            return;
        }

        await Shell.Current.GoToAsync("//page/LoginSecondPlayer");
        
    }


    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


}