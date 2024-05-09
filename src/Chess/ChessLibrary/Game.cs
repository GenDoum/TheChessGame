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
        public User Player1;
        public User Player2;
        public Chessboard Board;
        //public event void ImpossibleMove();
        //public event EventHandler<ImpossibleMove> impossible;
        //protected virtual void OnGameStarted()
        //{
        //    impossible?.Invoke(this, EventArgs.Empty);
        //}
        public Game(User player1, User player2, Chessboard board)
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
            throw new NotImplementedException();
        }

        public void movement(Case initial, Case Final, Chessboard board, User ActualPlayer)
        {
            if(initial.Piece == null)
                throw new ArgumentNullException(nameof(initial.Piece));
            if(initial.Piece.Color == ActualPlayer.color)
                throw new InvalidOperationException("Invalid move for this player");
            if (board.MovePiece(initial.Piece,initial, Final))
            {
                if (initial.Piece.Color == Color.White)
                {
                    board.WhitePieces.Add(new CoPieces { CaseLink = Final, piece = initial.Piece });
                    board.WhitePieces.Remove(new CoPieces { CaseLink = initial, piece = initial.Piece });
                }
                else
                {
                    board.BlackPieces.Add(new CoPieces { CaseLink = Final, piece = initial.Piece });
                    board.BlackPieces.Remove(new CoPieces { CaseLink = initial, piece = initial.Piece });
                }
                Final.Piece = initial.Piece;
                initial.Piece = null;
            }
            else { }
                // evenement a implementer
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
