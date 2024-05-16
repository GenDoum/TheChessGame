using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    internal interface IBoard
    {
        bool CanMovePiece(Piece piece, Case Initial,Case Final);

        bool IsMoveValid(List<Case> Lcase, Case Final);
        bool Echec(King king,Case kingCase);

    }
}