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

namespace Chess.Pages
{
    /// <summary>
    /// Represents the chess board page.
    /// </summary>
    public partial class chessBoard : ContentPage
    {
        /// <summary>
        /// Gets or sets the current game instance.
        /// </summary>
        public Game Game { get; set; }

        /// <summary>
        /// Gets the manager instance from the application.
        /// </summary>
        public Manager MyManager => (App.Current as App)!.MyManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="chessBoard"/> class.
        /// </summary>
        public chessBoard()
        {
            foreach (Game game in MyManager.Games)
            {
                if ((Equals(game.Player1, MyManager.Games.First().Player1) || Equals(game.Player1, MyManager.Games.First().Player2)) &&
                    (Equals(game.Player2, MyManager.Games.First().Player1) || Equals(game.Player2, MyManager.Games.First().Player2)))
                {
                    this.Game = game;
                }
            }

            this.Game = MyManager.Games.First();
            this.Game.InvalidMove += OnInvalidMove;
            this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
            this.Game.EvolveNotified += OnEvolvePiece;
            this.Game.GameOverNotified += OnGameOver;

            BindingContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="chessBoard"/> class with specified players.
        /// </summary>
        /// <param name="u1">The first player.</param>
        /// <param name="u2">The second player.</param>
        public chessBoard(User u1, User u2)
        {
            this.Game = new Game(u1, u2);
            this.Game.InvalidMove += OnInvalidMove;
            this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
            this.Game.EvolveNotified += OnEvolvePiece;
            this.Game.GameOverNotified += OnGameOver;

            BindingContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the invalid move event.
        /// Displays an alert message indicating the invalid move.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public async void OnInvalidMove(object sender, EventArgs e)
        {
            await DisplayAlert("Erreur", "Mouvement invalide, vérifiez les règles.", "OK");
        }

        /// <summary>
        /// Handles the error player turn event.
        /// Displays an alert message indicating it is not the player's turn.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public async void OnErrorPlayerTurnNotified(object sender, EventArgs e)
        {
            await DisplayAlert("Erreur", "Ce n'est pas votre tour de jouer.", "OK");
        }

        /// <summary>
        /// Handles the evolve piece event.
        /// Displays a dialog for the user to choose the piece to evolve the pawn into.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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

        /// <summary>
        /// Handles the game over event.
        /// Displays an alert message indicating the winner and updates the scores.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private async void OnGameOver(object sender, GameOverNotifiedEventArgs e)
        {
            await DisplayAlert("Game Over", e.Winner.Pseudo + " wins the game!", "OK");
            e.Winner.Score += 5;
            e.Loser.Score -= 5;
            await Shell.Current.GoToAsync("//page/MainPage");
        }

        private Case? _selectedCase;

        /// <summary>
        /// Handles the event when a piece is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
                            var piece = clickedCase.Piece;
                            if (piece != null)
                            {
                                // If no piece is selected, select the clicked piece
                                _selectedCase = clickedCase;
                                // Get the possible moves for the piece
                                var possibleMoves = piece.PossibleMoves(_selectedCase, Game.Board);
                                var possibleCoordinates = possibleMoves.Select(c => $"({c.Column}, {c.Line})");

                                foreach (var move in possibleMoves)
                                {
                                    move.IsPossibleMove = true;
                                }
                            }
                        }
                        else
                        {
                            // If a piece is already selected, move it to the clicked case
                            var piece = _selectedCase.Piece;
                            if (piece != null)
                            {
                                User actualPlayer = piece.Color == Color.White ? Game.Player1 : Game.Player2;
                                Game.MovePieceFront(_selectedCase, clickedCase, Game.Board, actualPlayer);
                                Game.Board.ResetPossibleMoves();
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

        /// <summary>
        /// Handles the event when the pause button is clicked.
        /// Displays the pause popup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnPauseButtonClicked(object sender, EventArgs e)
        {
            this.ShowPopup(new pausePage());
        }
    }
}
