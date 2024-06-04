using Chess.Pages;
using ChessLibrary;
using Persistance;
using System.Diagnostics;

namespace Chess
{
    public partial class MainPage : ContentPage
    {
        UserManager userManager = new UserManager();

        public Game game;

        public MainPage()
        {
            InitializeComponent();
            game = new Game(userManager);

        }
        
        async void OnConnexionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login1(this.game));
        }

        async void OnInscriptionClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register(this.game));
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
