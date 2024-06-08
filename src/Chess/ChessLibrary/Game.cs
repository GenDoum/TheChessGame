﻿using System.Runtime.Serialization;
using ChessLibrary.Events;
using System.ComponentModel;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the entry point of the chess game.
    /// </summary>
    [DataContract]
    [KnownType(typeof(CoPieces))]
    public class Game : IRules, INotifyPropertyChanged
    {
        /// <summary>
        /// Event triggered when a pawn can evolve.
        /// </summary>
        public event EventHandler<EvolveNotifiedEventArgs> EvolveNotified = null!;

        /// <summary>
        /// Invokes the EvolveNotified event.
        /// </summary>
        /// <param name="args">Event arguments for evolution.</param>
        protected virtual void OnEvolvePiece(EvolveNotifiedEventArgs args)
            => EvolveNotified?.Invoke(this, args);

        /// <summary>
        /// Event triggered when a player wins the game.
        /// </summary>
        public event EventHandler<GameOverNotifiedEventArgs> GameOverNotified = null!;

        /// <summary>
        /// Invokes the GameOverNotified event.
        /// </summary>
        /// <param name="args">Event arguments for game over.</param>
        protected virtual void OnGameOver(GameOverNotifiedEventArgs args)
            => GameOverNotified?.Invoke(this, args);

        /// <summary>
        /// Event triggered when an invalid move is made.
        /// </summary>
        public event EventHandler InvalidMove = null!;

        /// <summary>
        /// Invokes the InvalidMove event.
        /// </summary>
        protected virtual void OnInvalidMove()
            => InvalidMove?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Event triggered when an error occurs with the player's turn.
        /// </summary>
        public event EventHandler ErrorPlayerTurnNotified = null!;

        /// <summary>
        /// Invokes the ErrorPlayerTurnNotified event.
        /// </summary>
        protected virtual void OnErrorPlayerTurn()
            => ErrorPlayerTurnNotified?.Invoke(this, EventArgs.Empty);

        private User _player1;
        /// <summary>
        /// Gets or sets Player1.
        /// </summary>
        [DataMember]
        public User Player1
        {
            get { return _player1; }
            set
            {
                if (_player1 != value)
                {
                    _player1 = value;
                    OnPropertyChanged(nameof(Player1));
                }
            }
        }

        private User _player2;
        /// <summary>
        /// Gets or sets Player2.
        /// </summary>
        [DataMember]
        public User Player2
        {
            get { return _player2; }
            set
            {
                if (_player2 != value)
                {
                    _player2 = value;
                    OnPropertyChanged(nameof(Player2));
                }
            }
        }

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the current player.
        /// </summary>
        [DataMember]
        public User CurrentPlayer { get; private set; }

        private Chessboard _board;
        /// <summary>
        /// Gets or sets the chessboard.
        /// </summary>
        [DataMember]
        public Chessboard Board
        {
            get
            {
                return _board;
            }
            set
            {
                if (value != _board)
                {
                    _board = value;
                    OnPropertyChanged(nameof(Board));
                }
            }
        }

        private bool _whiteCheck;
        /// <summary>
        /// Indicates whether the white player is in check.
        /// </summary>
        [DataMember]
        public bool WhiteCheck
        {
            get { return _whiteCheck; }
            set
            {
                if (_whiteCheck != value)
                {
                    _whiteCheck = value;
                    OnPropertyChanged(nameof(WhiteCheck));
                }
            }
        }

        private bool _blackCheck;

        /// <summary>
        /// Indicates whether the black player is in check.
        /// </summary>
        [DataMember]
        public bool BlackCheck
        {
            get { return _blackCheck; }
            set
            {
                if (_blackCheck != value)
                {
                    _blackCheck = value;
                    OnPropertyChanged(nameof(BlackCheck));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            Player1 = new User(ChessLibrary.Color.White);
            Player2 = new User(ChessLibrary.Color.Black);

            WhiteCheck = false;
            BlackCheck = false;

            Case?[,] allcase = new Case[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    allcase[i, j] = new Case(i, j, null);
                }
            }

            Chessboard chessboard = new Chessboard();
            this.Board = chessboard;

            CurrentPlayer = Player1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class with specified players.
        /// </summary>
        /// <param name="player1">The first player.</param>
        /// <param name="player2">The second player.</param>
        public Game(User player1, User player2)
        {
            WhiteCheck = false;
            BlackCheck = false;
            this.Player1 = player1;
            this.Player2 = player2;

            Case?[,] allcase = new Case[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    allcase[i, j] = new Case(i, j, null);
                }
            }

            Chessboard chessboard = new Chessboard(allcase, false);
            this.Board = chessboard;

            CurrentPlayer = Player1;
        }

        /// <summary>
        /// Checks if the specified player is in check.
        /// </summary>
        /// <param name="actualPlayer">The player to check.</param>
        /// <returns>True if the player is in check, otherwise false.</returns>
        public bool IsCheck(User actualPlayer)
        {
            var pieces = (actualPlayer.Color == Color.White) ? Board.BlackPieces : Board.WhitePieces;
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king && Board.Echec(king, pieceInfo.CaseLink))
                {
                    if (actualPlayer.Color == Color.White)
                    {
                        WhiteCheck = true;
                    }
                    else
                    {
                        BlackCheck = true;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines if the game is over.
        /// </summary>
        /// <param name="winner">The player who potentially won.</param>
        /// <returns>True if the game is over, otherwise false.</returns>
        public bool GameOver(User winner)
        {
            var pieces = (Player1.Color == Color.White) ? Board.CopyBlackPieces() : Board.CopyWhitePieces();
            foreach (var pieceInfo in pieces)
            {
                if (Board.CanDefendKing(pieces, pieceInfo.CaseLink))
                {
                    return false;
                }
                if (pieceInfo.piece is King king && Board.EchecMat(king, pieceInfo.CaseLink))
                {
                    OnGameOver(new GameOverNotifiedEventArgs { Winner = winner, Loser = winner == Player1 ? Player2 : Player1 });
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Evolves a pawn to a specified piece.
        /// </summary>
        /// <param name="pawn">The pawn to evolve.</param>
        /// <param name="finalCase">The case where the evolution occurs.</param>
        /// <param name="choiceUser">The choice of piece for evolution.</param>
        /// <exception cref="InvalidOperationException">Thrown when the choice for evolution is invalid.</exception>
        public void Evolve(Pawn pawn, Case finalCase, ChoiceUser choiceUser)
        {
            Piece newPiece;

            switch (choiceUser)
            {
                case ChoiceUser.Queen:
                    newPiece = new Queen(pawn.Color, pawn.Id);
                    break;
                case ChoiceUser.Rook:
                    newPiece = new Rook(pawn.Color, pawn.Id);
                    break;
                case ChoiceUser.Bishop:
                    newPiece = new Bishop(pawn.Color, pawn.Id);
                    break;
                case ChoiceUser.Knight:
                    newPiece = new Knight(pawn.Color, pawn.Id);
                    break;
                default:
                    throw new InvalidOperationException("Invalid choice for pawn evolution.");
            }

            finalCase.Piece = newPiece;
            Board.ModifPawn(pawn, newPiece, finalCase);
        }


        /// <summary>
        /// Moves a piece from the initial position to the final position, handling special moves like castling.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <param name="board">The chessboard.</param>
        /// <param name="actualPlayer">The player making the move.</param>
        /// <exception cref="InvalidOperationException">Thrown when the move is invalid.</exception>
        public void MovePiece(Case? initial, Case? final, Chessboard board, User actualPlayer)
        {
            if (initial!.Piece is King king &&
                ((king.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 7 && final.Line == 7) ||
                 (king.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 7 && final.Line == 0)))
            {
                king.PetitRoque(board); // Call the PetitRoque method
            }
            else if (initial!.Piece is King king1 &&
                     ((king1.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 0 && final.Line == 7) ||
                      (king1.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 0 && final.Line == 0)))
            {
                king1.GrandRoque(board); // Call the GrandRoque method
            }
            else
            {
                if (initial!.Piece == null)
                    throw new InvalidOperationException("You cannot move a piece that does not exist.");

                if (actualPlayer != CurrentPlayer)
                {
                    return;
                }

                var blackPieces = board.CopyBlackPieces();
                var whitePieces = board.CopyWhitePieces();
                var movingPiece = initial.Piece;
                var capturedPiece = final!.Piece;

                if (board.CanMovePiece(movingPiece, initial, final))
                {
                    // Simulate the move for check verification
                    UpdatePieceLists(blackPieces, whitePieces, initial, final, board); // Temporarily update piece lists
                    final.Piece = movingPiece;
                    initial.Piece = null;

                    // Check for check after temporary move
                    if (board.IsInCheck(actualPlayer.Color))
                    {
                        // Undo the temporary move
                        final.Piece = capturedPiece;
                        initial.Piece = movingPiece;
                        RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                        throw new InvalidOperationException("You cannot put yourself in check.");
                    }
                    RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;

                    // Check if the move is legal and if it can resolve an existing check
                    // Perform the actual move
                    Board.ProcessPostMove(initial, final);

                    if (final.Piece is Pawn pawn && (final.Line == 0 || final.Line == 7))
                        OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = pawn, Case = final });

                    if (GameOver(CurrentPlayer))
                    {
                        OnGameOver(new GameOverNotifiedEventArgs { Winner = CurrentPlayer, Loser = actualPlayer == Player1 ? Player2 : Player1 });
                        return;
                    }

                    CurrentPlayer = (actualPlayer == Player1) ? Player2 : Player1;
                }
                else
                {
                    // Undo the temporary move
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    Board.ResetPossibleMoves();
                    OnInvalidMove();
                }
            }
        }
        private bool roque1(Case initial, Case final, Chessboard board)
        {
            if (initial!.Piece is King king &&((king.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 7 && final.Line == 7) ||(king.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 7 && final.Line == 0)))
            {
                king.PetitRoque(board); // Call the PetitRoque method
                return true;
            }
            return false;
        }
        private bool roque2(Case initial,Case final,Chessboard board)
        {
            if (initial!.Piece is King king1 &&
                     ((king1.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 0 && final.Line == 7) ||
                      (king1.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 0 && final.Line == 0)))
            {
                king1.GrandRoque(board); // Call the GrandRoque method
                return true;
            }
            return false;
        }
        private void evolve(Case final)
        {
            if (final.Piece is Pawn pawn && (final.Line == 0 || final.Line == 7)) 
            { 
                OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = pawn, Case = final });
            }
        }
        /// <summary>
        /// Moves a piece forward, handling special moves like castling.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <param name="board">The chessboard.</param>
        /// <param name="actualPlayer">The player making the move.</param>
        /// <exception cref="InvalidOperationException">Thrown when the move is invalid.</exception>
        public void MovePieceFront(Case? initial, Case? final, Chessboard board, User actualPlayer)
        {
            if (roque1(initial, final, board)) 
            {
                return;
            }
            else if( roque2(initial,final, board) )
            {
                return;
            }
            else
            {
                if (initial!.Piece == null)
                    throw new InvalidOperationException("You cannot move a piece that does not exist.");

                if (actualPlayer.Color != CurrentPlayer.Color)
                {
                    Board.ResetPossibleMoves();
                    OnErrorPlayerTurn();
                    return;
                }

                var blackPieces = board.CopyBlackPieces();
                var whitePieces = board.CopyWhitePieces();
                var movingPiece = initial.Piece;
                var capturedPiece = final!.Piece;

                if (board.CanMovePiece(movingPiece, initial, final))
                {
                    // Simulate the move for check verification
                    UpdatePieceLists(blackPieces, whitePieces, initial, final, board); // Temporarily update piece lists
                    final.Piece = movingPiece;
                    initial.Piece = null;

                    // Check for check after temporary move
                    if (board.IsInCheck(actualPlayer.Color))
                    {
                        // Undo the temporary move
                        final.Piece = capturedPiece;
                        initial.Piece = movingPiece;
                        RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                        throw new InvalidOperationException("You cannot put yourself in check.");
                    }
                    RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;

                    // Check if the move is legal and if it can resolve an existing check
                    // Perform the actual move
                    Board.ProcessPostMove(initial, final);
                    evolve(final);

                    if (GameOver(CurrentPlayer))
                    {
                        OnGameOver(new GameOverNotifiedEventArgs { Winner = CurrentPlayer, Loser = actualPlayer == Player1 ? Player2 : Player1 });
                    }
                    CurrentPlayer = (actualPlayer == Player1) ? Player2 : Player1;
                }
                else
                {
                    // Undo the temporary move
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    Board.ResetPossibleMoves();
                    OnInvalidMove();
                }
            }
        }

        /// <summary>
        /// Restores the piece lists to their original state after a move has been undone.
        /// </summary>
        /// <param name="blackPieces">The list of black pieces.</param>
        /// <param name="whitePieces">The list of white pieces.</param>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <param name="board">The chessboard.</param>
        /// <param name="movedPiece">The piece that was moved.</param>
        /// <param name="capturedPiece">The piece that was captured.</param>
        public static void RestorePieceLists(List<CoPieces> blackPieces, List<CoPieces> whitePieces, Case? initial, Case? final, Chessboard board, Piece movedPiece, Piece capturedPiece)
        {
            // Restore the moved piece to its original position
            var listToUpdate = movedPiece.Color == Color.White ? whitePieces : blackPieces;
            listToUpdate.RemoveAll(p => p.piece == movedPiece && p.CaseLink == final);
            listToUpdate.Add(new CoPieces { CaseLink = initial, piece = movedPiece });

            // Restore the captured piece to its list if it was captured
            if (capturedPiece != null)
            {
                var listToRestore = capturedPiece.Color == Color.White ? whitePieces : blackPieces;
                listToRestore.Add(new CoPieces { CaseLink = final, piece = capturedPiece });
            }
        }

        /// <summary>
        /// Updates the piece lists after a move has been made.
        /// </summary>
        /// <param name="blackPieces">The list of black pieces.</param>
        /// <param name="whitePieces">The list of white pieces.</param>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        /// <param name="board">The chessboard.</param>
        public static void UpdatePieceLists(List<CoPieces> blackPieces, List<CoPieces> whitePieces, Case? initial, Case? final, Chessboard board)
        {
            var movedPiece = initial!.Piece;
            var capturedPiece = final!.Piece;
            var listToUpdate = movedPiece!.Color == Color.White ? whitePieces : blackPieces;

            listToUpdate.RemoveAll(p => p.piece == movedPiece);
            listToUpdate.Add(new CoPieces { CaseLink = final, piece = movedPiece });

            if (capturedPiece != null && capturedPiece.Color != movedPiece.Color)
            {
                var listToRemoveFrom = capturedPiece.Color == Color.White ? whitePieces : blackPieces;
                listToRemoveFrom.RemoveAll(p => p.piece == capturedPiece);
            }
        }

    }
}