using CommunityToolkit.Maui.Views;

namespace Chess.Pages;

public partial class endGame : Popup
{
	public endGame()
	{
		InitializeComponent();
	}

    private async void OnQuitGame(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
        Close();
    }
}