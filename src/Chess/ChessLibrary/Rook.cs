using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the rook piece in chess.
    /// </summary>
    [DataContract(Name = "Rook")]
    public class Rook : Piece, IFirstMove
    {
        /// <summary>
        /// Gets or sets a value indicating whether the rook has made its first move.
        /// </summary>
        [DataMember]
        public bool FirstMove { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rook"/> class.
        /// </summary>
        /// <param name="color">The color of the rook.</param>
        /// <param name="id">The unique identifier for this rook.</param>
        public Rook(Color color, int id) : base(color, id)
        {
            // Initially, the rook has not moved.
            this.FirstMove = true;
            // Sets the image path based on the color.
            ImagePath = color == Color.White ? "tour.png" : "tour_b.png";
        }

        /// <summary>
        /// Determines if the rook can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the rook.</param>
        /// <param name="y">The current y-coordinate of the rook.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is out of bounds or not a valid rook move (horizontal or vertical).</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the destination is within the board limits.
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                throw new InvalidMovementException("Invalid move for Rook: destination out of bounds.");

            // Check for valid rook moves: horizontal or vertical.
            if (x == x2 || y == y2)
                return true;

            throw new InvalidMovementException("Invalid move for Rook: not horizontal or vertical.");
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the rook from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the rook.</param>
        /// <param name="chessboard">The chessboard on which the rook is placed.</param>
        /// <returns>A list of possible moves for the rook.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            // Possible directions for the rook's movement.
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0) };  // Top, Bottom, Left, Right

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
                            {
                                break; // Stop if blocked by a piece of the same color.
                            }
                        }
                    }
                    else
                    {
                        break; // Stop if out of bounds.
                    }
                }
            }

            return result;
        }
    }
}
