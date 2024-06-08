using CommunityToolkit.Maui.Views;

namespace Chess.Pages;

public partial class endGame : Popup
{
	public endGame()
	{
		InitializeComponent();
	}

    public endGame(string winner)
    {
        InitializeComponent();
        WinnerLabel.Text = winner;
    }

    private async void OnQuitGame(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
        Close();
    }
}