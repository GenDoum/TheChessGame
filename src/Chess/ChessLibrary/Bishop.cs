﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Bishop : Piece
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Bishop(Color color, int id) : base(color, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (Math.Abs(x - x2) != Math.Abs(y - y2))
            {
                throw new InvalidOperationException("Invalid move for Bishop");
            }

            return true;
        }
    }
}