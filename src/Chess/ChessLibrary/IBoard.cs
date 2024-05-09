using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IBoard
    {
        bool MovePiece(Case Initial,Case Final, User P);

        bool IsMoveValid(List<Case> Lcase, Case Final);
        bool Echec(King king);

        //User Turn();

        //bool IsCaseEmpty(Case @case);

    }
}