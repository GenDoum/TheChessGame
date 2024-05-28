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
            await Shell.Current.GoToAsync("//page/LogIn");
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
    }

}
