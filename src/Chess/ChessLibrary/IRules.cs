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

        void GameOver(User winner);
        //appel a evenement


    }
}
