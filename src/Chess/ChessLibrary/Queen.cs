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
            throw new NotImplementedException();
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            if (chessboard == null)
            {
                throw new ArgumentNullException(nameof(chessboard));
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
                int newLine = caseInitial.Line + i;
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
                int newLine = caseInitial.Line + i;
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