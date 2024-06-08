using Chess.Pages;

namespace Chess
{
    /// <summary>
    /// Represents the application shell which defines the navigation structure.
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppShell"/> class.
        /// </summary>
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("MainPageDetails", typeof(MainPage));
            Routing.RegisterRoute("chessBoardDetails", typeof(chessBoard));
            Routing.RegisterRoute("RegisterDetails", typeof(Register));
            Routing.RegisterRoute("LogInDetails", typeof(Login1));
            Routing.RegisterRoute("pausePageDetails", typeof(pausePage));
            Routing.RegisterRoute("LoginSecondPlayerDetails", typeof(LoginSecondPlayer));
            Routing.RegisterRoute("LeaderBoardDetails", typeof(LeaderBoard));
            Routing.RegisterRoute("RulesPageDetails", typeof(RulesPage));
        }
    }
}
