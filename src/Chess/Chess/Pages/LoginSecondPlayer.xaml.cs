namespace Chess.Pages;

public partial class LoginSecondPlayer : ContentPage
{
	public LoginSecondPlayer()
	{
		InitializeComponent();
	}
	
	async void OnConnexionButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//page/chessBoard");
	}
    
	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//page/MainPage");
	}
}