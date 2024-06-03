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
using Color = ChessLibrary.Color;

namespace Chess.Pages;

public partial class chessBoard : ContentPage
{
    public Game Game { get; } = new Game(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black));
    public Chessboard Board { get; } = new Chessboard(new Case[8, 8], false);
    
    public chessBoard()
    {
        InitializeComponent();
        BindingContext = this;
        
        Game.InvalidMove += OnInvalidMove;
        Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
        Game.EvolveNotified += OnEvolvePiece;
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
<<<<<<< HEAD
        string action = await DisplayActionSheet("Choose the piece to evolve to:", "Cancel", null, "Queen", "Rook", "Bishop", "Knight");
=======
        string action = await DisplayActionSheet("Choose the piece to evolve to:", null, null, "Queen", "Rook", "Bishop", "Knight");        
>>>>>>> f16004b681d7c8c08d8b69d1a943eb176aebe0e5
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
    
    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
    
    
    private Case? _selectedCase;
    

    async void OnPieceClicked(object sender, EventArgs e)
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
                        Game.MovePiece(_selectedCase, clickedCase, Board, actualPlayer);
                    }

                    _selectedCase = null;
                }
            }
        }
    }
    
}