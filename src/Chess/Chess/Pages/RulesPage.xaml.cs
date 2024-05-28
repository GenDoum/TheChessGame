namespace Chess.Pages;

public partial class RulesPage : ContentPage
{
    public RulesPage()
    {
        InitializeComponent();
    }
    
    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}