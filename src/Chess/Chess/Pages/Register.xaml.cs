using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance;
using ChessLibrary;
using testPersistance;

namespace Chess.Pages;

public partial class Register : ContentPage
{
    private UserManager userManager = new UserManager();
    
    public Register()
    {
        InitializeComponent();
    }
    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        var pseudo = pseudoEntry.Text;
        var password = passwordEntry.Text;
        var confirmPassword = confirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(pseudo) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas.", "OK");
            return;
        }

        var users = userManager.ReadUsers();
        if (users.Find(u => u.Pseudo == pseudo) != null)
        {
            await DisplayAlert("Erreur", "Ce pseudo est déjà utilisé.", "OK");
            return;
        }

        var newUser = new User(pseudo, password, ChessLibrary.Color.White, false, 0);
        users.Add(newUser);

        userManager.WriteUsers(users);

        await DisplayAlert("Succès", "Inscription réussie !", "OK");
        await Shell.Current.GoToAsync("//page/MainPage");
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