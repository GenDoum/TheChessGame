using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a queen piece
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Queen(Color color, int id) : base(color, id)
        {
        }

        public override bool canMove(int x, int y, int x2, int y2)
        {
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
            {
                throw new InvalidOperationException("Invalid move for Queen: destination out of bounds.");
            }
            
            if (x == x2 || y == y2 || Math.Abs(x - x2) == Math.Abs(y - y2))
            {
                return true;
            }

            throw new InvalidOperationException("Invalid move for Queen");
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            
            ArgumentNullException.ThrowIfNull(nameof(chessboard));

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0),(-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right ,Top Left, Top Right, Bot Left,Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + (colInc * i);
                    int newLine = caseInitial.Line + (lineInc * i);
                    if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                    {
                        Case potentialCase = chessboard.Board[newColumn, newLine];
                        if (canMove(caseInitial.Column, caseInitial.Line, newColumn, newLine))
                        {
                            result.Add(potentialCase);
                        }
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


    }
}