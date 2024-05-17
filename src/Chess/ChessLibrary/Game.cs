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
    public class Game : IRules
    {
        public event EventHandler<EvolveNotifiedEventArgs> EvolveNotified;

        protected virtual void OnEvolvePiece(EvolveNotifiedEventArgs args)
            => EvolveNotified?.Invoke(this, args);

        public event EventHandler<GameOverNotifiedEventArgs> GameOverNotified;

        protected virtual void OnGameOver(GameOverNotifiedEventArgs args)
            => GameOverNotified?.Invoke(this, args);

        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public Chessboard Board { get; set; }
        public bool WhiteCheck { get; set; }
        public bool BlackCheck { get; set; }
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
        
        public void AddToList(List<CoPieces> list, CoPieces item)
        {
            list.Add(item);
        }

        public void RemoveFromList(List<CoPieces> list, CoPieces item)
        {
            list.Remove(item);
        }
        

        public void MovePiece(Case initial, Case final, Chessboard board, User actualPlayer)
        {
            // Validation de base pour vérifier la pièce initiale
            ArgumentNullException.ThrowIfNull(initial.Piece, "Vous ne pouvez pas déplacer une pièce qui n'existe pas.");

            // Vérifier si la pièce appartient au joueur actuel
            if (initial.Piece.Color != actualPlayer.color)
                throw new InvalidOperationException("It's not this player's turn.");

            // Vérifier si le joueur actuel est en échec
            bool isInCheck = board.IsInCheck(actualPlayer.color);

            // Si le joueur est en échec, vérifier que le mouvement résout l'échec
            if (isInCheck)
            {
                if (!board.CanResolveCheck(initial, final, actualPlayer.color))
                    throw new InvalidOperationException("You must move out of check.");
            }

            // Effectuer le déplacement
            if (board.CanMovePiece(initial.Piece, initial, final))
            {
                UpdatePieceLists(initial, final, board);
                ProcessPostMove(initial, final);

                // Vérifier si la case finale est un pion et qu'elle peut évoluer
                if (final is { Piece: Pawn, Line: 0 or 7 })
                {
                    OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = final.Piece as Pawn, Case = final });

                }
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