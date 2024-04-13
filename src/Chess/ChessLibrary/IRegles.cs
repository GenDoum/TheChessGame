using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IRegles
    {
        void MovePiece(Piece piece, Case Initial,Case Final);

        bool IsMoveValid(Piece piece);

        User Turn();

        List<Case> MoveAvaliable(Piece piece, Case @case);


        bool IsCaseEmpty(Case @case);

    }
}
