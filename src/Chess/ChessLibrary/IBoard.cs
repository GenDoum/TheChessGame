using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IBoard
    {
        bool CanMovePiece(Piece? piece, Case initial,Case final);

        bool IsMoveValid(List<Case> lcase, Case final);
        bool Echec(King? king,Case kingCase);

    }
}