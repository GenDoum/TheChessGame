using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the queen piece in chess.
    /// </summary>
    [DataContract(Name = "Queen")]
    public class Queen : Piece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Queen"/> class.
        /// </summary>
        /// <param name="color">The color of the queen.</param>
        /// <param name="id">The unique identifier for this queen.</param>
        public Queen(Color color, int id) : base(color, id)
        {
            // Sets the image path based on the color.
            ImagePath = color == Color.White ? "dame.png" : "dame_b.png";
        }

        /// <summary>
        /// Determines if the queen can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the queen.</param>
        /// <param name="y">The current y-coordinate of the queen.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is out of bounds or not a valid queen move (diagonal, horizontal, or vertical).</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the destination is within the board limits.
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                throw new InvalidMovementException("Invalid move for Queen: destination out of bounds.");

            // Check for valid queen moves: diagonal, horizontal, or vertical.
            if (x == x2 || y == y2 || Math.Abs(x - x2) == Math.Abs(y - y2))
                return true;

            throw new InvalidMovementException("Invalid move for Queen: not diagonal, horizontal or vertical.");
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the queen from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the queen.</param>
        /// <param name="chessboard">The chessboard on which the queen is placed.</param>
        /// <returns>A list of possible moves for the queen.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(caseInitial);
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            // Possible directions for the queen's movement.
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bottom, Left, Right, Top Left, Top Right, Bottom Left, Bottom Right

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial!.Column + (colInc * i);
                    int newLine = caseInitial.Line + (lineInc * i);

                    // Check if the new position is within the board limits.
                    if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                    {
                        Case? potentialCase = chessboard.Board[newColumn, newLine];
                        if (CanMove(caseInitial.Column, caseInitial.Line, newColumn, newLine))
                        {
                            // Add the move if the square is empty.
                            if (potentialCase!.IsCaseEmpty())
                            {
                                result.Add(potentialCase);
                            }
                            // Add the move if the square contains an opponent's piece and stop.
                            else if (!potentialCase.IsCaseEmpty() && potentialCase.Piece!.Color != this.Color)
                            {
                                result.Add(potentialCase);
                                break;
                            }
                            else
                                break; // Stop if blocked by a piece of the same color.
                        }
                    }
                    else
                        break; // Stop if out of bounds.
                }
            }
            return result;
        }
    }
}
