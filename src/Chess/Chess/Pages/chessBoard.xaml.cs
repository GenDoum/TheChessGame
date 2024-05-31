using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessLibrary;
using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Graphics;

namespace Chess.Pages;

public partial class chessBoard : ContentPage
{
    public Game game { get; } = new Game(new User(ChessLibrary.Color.White), new User(ChessLibrary.Color.Black));
    public Chessboard Board { get; } = new Chessboard(new Case[8, 8], false);
    
    public chessBoard()
    {
        InitializeComponent();
        BindingContext = this;
    }
    
    async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//page/MainPage");
    }
    
    void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        var imageButton = sender as ImageButton;
        if (imageButton != null)
        {
            var piece = imageButton.BindingContext as Piece;
            if (piece != null)
            {
                e.Data.Properties.Add("piece", piece);
            }
        }
    }
    
    void OnDrop(object sender, DropEventArgs e)
    {
        var imageButton = sender as ImageButton;
        if (imageButton != null)
        {
            var piece = e.Data.Properties["piece"] as Piece;
            if (piece != null)
            {
                var finalCase = imageButton.BindingContext as Case;
                var initialCase = Board.FindCase(piece);
                if (finalCase != null && initialCase != null)
                {
                    try
                    {
                        game.MovePiece(initialCase, finalCase, Board, actualPlayer: game.Player1);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
    
}