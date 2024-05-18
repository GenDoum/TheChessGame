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
            var pieces = (Player1.Color == Color.White) ? Board.BlackPieces : Board.WhitePieces;
            List<CoPieces> list = new List<CoPieces>();
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king && Board.EchecMat(king, pieceInfo.CaseLink))
                {
                    OnGameOver(new GameOverNotifiedEventArgs { Winner = winner });
                    if ( Board.CanDefendKing(pieceInfo, pieceInfo.CaseLink) )
                    {
                        return false;
                    }

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
            ArgumentNullException.ThrowIfNull(initial.Piece, "Vous ne pouvez pas déplacer une pièce qui n'existe pas.");

            if (initial.Piece.Color != actualPlayer.Color)
                throw new InvalidOperationException("It's not this player's turn.");

            var movingPiece = initial.Piece;
            var capturedPiece = final.Piece;

            // Déplacement temporaire pour la vérification
            final.Piece = initial.Piece;
            initial.Piece = null;

            // Vérification de l'échec
            if (board.IsInCheck(actualPlayer.Color))
            {
                initial.Piece = final.Piece;
                final.Piece = capturedPiece;
                // throw new InvalidOperationException("You cannot move into check."); // commenté car on veut que le joueur puisse se déplacer pour sortir de l'échec
            }

            // Déplacement réel
            initial.Piece = final.Piece;
            final.Piece = capturedPiece;

            if (board.CanMovePiece(movingPiece, initial, final))
            {
                UpdatePieceLists(initial, final, board);
                ProcessPostMove(initial, final);

                if (final.Piece is Pawn pawn && (final.Line == 0 || final.Line == 7))
                {
                    OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = pawn, Case = final });
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid move, check the rules.");
            }
        }



        public void UpdatePieceLists(Case initial, Case final, Chessboard board)
        {
            var movedPiece = initial.Piece;
            var capturedPiece = final.Piece;

            var listToUpdate = movedPiece.Color == Color.White ? board.WhitePieces : board.BlackPieces;

            listToUpdate.RemoveAll(p => p.piece == movedPiece);
            listToUpdate.Add(new CoPieces { CaseLink = final, piece = movedPiece });

            if (capturedPiece != null && capturedPiece.Color != movedPiece.Color)
            {
                var listToRemoveFrom = capturedPiece.Color == Color.White ? board.WhitePieces : board.BlackPieces;
                listToRemoveFrom.RemoveAll(p => p.piece == capturedPiece);
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

