
using System.Diagnostics;

namespace Chess
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        
        async void OnConnexionClicked(object sender, EventArgs e)
        {
            try
            {

            await Shell.Current.GoToAsync("//page/Login1");
            Debug.WriteLine("Connexion clicked");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" failed login {ex.Message}");
            }

        }

        async void OnInscriptionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/Register");
        }
        
        async void OnRulesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/RulesPage");
        }
        
        async void OnLeaderBoardClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/LeaderBoard");
        }

        async void OnInvitedPlayersCLicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/chessBoard");
        }
    }

}
