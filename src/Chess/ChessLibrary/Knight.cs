﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a knight piece
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Knight(Color c, int id) : base(c, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            throw new NotImplementedException();
            //We need to know how we gonna check "échec" because if a case put the King in "échec position he can go on this case"
        }
    }
}
