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
    public class Rook : Piece, IFirstMove.FirstMove
    {
        public bool FirstMove { get; set; }
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Rook(Color color, int id) : base(color, id)
        {
            this.FirstMove = true;
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
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0) };  // Top, Bot, Left, Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + colInc * i;
                    int newLine = caseInitial.Line + lineInc * i;
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

