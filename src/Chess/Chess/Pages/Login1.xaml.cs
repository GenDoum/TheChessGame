namespace Chess.Pages;

public partial class Login1 : ContentPage
{
    public Login1()
    {
        InitializeComponent();
    }

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}