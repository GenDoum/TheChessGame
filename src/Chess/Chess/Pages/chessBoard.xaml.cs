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
    
    // void OnDragStarting(object sender, DragStartingEventArgs e)
    // {
    //     var imageButton = sender as ImageButton;
    //     if (imageButton != null)
    //     {
    //         var piece = imageButton.BindingContext as Case;
    //         if (piece != null)
    //         {
    //             e.Data.Properties.Add("piece", piece.Piece);
    //             e.Data.Properties.Add("currentCase", piece);
    //         }
    //     }
    // }
    //
    // void OnDrop(object sender, DropEventArgs e)
    // {
    //     var imageButton = sender as ImageButton;
    //     if (imageButton != null)
    //     {
    //         var targetCase = imageButton.BindingContext as Case;
    //         if (targetCase != null)
    //         {
    //             var piece = e.Data.Properties["piece"] as Piece;
    //             var currentCase = e.Data.Properties["currentCase"] as Case;
    //             
    //             if (piece != null && currentCase != null)
    //             {
    //                 var possibleMoves = piece.PossibleMoves(currentCase, Board);
    //                 
    //                 if (possibleMoves.Contains(targetCase))
    //                 {
    //                     User actualPlayer = piece.Color == Color.White ? game.Player1 : game.Player2;
    //                     game.MovePiece(currentCase, targetCase, Board, actualPlayer);
    //                     Console.WriteLine($"Calling MovePiece with parameters: currentCase={currentCase}, targetCase={targetCase}, Board={Board}, actualPlayer={actualPlayer}");
    //                 }
    //             }
    //         }
    //     }
    // }
    
    private Case? selectedCase;

    void OnPieceClicked(object sender, EventArgs e)
    {
        var imageButton = sender as ImageButton;
        if (imageButton != null)
        {
            var clickedCase = imageButton.BindingContext as Case;
            if (clickedCase != null)
            {
                if (selectedCase == null)
                {
                    // Si aucune pièce n'est sélectionnée, sélectionnez la pièce sur laquelle nous avons cliqué
                    selectedCase = clickedCase;
                }
                else
                {
                    // Si une pièce est déjà sélectionnée, essayez de la déplacer vers la case sur laquelle nous avons cliqué
                    var piece = selectedCase.Piece;
                    if (piece != null)
                    {
                        var possibleMoves = piece.PossibleMoves(selectedCase, Board);
                        if (possibleMoves.Contains(clickedCase))
                        {
                            User actualPlayer = piece.Color == Color.White ? game.Player1 : game.Player2;
                            game.MovePiece(selectedCase, clickedCase, Board, actualPlayer);
                            Console.WriteLine($"Calling MovePiece with parameters: currentCase={selectedCase}, targetCase={clickedCase}, Board={Board}, actualPlayer={actualPlayer}");
                        }
                    }

                    selectedCase = null;
                }
            }
        }
    }
    
    
}