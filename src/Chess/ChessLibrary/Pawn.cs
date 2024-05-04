﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a pawn piece
    /// </summary>
    public class Pawn : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Pawn(Color c, int id) : base(c, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
            {
                throw new InvalidOperationException("Invalid move for Pawn: destination out of bounds.");
            }
    
            int direction = (Color == Color.White) ? 1 : -1;

            if (x == x2)
            {
                if (y2 == y + direction || (Color == Color.White && y == 1 || Color == Color.Black && y == 6) && y2 == y + 2 * direction)
                {
                    return true;
                }
            }

            if (Math.Abs(x2 - x) == 1 && y2 == y + direction)
            {
                return true;
            }

            throw new InvalidOperationException("Invalid move for Pawn");
        }
        
        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }
            List<Case> result = new List<Case>();

            //Check if this is the first movement


            if (true/*this.FirstMove()*/) // Need to create fonction first Move Maybe we need to change the constructor of the Pawn.
            {
                for (int i = 1; i <= 2; i++)
                {
                    int newColumn = caseInitial.Column + i;
                    int newLine = caseInitial.Line;
                    Case potentialCase = chessboard.Board[newColumn, newLine];
                    if (potentialCase.IsCaseEmpty())
                    {
                        result.Add(potentialCase);
                    }
                    // Need to create a fonction to check if the pawn can eat a other piece top Right or Top Left  
                    // and we need to check if the left piece if we can do the special movement of the pawn.
                }
            }
            //Check for the rest.
            else
            {
                int newColumn = caseInitial.Column + 1;
                int newLine = caseInitial.Line;
                Case potentialCase = chessboard.Board[newColumn, newLine];
                if (potentialCase.IsCaseEmpty())
                {
                    result.Add(potentialCase);
                }
                // Same as last if before .
            }
            return result;
        }
  
    }
}