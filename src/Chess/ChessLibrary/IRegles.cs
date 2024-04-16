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

        bool IsMoveValid(List<Case> Lcase, Case Final);

        User Turn();

        //bool IsCaseEmpty(Case @case);

    }
}