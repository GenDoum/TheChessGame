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
using System.Runtime.CompilerServices;

namespace Chess.Pages
{
    /// <summary>
    /// Represents the chess board page in the application.
    /// </summary>
    public partial class chessBoard : ContentPage
    {
        private Case? _selectedCase;

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
            // Set the current game from the manager's games
            this.Game = MyManager.Games.First();

            // Check if there is an existing game with the connected players
            CheckGameIfExists(this.Game.Player1, this.Game.Player2);

            // Subscribe to game events
            this.Game.InvalidMove += OnInvalidMove;
            this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
            this.Game.EvolveNotified += OnEvolvePiece;
            this.Game.GameOverNotified += OnGameOver;

            // Set the binding context
            BindingContext = this;

            // Initialize the component
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="chessBoard"/> class with specified players.
        /// </summary>
        /// <param name="u1">The first player.</param>
        /// <param name="u2">The second player.</param>
        public chessBoard(User u1, User u2)
        {
            // Check if there is an existing game with the specified players
            CheckGameIfExists(u1, u2);

            // Create a new game with the specified players
            this.Game = new Game(u1, u2);

            // Subscribe to game events
            this.Game.InvalidMove += OnInvalidMove;
            this.Game.ErrorPlayerTurnNotified += OnErrorPlayerTurnNotified;
            this.Game.EvolveNotified += OnEvolvePiece;
            this.Game.GameOverNotified += OnGameOver;
            this.Game.Board = new Chessboard();
            // Set the binding context
            BindingContext = this;

            // Initialize the component
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
        Game existingGame = null;

        foreach (Game game in MyManager.Games)
        {
            if ((game.Player1.Pseudo == playerOne.Pseudo && game.Player2.Pseudo == playerTwo.Pseudo) || 
                (game.Player2.Pseudo == playerOne.Pseudo && game.Player1.Pseudo == playerTwo.Pseudo))
            {
                existingGame = game;
                break;
            }
        }

        if (existingGame != null)
        {
            bool continueGameExisting = await DisplayAlert("Warning", "If a game already exist with you two. Did you want to continu this game ?", "Yes", "No");

            if (continueGameExisting)
            {
                this.Game = existingGame;
                return;
            }
        }

        // If no existing game or user chose not to continue the existing game, create a new one
        this.Game = new Game(playerOne, playerTwo);
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
            this.ShowPopup(new endGame(e.Winner.Pseudo));
            e.Winner!.Score += 5;
            if ((e.Loser!.Score -= 5) < 0)
                e.Loser!.Score = 0;
            else
                e.Loser!.Score -= 5;
            await Shell.Current.GoToAsync("//page/MainPage");
        }

        /// <summary>
        /// Handles the back button click event.
        /// Navigates back to the root page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Handles the piece click event.
        /// Selects or moves a piece based on the current selection.
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

        /// <summary>
        /// Selects the piece on the clicked case.
        /// </summary>
        /// <param name="clickedCase">The case that was clicked.</param>
        private void SelectPiece(Case clickedCase)
        {
            var piece = clickedCase.Piece;
            // If no piece is selected, select the clicked piece
            if (piece != null)
            {
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

        /// <summary>
        /// Moves the selected piece to the clicked case.
        /// </summary>
        /// <param name="clickedCase">The case that was clicked.</param>
        private void MovePiece(Case clickedCase)
        {
            var piece = _selectedCase.Piece;
            if (piece != null)
            {
                var actualPlayer = piece.Color == Color.White ? Game.Player1 : Game.Player2;
                Game.MovePiece(_selectedCase, clickedCase, Game.Board, actualPlayer);
                Game.Board.ResetPossibleMoves();
            }

            _selectedCase = null;
        }

        /// <summary>
        /// Handles the pause button click event.
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
