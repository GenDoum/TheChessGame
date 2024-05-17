using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe qui représente le point d'entrée du jeu d'échecs
    /// </summary>
    public class Game : IRules
    {
        /// <summary>
        /// Événement déclenché lorsqu'un pion peut évoluer
        /// </summary>
        public event EventHandler<EvolveNotifiedEventArgs> EvolveNotified;

        /// <summary>
        /// Événement déclenché lorsqu'un pion peut évoluer
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnEvolvePiece(EvolveNotifiedEventArgs args)
            => EvolveNotified?.Invoke(this, args);

        /// <summary>
        /// Événement déclenché lorsqu'un joueur gagne la partie
        /// </summary>
        public event EventHandler<GameOverNotifiedEventArgs> GameOverNotified;

        /// <summary>
        /// Événement déclenché lorsqu'un joueur gagne la partie
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnGameOver(GameOverNotifiedEventArgs args)
            => GameOverNotified?.Invoke(this, args);

        /// <summary>
        /// Représente le joueur 1
        /// </summary>
        public User Player1 { get; set; }
        
        /// <summary>
        /// Représente le joueur 2
        /// </summary>
        public User Player2 { get; set; }
        
        /// <summary>
        /// Représente l'echiquier
        /// </summary>
        public Chessboard Board { get; set; }
        
        /// <summary>
        /// Savoir si le joueur blanc est en échec
        /// </summary>
        public bool WhiteCheck { get; set; }
        
        /// <summary>
        /// Savoir si le joueur noir est en échec
        /// </summary>
        public bool BlackCheck { get; set; }
        
        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public Game(User player1, User player2)
        {
            WhiteCheck = false;
            BlackCheck = false;
            this.Player1 = player1;
            this.Player2 = player2;
            Case[,] allcase = new Case[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    allcase[i, j] = new Case(i, j, null);
                }
            }

            Chessboard chessboard = new Chessboard(allcase, false);
            this.Board = chessboard;

        }

        /// <summary>
        /// Vérifie si le joueur est en échec
        /// </summary>
        /// <param name="game"></param>
        /// <param name="actualPlayer"></param>
        /// <returns></returns>
        public bool CheckChec(Game game, User actualPlayer)
        {
            var pieces = (actualPlayer.color == Color.White) ? game.Board.BlackPieces : game.Board.WhitePieces;
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king && game.Board.Echec(king, pieceInfo.CaseLink))
                {
                    if (actualPlayer.color == Color.White)
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
            var pieces = (Player1.color == Color.White) ? Board.BlackPieces : Board.WhitePieces;
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king && Board.EchecMat(king, pieceInfo.CaseLink))
                {
                    OnGameOver(new GameOverNotifiedEventArgs { Winner = winner });
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Fonction pour ajouter une pièce à une liste
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public void AddToList(List<CoPieces> list, CoPieces item)
        {
            list.Add(item);
        }

        /// <summary>
        /// Fonction pour retirer une pièce d'une liste
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public void RemoveFromList(List<CoPieces> list, CoPieces item)
        {
            list.Remove(item);
        }

        /// <summary>
        /// Fonction pour le déplacement d'une pièce
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        /// <param name="board"></param>
        /// <param name="actualPlayer"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void MovePiece(Case initial, Case final, Chessboard board, User actualPlayer)
        {
            // Validation de base pour vérifier la pièce initiale
            ArgumentNullException.ThrowIfNull(initial.Piece, "Vous ne pouvez pas déplacer une pièce qui n'existe pas.");

            // Vérifier si la pièce appartient au joueur actuel
            if (initial.Piece.Color != actualPlayer.color)
                throw new InvalidOperationException("It's not this player's turn.");

            // Effectuer le déplacement temporaire pour la vérification
            final.Piece = initial.Piece;
            initial.Piece = null;

            // Vérifier si le mouvement met le roi en échec
            if (board.IsInCheck(actualPlayer.color))
            {
                // Restaurer l'état initial des cases
                initial.Piece = final.Piece;
                final.Piece = null;
                throw new InvalidOperationException("You cannot move into check.");
            }

            // Restaurer l'état initial des cases avant de faire le déplacement réel
            initial.Piece = final.Piece;
            final.Piece = null;

            // Si le joueur actuel est en échec, vérifier que le mouvement résout l'échec
            bool isInCheck = board.IsInCheck(actualPlayer.color);
            if (isInCheck)
            {
                if (!board.CanResolveCheck(initial, final, actualPlayer.color))
                    throw new InvalidOperationException("You must move out of check.");
            }

            // Effectuer le déplacement réel
            if (board.CanMovePiece(initial.Piece, initial, final))
            {
                // Mettre à jour l'état réel du plateau


                UpdatePieceLists(initial, final, board);
                ProcessPostMove(initial, final);

                // Vérifier si la case finale est un pion et qu'elle peut évoluer
                if (final is { Piece: Pawn, Line: 0 or 7 })
                {
                    OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = final.Piece as Pawn, Case = final });
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid move, check the rules.");
            }
        }


        public void UpdatePieceLists(Case initial, Case final, Chessboard board)
        {
            var movedPieceInfo = new CoPieces { CaseLink = initial, piece = initial.Piece };
            var listToUpdate = initial.Piece.Color == Color.White ? board.WhitePieces : board.BlackPieces;
            List<CoPieces> list = new List<CoPieces>(listToUpdate);

            // Mettre à jour la position de la pièce déplacée
            RemoveFromList(list, movedPieceInfo);
            AddToList(list, new CoPieces { CaseLink = final, piece = initial.Piece });


            // Vérifier si une pièce a été capturée
            if (final.Piece != null && final.Piece.Color != initial.Piece.Color)
            {
                var capturedPieceInfo = new CoPieces { CaseLink = final, piece = final.Piece };
                var listToRemoveFrom = final.Piece.Color == Color.White ? board.WhitePieces : board.BlackPieces;
                List<CoPieces> list2 = new List<CoPieces>(listToRemoveFrom);
                RemoveFromList(list2, capturedPieceInfo);

            }
        }

        /// <summary>
        /// Fonction pour traiter le déplacement d'une pièce
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        private void ProcessPostMove(Case initial, Case final)
        {
            // Mettre à jour les positions des cases
            final.Piece = initial.Piece;
            initial.Piece = null;

            // Marquer les mouvements spéciaux comme le premier mouvement pour les rois, tours et pions
            if (final.Piece is IFirstMove firstMover)
            {
                firstMover.FirstMove = false;
            }
        }

    }
}

