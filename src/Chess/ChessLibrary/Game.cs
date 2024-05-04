using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Game
    {
        User Player1;
        User Player2;
        Chessboard Board;

        public Game(User player1, User player2, Chessboard board)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.Board = board;
        }

/*        public bool GameIsOver()
        {
            if (Player1.isCheckMate() || Player2.isCheckMate())
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
    }
}