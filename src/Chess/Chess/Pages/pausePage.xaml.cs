using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary;
using CommunityToolkit.Maui.Views;

namespace Chess.Pages;

public partial class pausePage : Popup
{
    Manager Mymanager = (App.Current as App).MyManager;

    public pausePage()
    {
        InitializeComponent();
    }

    private void OnContinueGame(object sender, EventArgs e)
    {
        Close();
    }

    private async void OnQuitGame(object sender, EventArgs e)
    {
        Mymanager.SaveData();
        await Shell.Current.GoToAsync("//MainPage");
        Close();
    }
}