namespace Chess.contentView;

public partial class connexionModuleButtons : ContentView
{
    public connexionModuleButtons()
    {
        InitializeComponent();
    }
    
    async void OnConnexionButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/LoginSecondPlayer");
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}