using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the knight piece in chess.
    /// </summary>
    [DataContract(Name = "Knight")]
    public class Knight : Piece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Knight"/> class.
        /// </summary>
        /// <param name="c">The color of the knight.</param>
        /// <param name="id">The unique identifier for this knight.</param>
        public Knight(Color c, int id) : base(c, id)
        {
            // Sets the image path based on the color.
            ImagePath = c == Color.White ? "chavalier.png" : "chavalier_b.png";
        }

        /// <summary>
        /// Determines if the knight can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the knight.</param>
        /// <param name="y">The current y-coordinate of the knight.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is out of bounds or not a valid knight move (L-shaped).</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the move is an L-shape: two squares in one direction and one in the other.
            if ((Math.Abs(x - x2) == 2 && Math.Abs(y - y2) == 1) || (Math.Abs(x - x2) == 1 && Math.Abs(y - y2) == 2))
            {
                // Ensure the destination is within the board limits.
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                    throw new InvalidMovementException("Invalid move for Knight: destination out of bounds.");

                return true;
            }

            throw new InvalidMovementException("Invalid move for Knight: not L-shaped.");
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the knight from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the knight.</param>
        /// <param name="chessboard">The chessboard on which the knight is placed.</param>
        /// <returns>A list of possible moves for the knight.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();

            // Possible L-shaped moves for a knight.
            int[,] offsets = new int[,]
            {
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 },
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }
            };

            // Iterate through each possible L-shaped move.
            for (int i = 0; i < 8; i++)
            {
                int newColumn = caseInitial!.Column + offsets[i, 0];
                int newLine = caseInitial.Line + offsets[i, 1];

                // Check if the new position is within the board limits.
                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];
                    // Add the move if it's legal and either the square is empty or contains an opponent's piece.
                    if (CanMove(caseInitial.Column, caseInitial.Line, newColumn, newLine) && (potentialCase!.IsCaseEmpty() || potentialCase.Piece!.Color != this.Color))
                        result.Add(potentialCase);
                }
            }
            return result;
        }
    }
}
