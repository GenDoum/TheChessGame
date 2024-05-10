using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Game : IRules
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public Chessboard Board { get; set; }
        //public event void ImpossibleMove();
        //public event EventHandler<ImpossibleMove> impossible;
        //protected virtual void OnGameStarted()
        //{
        //    impossible?.Invoke(this, EventArgs.Empty);
        //}
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

        public void GameOver(User winner)
        {
            Console.WriteLine("Game Over");
            Console.WriteLine(winner.Pseudo + " win");
        }

        public void MovePiece(Case initial, Case final, Chessboard board, User actualPlayer)
        {
            // Validation de base pour vérifier la pièce initiale
            if (initial.Piece == null)
                throw new ArgumentNullException(nameof(initial.Piece), "No piece at the initial position.");

            // Vérifier si la pièce appartient au joueur actuel
            if (initial.Piece.Color != actualPlayer.color)
                throw new InvalidOperationException("It's not this player's turn.");

            // Effectuer le déplacement
            if (board.MovePiece(initial.Piece, initial, final))
            {
                UpdatePieceLists(initial, final, board);
                ProcessPostMove(initial, final);
            }
            else
            {
                throw new InvalidOperationException("Invalid move, check the rules.");
            }
        }

        private void UpdatePieceLists(Case initial, Case final, Chessboard board)
        {
            // Logique pour mettre à jour les listes des pièces
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


        public void start()
        {
            throw new NotImplementedException();
        }

    }
}