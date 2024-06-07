using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Maui.Views;

namespace Chess.Pages;

public partial class pausePage : Popup
{
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
        await Shell.Current.GoToAsync("//MainPage");
        Close();
    }
}