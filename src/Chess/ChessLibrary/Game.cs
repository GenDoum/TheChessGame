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
                if (pieceInfo.piece is King king)
                {
                    if (game.Board.Echec(king, pieceInfo.CaseLink))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CheckGameOver(Game game)
        {
            var pieces = (game.Player1.color == Color.White) ? game.Board.BlackPieces : game.Board.WhitePieces;
            foreach (var pieceInfo in pieces)
            {
                if (pieceInfo.piece is King king)
                {
                    if (game.Board.EchecMat(king, pieceInfo.CaseLink))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public void GameOver(User winner)
        {
        }

        public void MovePiece(Case initial, Case Final, Chessboard board, User ActualPlayer)
        {
            // Validation de base pour vérifier la pièce initiale
            if (initial.Piece == null)
                throw new ArgumentNullException(nameof(initial.Piece), "No piece at the initial position.");

            // Vérifier si la pièce appartient au joueur actuel
            if (initial.Piece.Color != ActualPlayer.color)
                throw new InvalidOperationException("It's not this player's turn.");

            // Effectuer le déplacement
            if (board.CanMovePiece(initial.Piece, initial, Final))
            {
                UpdatePieceLists(initial, Final, board);
                ProcessPostMove(initial, Final);

                // Vérifier si la case finale est un pion et qu'elle peut évoluer
                if (Final.Piece is Pawn && (Final.Line == 0 || Final.Line == 7))
                {
                    // Dans ce cas, on doit demander au joueur quelle pièce il veut. (Avec un événement)
                    OnEvolvePiece(new EvolveNotifiedEventArgs { Pawn = Final.Piece as Pawn, Case = Final });
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

            listToUpdate.Remove(movedPieceInfo);
            listToUpdate.Add(new CoPieces { CaseLink = final, piece = initial.Piece });

            if (!final.IsCaseEmpty())
            {
                var capturedPieceInfo = new CoPieces { CaseLink = final, piece = final.Piece };
                var listToRemoveFrom = final.Piece.Color == Color.White ? board.WhitePieces : board.BlackPieces;
                listToRemoveFrom.Remove(capturedPieceInfo);

                Console.WriteLine($"{final.Piece.Color} {final.Piece.GetType().Name} captured.");
            }
        }

        private void ProcessPostMove(Case initial, Case final)
        {
            // Marquer les mouvements spéciaux comme le premier mouvement pour les rois, tours et pions
            if (initial.Piece is IFirstMove.FirstMove firstMover)
            {
                firstMover.FirstMove = false;
            }
            // Mettre à jour les positions des cases
            final.Piece = initial.Piece;
            initial.Piece = null;
        }
    }
}