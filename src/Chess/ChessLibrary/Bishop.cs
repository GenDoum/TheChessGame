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

            return true;
        }





        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new NotImplementedException();
            }

            List<Case> result = new List<Case>();
            // Go Top Left

            for (int i = 1; i < 8; i++)
            {
                int newColumn = caseInitial.Column + i;
                int newLine = caseInitial.Line - i;
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


            // Go Top Right
            for (int i = 1; i < 8; i++)
            {
                int newColumn = caseInitial.Column + i;
                int newLine = caseInitial.Line +i;
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

            // Go Bottom Left
            for (int i = 1; i < 8; i++)
            {
                int newColumn = caseInitial.Column - i;
                int newLine = caseInitial.Line - i;
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

            // Go Bottom Right
            for (int i = 1; i < 8; i++)
            {
                int newColumn = caseInitial.Column + i;
                int newLine = caseInitial.Line - i;
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

            return result;
        }
    }
}