using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe représentant le chavalier
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Constructeur de la classe Knight
        /// </summary>
        /// <param name="c"></param>
        /// <param name="id"></param>
        public Knight(Color c, int id) : base(c, id)
        {
            ImagePath = c == Color.White ? "chavalier.png" : "chavalier_b.png";
        }

        public override bool CanMove(int x, int y, int x2, int y2)
        {
            if ((Math.Abs(x - x2) == 2 && Math.Abs(y - y2) == 1) || (Math.Abs(x - x2) == 1 && Math.Abs(y - y2) == 2))
            {
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                    throw new InvalidMovementException("Invalid move for Knight: destination out of bounds.");
                
                return true;
            }

            throw new InvalidMovementException("Invalid move for Knight: not L-shaped.");
        }


        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();

            int[,] offsets = new int[,]
            {
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 },
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }
            };

            for (int i = 0; i < 8; i++)
            {
                int newColumn = caseInitial!.Column + offsets[i, 0];
                int newLine = caseInitial.Line + offsets[i, 1];

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];
                    if (CanMove(caseInitial.Column, caseInitial.Line, newColumn, newLine) && (!potentialCase!.IsCaseEmpty() && potentialCase.Piece!.Color != this.Color || potentialCase.IsCaseEmpty()))
                        result.Add(potentialCase);
                }
            }
            return result;
        }


    }
}