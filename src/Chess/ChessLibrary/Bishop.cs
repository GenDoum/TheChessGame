using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            
            if (x2 < 1 || x2 > 8 || y2 < 1 || y2 > 8)
            {
                throw new InvalidOperationException("Invalid move for Bishop: destination out of bounds.");
            }

            return true;    
        }





        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
            }
            if (caseInitial== null)
            {
                throw new ArgumentNullException(nameof(caseInitial));
            }

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (-1, 1), (1, 1), (-1, -1), (1,-1) };  // Top Left, Top Right, Bot Left,Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + (colInc * i);
                    int newLine = caseInitial.Line + (lineInc * i);
                    if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                    {
                        Case potentialCase = chessboard.Board[newColumn, newLine];
                        if (potentialCase.IsCaseEmpty())
                        {
                            result.Add(potentialCase);
                        }
                        else
                        {
                            if (potentialCase.Piece.Color != this.Color)
                            {
                                result.Add(potentialCase);
                            }
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