using ChessLibrary;

namespace Chess.Pages;


public partial class LoginSecondPlayer : ContentPage
{

    public Manager MyManager => (App.Current as App).MyManager;

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
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
        }
        else
        {
            var existingUser = MyManager.Users.FirstOrDefault(u => u.Pseudo == entryPseudo);
            if (existingUser != null)
            {
                if (User.HashPassword(entryPassword) == existingUser.Password)
                {
                    MyManager.Games.First().Player2 = existingUser;

                    MyManager.Games.Add(new Game(MyManager.Games.First().Player1, MyManager.Games.First().Player2));

                    await Shell.Current.GoToAsync("//page/chessBoard");
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

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/Login1");
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}