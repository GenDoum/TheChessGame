using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessLibrary;
using ChessLibrary.Events;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics;
using Persistance;
using Color = ChessLibrary.Color;
using Plugin.Maui.Audio;
using System.Runtime.CompilerServices;

namespace Chess.Pages;

public partial class chessBoard : ContentPage
{
    private Case? _selectedCase;

    public Game Game { get; set; }
    public Manager MyManager => (App.Current as App).MyManager;

    public chessBoard()
    {

        this.Game = MyManager.Games.First();

        CheckGameIfExists(this.Game.Player1, this.Game.Player2);


        this.Game.InvalidMove += OnInvalidMove;
        this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
        this.Game.EvolveNotified += OnEvolvePiece;
        this.Game.GameOverNotified += OnGameOver;
        
        BindingContext = this;
        
        InitializeComponent();
    }

    public chessBoard(User u1, User u2)
    {
        CheckGameIfExists(u1, u2);

        this.Game = new Game(u1, u2);
        this.Game.InvalidMove += OnInvalidMove;
        this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
        this.Game.EvolveNotified += OnEvolvePiece;
        this.Game.GameOverNotified += OnGameOver;


        BindingContext = this;
        InitializeComponent();
    }

    /// <summary>
    /// This function search in all the game is MyManager.Games if already have a game with the connected player.
    /// If the game exist, they can continue it, it it doesn't a new game starting.
    /// </summary>
    /// <param name="playerOne"></param>
    /// <param name="playerTwo"></param>
    /// <returns></returns>
    public async void CheckGameIfExists(User playerOne, User playerTwo)
    {
        foreach (Game game in MyManager.Games)
        {
            if (game.Player1.Pseudo == playerOne.Pseudo && game.Player2.Pseudo == playerTwo.Pseudo || game.Player2.Pseudo == playerOne.Pseudo && game.Player1.Pseudo == playerTwo.Pseudo)
            {
                bool continueGameExisting = await DisplayAlert("Warning", "A game already exist with you two. Did you want to continu this game ?", "Yes", "No");

                if (continueGameExisting)
                {
                    this.Game = game;
                }
                else
                {
                    this.Game = new Game(playerOne, playerTwo);
                }
            }
            this.Game = new Game(playerOne, playerTwo);
        }
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
        this.ShowPopup(new endGame(e.Winner!.Pseudo));
        e.Winner.Score += 5;
        e.Loser!.Score -= 5;
        await Shell.Current.GoToAsync("//page/MainPage");
    }

    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

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
                        SelectPiece(clickedCase);
                    }
                    else
                    {
                        MovePiece(clickedCase);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", ex.Message, "OK");
        }
    }

    private void SelectPiece(Case clickedCase)
    {
        var piece = clickedCase.Piece;
        // Si aucune pièce n'est sélectionnée, sélectionne la pièce sur laquelle nous avons cliqué
        if (piece != null)
        {
            _selectedCase = clickedCase;
            // Récupérer les mouvements possibles de la pièce
            var possibleMoves = piece.PossibleMoves(_selectedCase, Game.Board);
            var possibleCoordinates = possibleMoves.Select(c => $"({c.Column}, {c.Line})");

            foreach (var move in possibleMoves)
            {
                move.IsPossibleMove = true;
            }
        }
    }

    private void MovePiece(Case clickedCase)
    {
        // Si une pièce est déjà sélectionnée, la déplacer vers la case sur laquelle nous avons cliqué
        var piece = _selectedCase.Piece;
        if (piece != null)
        {
            var actualPlayer = piece.Color == Color.White ? Game.Player1 : Game.Player2;
            Game.MovePiece(_selectedCase, clickedCase, Game.Board, actualPlayer);
            Game.Board.ResetPossibleMoves();
        }

        _selectedCase = null;
    }


    private void OnPauseButtonClicked(object sender, EventArgs e)
    {
        this.ShowPopup(new pausePage());
    }

}