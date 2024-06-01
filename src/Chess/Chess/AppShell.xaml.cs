using Chess.Pages;

namespace Chess
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("MainPageDetails", typeof(MainPage));
            Routing.RegisterRoute("chessBoardDetails", typeof(chessBoard));
            Routing.RegisterRoute("RegisterDetails", typeof(Register));
            Routing.RegisterRoute("LogInDetails", typeof(LogIn));
            Routing.RegisterRoute("pausePageDetails", typeof(pausePage));
            Routing.RegisterRoute("LoginSecondPlayerDetails", typeof(LoginSecondPlayer));
            Routing.RegisterRoute("LeaderBoardDetails", typeof(LeaderBoard));
            Routing.RegisterRoute("RulesPageDetails", typeof(RulesPage));

        }
    }
}
