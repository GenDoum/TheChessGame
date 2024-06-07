using CommunityToolkit.Maui.Views;

namespace Chess.Pages;

public partial class continueGame : Popup
{
	public continueGame()
	{
		InitializeComponent();
	}

    private void OnContinueGame(object sender, EventArgs e)
    {
        Close();
    }

    public void OnRestartGame(object sender, EventArgs e)
    {
        Close();
    }
}