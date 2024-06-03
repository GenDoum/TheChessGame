using ChessLibrary;
using Persistance;
using System.Diagnostics;
using Windows.Media.Playback;

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
            foreach(User u in game.Users)
            {
                Console.WriteLine(u.Pseudo);
            }
        }
        
        async void OnConnexionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//page/Login1");
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
