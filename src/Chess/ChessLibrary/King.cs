﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a king piece
    /// </summary>
    public class King : Piece
    {
        private bool canCastleRight;
        private bool canCastleLeft;
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="initialCase"></param>
        public King(Color color, int id) : base(color, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (Math.Abs(x - x2) > 1 || Math.Abs(y - y2) > 1)
            {
                throw new InvalidOperationException("Invalid move for King");
            }

            return true;
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            throw new NotImplementedException();
            //We need to know how we gonna check "échec" because if a case put the King in "échec position he can go on this case!!"
        }
    }
}