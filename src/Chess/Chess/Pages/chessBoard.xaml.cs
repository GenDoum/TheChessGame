using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessLibrary;
using ChessLibrary.Events;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Graphics;
using Persistance;
using Color = ChessLibrary.Color;

namespace Chess.Pages;

public partial class chessBoard : ContentPage
{
    private static readonly IUserDataManager UserManager = new UserManager();
    public Game Game { get; set; } = new Game(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black), UserManager);
    
    public chessBoard()
    {
        InitializeComponent();
        BindingContext = this;

        Game.InvalidMove += OnInvalidMove;
        Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
        Game.EvolveNotified += OnEvolvePiece;
        Game.GameOverNotified += OnGameOver;
    }

    public chessBoard(Game game)
    {
        InitializeComponent();
        BindingContext = this;

        game.InvalidMove += OnInvalidMove!;
        game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified!;
        game.EvolveNotified += OnEvolvePiece!;
        game.GameOverNotified += OnGameOver!;
        Game = game;
    }

    public async void OnInvalidMove(object sender, EventArgs e)
    {
        await DisplayAlert("Erreur", "Mouvement invalide, vérifiez les règles.", "OK");
    }
    
    public async void OnErrorPlayerTurnNotified(object sender, EventArgs e)
    {
        await DisplayAlert("Erreur", "Ce n'est pas votre tour de jouer.", "OK");
    }
    
    private async void OnEvolvePiece(object sender, EvolveNotifiedEventArgs e)
    {

        string action = await DisplayActionSheet("Choose the piece to evolve to:", null, null, "Queen", "Rook", "Bishop", "Knight");        
        if (action != null)
        {
            ChoiceUser choice;
            switch (action)
            {
                case "Queen":
                    choice = ChoiceUser.Queen;
                    break;
                case "Rook":
                    choice = ChoiceUser.Rook;
                    break;
                case "Bishop":
                    choice = ChoiceUser.Bishop;
                    break;
                case "Knight":
                    choice = ChoiceUser.Knight;
                    break;
                default:
                    return;
            }
            Game.Evolve(e.Pawn, e.Case, choice);
        }
    }
    
    private async void OnGameOver(object sender, GameOverNotifiedEventArgs e)
    {
        await DisplayAlert("Game Over", e.Winner.Pseudo + " wins the game!", "OK");
        e.Winner.Score += 5;
        e.Loser.Score -= 5;
        await Shell.Current.GoToAsync("//page/MainPage");
    }
    
    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
    
    
    private Case? _selectedCase;
    

    async void OnPieceClicked(object sender, EventArgs e)
    {
        try
        {
            var imageButton = sender as ImageButton;
            if (imageButton != null)
            {
                var clickedCase = imageButton.BindingContext as Case;
                if (clickedCase != null)
                {
                    if (_selectedCase == null)
                    {
                        // Si aucune pièce n'est sélectionnée, sélectionne la pièce sur laquelle nous avons cliqué
                        _selectedCase = clickedCase;
                    }
                    else
                    {
                        // Si une pièce est déjà sélectionnée, la déplacer vers la case sur laquelle nous avons cliqué
                        var piece = _selectedCase.Piece;
                        if (piece != null)
                        {
                            User actualPlayer = piece.Color == Color.White ? Game.Player1 : Game.Player2;
                            Game.MovePieceFront(_selectedCase, clickedCase, Game.Board, actualPlayer);
                        }

                        _selectedCase = null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

}