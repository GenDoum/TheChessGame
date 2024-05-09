using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IRules
    {
        void movement(Case initial,Case Final,Chessboard board,User ActualPlayer);

        void start();

        bool IsGameOver(Chessboard chess);
        //appel a Check Mate ,true fin de game false on continue la game
        void GameOver(User winner);
        //appel a evenement


    }
}
