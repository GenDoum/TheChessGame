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

    private static readonly IUserDataManager UserManager;
    public Game Game { get; } = new Game(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black), UserManager);
    
    public Register()
    {
        InitializeComponent();
    }

    public Register(Game game)
    {
        InitializeComponent();
        BindingContext = this;
        Game = game;
    }
    
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        
    }
    
    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
}