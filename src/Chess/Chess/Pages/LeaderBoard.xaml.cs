namespace Chess.Pages;

public partial class LeaderBoard : ContentPage
{
	public LeaderBoard()
	{
		InitializeComponent();
	}
	
	async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//page/MainPage");
	}
}