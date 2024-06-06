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


        if (password == confirmPassword)
        {
            User user = new User(pseudo, password, ChessLibrary.Color.White, true, 0);
            await DisplayAlert("Succès", "Inscription réussie", "OK");
            await Shell.Current.GoToAsync("//page/MainPage");
        }
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        game.Player1.IsConnected = false;
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}