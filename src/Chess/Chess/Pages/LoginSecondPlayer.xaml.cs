using ChessLibrary;

namespace Chess.Pages;


public partial class LoginSecondPlayer : ContentPage
{

    public LoginSecondPlayer()
	{
		InitializeComponent();
	}

    async void OnConnexionButtonClicked(object sender, EventArgs e)
	{
        string entryPseudo = UsernameEntry.Text;
        string entryPassword = PasswordEntry.Text;
        if (string.IsNullOrWhiteSpace(entryPseudo) || string.IsNullOrWhiteSpace(entryPassword))
        {
            DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }
        
        await Shell.Current.GoToAsync("//page/chessBoard");

    }

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        await Navigation.PopAsync();
    }
}