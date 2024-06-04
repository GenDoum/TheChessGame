using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance;
using ChessLibrary;
using Persistance;

namespace Chess.Pages;

public partial class Register : ContentPage
{

    Game game;

    public Register()
    {
        InitializeComponent();
    }

    public Register(Game game)
    {
        InitializeComponent();
        BindingContext = game;
        this.game = game;
    }
    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string pseudo = PseudoEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        if (string.IsNullOrEmpty(pseudo) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
            return;
        }

        var existingUsers = game._userDataManager.ReadUsers();
        if (existingUsers.Any(u => u.Pseudo == pseudo))
        {
            await DisplayAlert("Erreur", "Ce pseudo est déjà utilisé", "OK");
            return;
        }

        User newUser = new User { Pseudo = pseudo, Password = password };
        existingUsers.Add(newUser);

        game._userDataManager.WriteUsers(existingUsers);

        await DisplayAlert("Succès", "Votre compte a bien été créé", "OK");

        await Shell.Current.GoToAsync("//page/MainPage");
        
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}