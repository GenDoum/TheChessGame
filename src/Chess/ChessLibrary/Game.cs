using System.Runtime.Serialization;
using ChessLibrary.Events;
using System.ComponentModel;

namespace ChessLibrary
{
    /// <summary>
    /// Classe qui représente le point d'entrée du jeu d'échecs
    /// </summary>
    [DataContract]
    [KnownType(typeof(CoPieces))]
    public class Game : IRules, INotifyPropertyChanged
    {

        /// <summary>
        /// Événement déclenché lorsqu'un pion peut évoluer
        /// </summary>
        public event EventHandler<EvolveNotifiedEventArgs> EvolveNotified = null!;

        /// <summary>
        /// Événement déclenché lorsqu'un pion peut évoluer
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnEvolvePiece(EvolveNotifiedEventArgs args)
            => EvolveNotified?.Invoke(this, args);

        /// <summary>
        /// Événement déclenché lorsqu'un joueur gagne la partie
        /// </summary>
        public event EventHandler<GameOverNotifiedEventArgs> GameOverNotified = null!;

        /// <summary>
        /// Événement déclenché lorsqu'un joueur gagne la partie
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnGameOver(GameOverNotifiedEventArgs args)
            => GameOverNotified?.Invoke(this, args);

        public event EventHandler InvalidMove = null!;

        protected virtual void OnInvalidMove()
            => InvalidMove?.Invoke(this, EventArgs.Empty);

        public event EventHandler ErrorPlayerTurnNotified = null!;

        protected virtual void OnErrorPlayerTurn()
            => ErrorPlayerTurnNotified?.Invoke(this, EventArgs.Empty);


        private User _player1;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Représente le joueur actuel
        /// </summary>
        [DataMember]
        public User CurrentPlayer { get; private set; }


        private Chessboard _board;
        /// <summary>
        /// Représente l'échiquier
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
        /// Savoir si le joueur blanc est en échec
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
        /// Savoir si le joueur noir est en échec
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
        /// Constructeur de la classe Game
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="userDataManager"></param>
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
        /// Fonction qui vérifie si un joueur est en échec
        /// </summary>
        /// <param name="actualPlayer"></param>
        /// <returns></returns>
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
        /// Fonction permettant de savoir si la partie est terminée
        /// </summary>
        /// <param name="winner"></param>
        /// <returns></returns>
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
        /// Fonction pour le déplacement d'une pièce
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <param name="board"></param>
        /// <param name="actualPlayer"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void MovePiece(Case? initial, Case? final, Chessboard board, User actualPlayer)
        {


            if (initial!.Piece is King king &&
            ((king.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 7 && final.Line == 7) ||
            (king.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 7 && final.Line == 0)))
            {
                king.PetitRoque(board); // Appel de la méthode PetitRoque
            }
            else if (initial!.Piece is King king1 &&
            ((king1.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 0 && final.Line == 7) ||
            (king1.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 0 && final.Line == 0)))
            {
                king1.GrandRoque(board); // Appel de la méthode GrandRoque
            }
            else
            {

                if (initial!.Piece == null)
                    throw new InvalidOperationException("Vous ne pouvez pas déplacer une pièce qui n'existe pas.");

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
                    // Simulation du mouvement pour la vérification

                    UpdatePieceLists(blackPieces, whitePieces, initial, final, board); // Met à jour les listes de pièces de manière temporaire
                    final.Piece = movingPiece;
                    initial.Piece = null;
                    // Vérification de l'échec après le mouvement temporaire
                    if (board.IsInCheck(actualPlayer.Color))
                    {
                        // Annuler le mouvement temporaire
                        final.Piece = capturedPiece;
                        initial.Piece = movingPiece;
                        RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                        throw new InvalidOperationException("Vous ne pouvez pas vous mettre en échec.");
                    }
                    RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    // Vérification si le mouvement est légal et si cela peut résoudre un échec existant
                    // Effectuer le mouvement réel
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
                    // Annuler le mouvement temporaire
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    Board.ResetPossibleMoves();
                    OnInvalidMove();
                }
            }
        }

        public void MovePieceFront(Case? initial, Case? final, Chessboard board, User actualPlayer)
        {
            if (initial!.Piece is King king &&
            ((king.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 7 && final.Line == 7) ||
            (king.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 7 && final.Line == 0)))
            {
                king.PetitRoque(board); // Appel de la méthode PetitRoque
            }
            else if (initial!.Piece is King king1 &&
            ((king1.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 0 && final.Line == 7) ||
            (king1.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 0 && final.Line == 0)))
            {
                king1.GrandRoque(board); // Appel de la méthode GrandRoque
            }
            else
            {

                if (initial!.Piece == null)
                    throw new InvalidOperationException("Vous ne pouvez pas déplacer une pièce qui n'existe pas.");

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
                    // Simulation du mouvement pour la vérification

                    UpdatePieceLists(blackPieces, whitePieces, initial, final, board); // Met à jour les listes de pièces de manière temporaire
                    final.Piece = movingPiece;
                    initial.Piece = null;
                    // Vérification de l'échec après le mouvement temporaire
                    if (board.IsInCheck(actualPlayer.Color))
                    {
                        // Annuler le mouvement temporaire
                        final.Piece = capturedPiece;
                        initial.Piece = movingPiece;
                        RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                        throw new InvalidOperationException("Vous ne pouvez pas vous mettre en échec.");
                    }
                    RestorePieceLists(blackPieces, whitePieces, initial, final, board, movingPiece, capturedPiece!);
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    // Vérification si le mouvement est légal et si cela peut résoudre un échec existant
                    // Effectuer le mouvement réel
                    Board.ProcessPostMove(initial, final);

                    if (final.Piece is Pawn pawn && (final.Line == 0 || final.Line == 7))
                        OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = pawn, Case = final });

                    if (GameOver(CurrentPlayer))
                    {
                        OnGameOver(new GameOverNotifiedEventArgs { Winner = CurrentPlayer, Loser = actualPlayer == Player1 ? Player2 : Player1 });
                    }
                    CurrentPlayer = (actualPlayer == Player1) ? Player2 : Player1;
                }
                else
                {
                    // Annuler le mouvement temporaire
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    Board.ResetPossibleMoves();
                    OnInvalidMove();
                }
            }
        }
        public static void RestorePieceLists(List<CoPieces> blackPieces, List<CoPieces> whitePieces, Case? initial, Case? final, Chessboard board, Piece movedPiece, Piece capturedPiece)
        {
            // Rétablir la pièce déplacée dans sa position originale
            var listToUpdate = movedPiece.Color == Color.White ? whitePieces : blackPieces;
            // Enlever la pièce de sa nouvelle position dans la liste et la remettre à l'initial
            listToUpdate.RemoveAll(p => p.piece == movedPiece && p.CaseLink == final);
            listToUpdate.Add(new CoPieces { CaseLink = initial, piece = movedPiece });

            // Si une pièce a été capturée, la remettre dans sa liste respective
            if (capturedPiece != null)
            {
                var listToRestore = capturedPiece.Color == Color.White ? whitePieces : blackPieces;
                listToRestore.Add(new CoPieces { CaseLink = final, piece = capturedPiece });
            }
        }

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