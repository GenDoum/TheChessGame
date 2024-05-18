using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe représentant la reine
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        /// Constructeur de la classe Queen
        /// </summary>
        /// <param name="c"></param>
        /// <param name="ca"></param>
        public Queen(Color color, int id) : base(color, id)
        {
        }

        public override bool CanMove(int x, int y, int x2, int y2)
        {
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                throw new InvalidMovementException("Invalid move for Queen: destination out of bounds.");

            if (x == x2 || y == y2 || Math.Abs(x - x2) == Math.Abs(y - y2))
                return true;

            throw new InvalidMovementException("Invalid move for Queen: not diagonal, horizontal or vertical.");
        }

        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {

            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right ,Top Left, Top Right, Bot Left,Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + (colInc * i);
                    int newLine = caseInitial.Line + (lineInc * i);
                    if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                    {
                        Case potentialCase = chessboard.Board[newColumn, newLine];
                        if (CanMove(caseInitial.Column, caseInitial.Line, newColumn, newLine))
                        {
                            if (potentialCase.IsCaseEmpty())
                            {
                                result.Add(potentialCase);
                            }
                            else if (!potentialCase.IsCaseEmpty() && potentialCase.Piece.Color != this.Color)
                            {
                                result.Add(potentialCase);
                                break;
                            }
                            else
                                break;
                        }
                    }
                    else
                        break;
                }
            }
            return result;
        }

    }


}
