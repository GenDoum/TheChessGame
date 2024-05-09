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
        User Player1;
        User Player2;
        Chessboard Board;
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

        public bool IsGameOver(Chessboard chess)
        {
           if(chess.EchecMat())
             return true;

           return false;
        }

        public void movement(Case initial, Case final, Chessboard board, User ActualPlayer)
        {
            if(initial.Piece == null)
                throw new ArgumentNullException(nameof(initial.Piece));

            if (board.MovePiece(initial.Piece,initial, final,ActualPlayer))
            {
                final.Piece = initial.Piece;
                initial.Piece = null;
            }
            else { }
                // evenement
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
