using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Game
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public Chessboard Board { get; set; }

        public Game(User player1, User player2, Chessboard board)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.Board = board;
        }
        
    }
}