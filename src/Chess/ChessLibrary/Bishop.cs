using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the bishop piece in chess.
    /// </summary>
    [DataContract(Name = "Bishop")]
    public class Bishop : Piece
    {
        /// <summary>
        /// Constructor for the Bishop class.
        /// </summary>
        /// <param name="color">The color of the bishop.</param>
        /// <param name="id">The unique identifier for this bishop.</param>
        public Bishop(Color color, int id) : base(color, id)
        {
            // Sets the image path based on the color.
            ImagePath = color == Color.White ? "fou.png" : "fou_b.png";
        }

        /// <summary>
        /// Determines if the bishop can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the bishop.</param>
        /// <param name="y">The current y-coordinate of the bishop.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is not diagonal or the destination is out of bounds.</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the move is diagonal.
            if (Math.Abs(x - x2) != Math.Abs(y - y2))
                throw new InvalidMovementException("Invalid move for Bishop: not diagonal.");

            // Ensure the destination is within the board limits.
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                throw new InvalidMovementException("Invalid move for Bishop: destination out of bounds.");

            return true;
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the bishop from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the bishop.</param>
        /// <param name="chessboard">The chessboard on which the bishop is placed.</param>
        /// <returns>A list of possible moves for the bishop.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(caseInitial);
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            // Possible diagonal directions for a bishop.
            (int colInc, int lineInc)[] directions = { (-1, 1), (1, 1), (-1, -1), (1, -1) };

            foreach (var (colInc, lineInc) in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    int newColumn = caseInitial.Column + (colInc * i);
                    int newLine = caseInitial.Line + (lineInc * i);
                    // Check if the new position is within the board limits.
                    if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                    {
                        Case? potentialCase = chessboard.Board[newColumn, newLine];
                        if (CanMove(caseInitial.Column, caseInitial.Line, newColumn, newLine))
                        {
                            // Add the move if it's a valid move.
                            if (FactPossibleMove(potentialCase))
                            {
                                result.Add(potentialCase);
                            }
                            else
                                break; // Stop if the path is blocked.
                        }
                        else
                            break; // Stop if the move is not valid.
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Determines if a move to a given case is possible.
        /// </summary>
        /// <param name="potentialCase">The case to check.</param>
        /// <returns>Returns true if the move is possible; otherwise, false.</returns>
        private bool FactPossibleMove(Case? potentialCase)
        {
            // Check if the case is empty or contains an opponent's piece.
            if (potentialCase!.IsCaseEmpty())
            {
                return true;
            }
            else if (!potentialCase.IsCaseEmpty() && potentialCase.Piece!.Color != this.Color)
            {
                return true;
            }
            else
                return false;
        }
    }
}
