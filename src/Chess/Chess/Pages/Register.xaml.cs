using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance;
using testPersistance;

namespace Chess.Pages;

public partial class Register : ContentPage
{
    
    public Register()
    {
        InitializeComponent();
    }
    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        
        await Shell.Current.GoToAsync("//page/LogIn");
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
    
    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}