using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessLibrary;
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
    }
    
    public async void OnInvalidMove(object sender, EventArgs e)
    {
        await DisplayAlert("Erreur", "Mouvement invalide, vérifiez les règles.", "OK");
    }
    
    public async void OnErrorPlayerTurnNotified(object sender, EventArgs e)
    {
        await DisplayAlert("Erreur", "Ce n'est pas votre tour de jouer.", "OK");
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
                        User actualPlayer = Game.ActualPlayer;
                        Game.MovePiece(_selectedCase, clickedCase, Board, actualPlayer);
                        Console.WriteLine($"Calling MovePiece with parameters: currentCase={_selectedCase}, targetCase={clickedCase}, Board={Board}, actualPlayer={actualPlayer}");
                    }

                    _selectedCase = null;
                }
            }
        }
    }
    
    
}