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

    private static readonly IUserDataManager UserManager = new UserManager();
    Game Game = new Game(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black), UserManager);
    
    public Register()
    {
        InitializeComponent();
    }
    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        string pseudo = PseudoEntry.Text;
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

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

        User newUser = new User(pseudo, password, ChessLibrary.Color.White, true, 0);
        try
        {
            List<User> users = Game.ReadUsers();
            users.Add(newUser);
            Game.SaveUsers(users);
        }
        catch (Exception ex)
        {
            // Affichez l'exception pour comprendre ce qui ne va pas
            await DisplayAlert("Erreur", $"Une erreur s'est produite lors de la lecture des utilisateurs : {ex.Message}", "OK");
            return;
        }

        await DisplayAlert("Succès", "Inscription réussie.", "OK");
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