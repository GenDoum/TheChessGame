using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IRules
    {
        void MovePiece(Case initial,Case final,Chessboard board,User actualPlayer);


        bool GameOver(User winner);
        //appel a evenement
    }
}
