using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance;
using ChessLibrary;

namespace Chess.Pages;

public partial class Register : ContentPage
{

    public Manager MyManager => (App.Current as App)!.MyManager;

    public Register()
    {
        InitializeComponent();
    }

    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string pseudo = PseudoEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        foreach(User user in MyManager.Users!)
        {
            if(user.Pseudo == pseudo)
            {
                await DisplayAlert("Erreur", "Ce pseudo est déjà utilisé", "OK");
                return;
            }
        }

        if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
        }
        else if (password != confirmPassword)
        {
            await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
        }
        else
        {
            User newUser = new User(pseudo, password, ChessLibrary.Color.White, false, 0);
            MyManager.Users.Add(newUser);
            MyManager.SaveData();
            await DisplayAlert("Succès", "Inscription réussie", "OK");

            PseudoEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ConfirmPasswordEntry.Text = string.Empty;
            await Shell.Current.GoToAsync("//page/MainPage");
        }
        
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }


}