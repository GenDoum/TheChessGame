using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a Rook piece
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Rook(Color color, int id) : base(color, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
            {
                throw new InvalidOperationException("Invalid move for Rook: destination out of bounds.");
            }
            
            if (x == x2 || y == y2)
            {
                return true;
            }

            throw new InvalidOperationException("Invalid move for Rook");
        }



        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0) };  // Top, Bot, Left, Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + colInc * i;
                    int newLine = caseInitial.Line + lineInc * i;

                    if (IsWithinBoardBoundaries(newColumn, newLine))
                    {
                        Case potentialCase = chessboard.Board[newColumn, newLine];
                        AddPotentialMove(result, potentialCase);
                        if (!potentialCase.IsCaseEmpty())
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private bool IsWithinBoardBoundaries(int column, int line)
        {
            return column >= 0 && column < 8 && line >= 0 && line < 8;
        }

        private void AddPotentialMove(List<Case> result, Case potentialCase)
        {
            if (potentialCase.IsCaseEmpty() || potentialCase.Piece.Color != this.Color)
            {
                result.Add(potentialCase);
            }
        }
    }
}

