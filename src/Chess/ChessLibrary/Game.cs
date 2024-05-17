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

        public Game(User player1, User player2)
        {

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
                    return true;
                }
            }
            return false;
        }

        public bool CheckGameOver(Game game)
        {
            var pieces = (game.Player1.color == Color.White) ? game.Board.BlackPieces : game.Board.WhitePieces;
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king && game.Board.EchecMat(king, pieceInfo.CaseLink))
                {
                    return true;
                }
            }
            return false;

        }

        
        public void GameOver(User winner)
        {
            OnGameOver(new GameOverNotifiedEventArgs { Winner = winner });
        }

        public void MovePiece(Case initial, Case final, Chessboard board, User actualPlayer)
        {
            // Validation de base pour vérifier la pièce initiale
            ArgumentNullException.ThrowIfNull(initial);

            // Vérifier si la pièce appartient au joueur actuel
            if (initial.Piece.Color != actualPlayer.color)
                throw new InvalidOperationException("It's not this player's turn.");

            // Effectuer le déplacement
            if (board.CanMovePiece(initial.Piece, initial, final))
            {
                UpdatePieceLists(initial, final, board);
                ProcessPostMove(initial, final);

                // Vérifier si la case finale est un pion et qu'elle peut évoluer
                if (final is { Piece: Pawn, Line: 0 or 7 })
                {
                    // Dans ce cas, on doit demander au joueur quelle pièce il veut. (Avec un événement)
                    OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = final.Piece as Pawn, Case = final });
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid move, check the rules.");
            }
        }


        private void UpdatePieceLists(Case initial, Case final, Chessboard board)
        {
            var movedPieceInfo = new CoPieces { CaseLink = initial, piece = initial.Piece };
            var listToUpdate = initial.Piece.Color == Color.White ? board.WhitePieces : board.BlackPieces;

            // Mettre à jour la position de la pièce déplacée
            listToUpdate.Remove(movedPieceInfo);
            listToUpdate.Add(new CoPieces { CaseLink = final, piece = initial.Piece });

            // Vérifier si une pièce a été capturée
            if (final.Piece != null && final.Piece.Color != initial.Piece.Color)
            {
                var capturedPieceInfo = new CoPieces { CaseLink = final, piece = final.Piece };
                var listToRemoveFrom = final.Piece.Color == Color.White ? board.WhitePieces : board.BlackPieces;
                listToRemoveFrom.Remove(capturedPieceInfo);

                Console.WriteLine($"{final.Piece.Color} {final.Piece.GetType().Name} captured.");
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