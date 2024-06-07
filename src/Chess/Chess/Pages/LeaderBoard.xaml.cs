<<<<<<< HEAD
namespace Chess.Pages;
=======
>>>>>>> fe8b817ef431fca219f0b5fb28b2914e0f1e6835
using ChessLibrary;
using System.Collections.ObjectModel;
using System.Linq;

<<<<<<< HEAD
=======
namespace Chess.Pages;
>>>>>>> fe8b817ef431fca219f0b5fb28b2914e0f1e6835
public partial class LeaderBoard : ContentPage
{
    public LeaderBoard()
	{
        InitializeComponent();  // S'assurer que tous les éléments XAML sont chargés
        BindingContext = this;  // Assigner le BindingContext
        Ondisplay();  // Charger les données
    }

    async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//page/MainPage");
	}
    private Manager MyManager => (App.Current as App).MyManager;
    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();


    private void Ondisplay()
    {
        MyManager.LoadData();
        if (MyManager.Users != null)
        {
            var sortedUsers = MyManager.Users.OrderByDescending(u => u.Score).ToList();
            foreach (User user in sortedUsers)
            {
                Users.Add(user);
            }
        }
    }
}