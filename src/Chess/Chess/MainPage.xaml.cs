using Chess.Pages;
using ChessLibrary;
using Persistance;
using Plugin.Maui.Audio;
using System.Diagnostics;

namespace Chess
{
    public partial class MainPage : ContentPage
    {

        public Game game;

        public MainPage()
        {
            InitializeComponent();

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
            await Navigation.PushAsync(new chessBoard(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black)));
        }
    }

}