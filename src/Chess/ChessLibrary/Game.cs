using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary.Events;

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
                if (pieceInfo.piece is King king && Board.EchecMat(king, pieceInfo.CaseLink))
                {
                    OnGameOver(new GameOverNotifiedEventArgs { Winner = winner });
                    if (Board.CanDefendKing(pieces, pieceInfo.CaseLink))
                    {
                        return false;
                    }

                    return true;
                }
            }
            return false;
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
            ((king.Color == Color.White && initial.Column == 4 && initial.Line == 7 && final!.Column == 6 && final.Line == 7) ||
            (king.Color == Color.Black && initial.Column == 4 && initial.Line == 0 && final!.Column == 6 && final.Line == 0)))
            {
                king.PetitRoque(Board); // Appel de la méthode PetitRoque
            }
            else
            {


                if (initial!.Piece == null)
                    throw new InvalidOperationException("Vous ne pouvez pas déplacer une pièce qui n'existe pas.");
                if (initial.Piece.Color != actualPlayer.Color)
                    throw new InvalidOperationException("Ce n'est pas le tour de ce joueur.");
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
                    ProcessPostMove(initial, final);

                    if (final.Piece is Pawn pawn && (final.Line == 0 || final.Line == 7))
                    {
                        OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = pawn, Case = final });
                    }
                }
                else
                {
                    // Annuler le mouvement temporaire
                    initial.Piece = movingPiece;
                    final.Piece = capturedPiece;
                    throw new InvalidOperationException("Mouvement invalide, vérifiez les règles.");
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

        /// <summary>
        /// Fonction pour traiter le déplacement d'une pièce
        /// </summary>
        /// <param name="initial"></param>
        /// <param name="final"></param>
        private void ProcessPostMove(Case? initial, Case? final)
        {
            if (final!.Piece! != null && final!.Piece!.Color != initial.Piece.Color)
            {
                Board.RemovePieceFromList(initial);
            }
            final!.Piece = initial!.Piece;
            initial.Piece = null;
            Board.ModifList(initial, final);
            // Marquer les mouvements spéciaux comme le premier mouvement pour les rois, tours et pions
            if (final.Piece is IFirstMove firstMover)
            {
                firstMover.FirstMove = false;
            }
        }

    }
}

