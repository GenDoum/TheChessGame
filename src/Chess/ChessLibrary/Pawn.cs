using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the pawn piece in chess.
    /// </summary>
    [DataContract(Name = "Pawn")]
    public class Pawn : Piece, IFirstMove
    {
        /// <summary>
        /// Indicates whether the pawn has made its first move.
        /// </summary>
        [DataMember]
        public virtual bool FirstMove { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pawn"/> class.
        /// </summary>
        /// <param name="c">The color of the pawn.</param>
        /// <param name="id">The unique identifier for this pawn.</param>
        public Pawn(Color c, int id) : base(c, id)
        {
            // Sets the image path based on the color.
            ImagePath = c == Color.White ? "pion.png" : "pion_b.png";
            this.FirstMove = true;
        }

        /// <summary>
        /// Determines if the pawn can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the pawn.</param>
        /// <param name="y">The current y-coordinate of the pawn.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is out of bounds or not a valid pawn move.</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the destination is within the board limits.
            if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                throw new InvalidMovementException("Invalid move for Pawn: destination out of bounds.");

            int direction = (Color == Color.White) ? 1 : -1;

            // Check for normal forward movement or initial double step.
            if (x == x2 && (y2 == y + direction || (Color == Color.White && y == 1 || Color == Color.Black && y == 6) && y2 == y + 2 * direction))
                return true;

            // Check for diagonal captures.
            if (Math.Abs(x2 - x) == 1 && y2 == y + direction)
                return true;

            throw new InvalidMovementException("Invalid move for Pawn: not diagonal or forward.");
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the pawn from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the pawn.</param>
        /// <param name="chessboard">The chessboard on which the pawn is placed.</param>
        /// <returns>A list of possible moves for the pawn.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            int direction = this.Color == Color.White ? -1 : +1; // White moves up (-1), Black moves down (+1)

            // Add normal moves (1 or 2 squares forward).
            AddNormalMoves(caseInitial, chessboard, direction, result);

            // Add diagonal captures (left and right).
            AddDiagonalCaptures(caseInitial, chessboard, direction, result);

            return result;
        }

        /// <summary>
        /// Adds normal movement possibilities for the pawn.
        /// </summary>
        /// <param name="caseInitial">The initial position of the pawn.</param>
        /// <param name="chessboard">The chessboard on which the moves are being considered.</param>
        /// <param name="direction">The direction of the pawn's movement (1 for down, -1 for up).</param>
        /// <param name="result">The list to add possible moves to.</param>
        private void AddNormalMoves(Case? caseInitial, Chessboard chessboard, int direction, List<Case?> result)
        {
            for (int i = 1; i <= (FirstMove ? 2 : 1); i++)
            {
                int newLine = caseInitial!.Line + direction * i;
                int newColumn = caseInitial.Column;
                // Check if the move is within the board boundaries.
                if (IsWithinBoard(newLine, newColumn, chessboard))
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];
                    // Add the move if the square is empty; stop if blocked.
                    if (potentialCase!.IsCaseEmpty())
                    {
                        result.Add(potentialCase);
                    }
                    else
                    {
                        break; // Blockage by another piece, stop here.
                    }
                }
            }
        }

        /// <summary>
        /// Adds diagonal capture possibilities for the pawn.
        /// </summary>
        /// <param name="caseInitial">The initial position of the pawn.</param>
        /// <param name="chessboard">The chessboard on which the moves are being considered.</param>
        /// <param name="direction">The direction of the pawn's movement (1 for down, -1 for up).</param>
        /// <param name="result">The list to add possible moves to.</param>
        private void AddDiagonalCaptures(Case? caseInitial, Chessboard chessboard, int direction, List<Case?> result)
        {
            int[] captureColumns = new int[] { caseInitial!.Column - 1, caseInitial.Column + 1 };
            foreach (int col in captureColumns)
            {
                int newLine = caseInitial.Line + direction;
                // Check if the diagonal move is within board limits.
                if (IsWithinBoard(newLine, col, chessboard))
                {
                    Case? potentialCase = chessboard.Board[col, newLine];
                    // Add the move if the square has an opposing piece.
                    if (potentialCase!.Piece != null && !potentialCase.IsCaseEmpty() && potentialCase.Piece.Color != this.Color)
                    {
                        result.Add(potentialCase);
                    }
                }
            }
        }

        /// <summary>
        /// Helper method to check if coordinates are within the chessboard limits.
        /// </summary>
        /// <param name="line">The line (y-coordinate) to check.</param>
        /// <param name="column">The column (x-coordinate) to check.</param>
        /// <param name="chessboard">The chessboard.</param>
        /// <returns>True if within bounds, false otherwise.</returns>
        private static bool IsWithinBoard(int line, int column, Chessboard chessboard)
        {
            return line >= 0 && line < chessboard.Board.GetLength(1) && column >= 0 && column < chessboard.Board.GetLength(0);
        }

        /// <summary>
        /// Determines the list of possible pieces that the pawn can capture from its current position.
        /// </summary>
        /// <param name="caseInitial">The current position of the pawn.</param>
        /// <param name="chessboard">The chessboard on which the pawn is placed.</param>
        /// <returns>A list of cases where the pawn can capture an opponent's piece.</returns>
        public List<Case?> CanEat(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);
            List<Case?> result = new List<Case?>();
            int direction = this.Color == Color.White ? -1 : +1; // White moves up (-1), Black moves down (+1)
            int[] captureColumns = new int[] { caseInitial!.Column - 1, caseInitial.Column + 1 };
            foreach (int col in captureColumns)
            {
                int newLine = caseInitial.Line + direction;
                // Check if the diagonal move is within board limits.
                if (IsWithinBoard(newLine, col, chessboard))
                {
                    Case? potentialCase = chessboard.Board[col, newLine];
                    // Add the move if the square has an opposing piece.
                    if (!potentialCase!.IsCaseEmpty() && potentialCase.Piece!.Color != this.Color)
                    {
                        result.Add(potentialCase);
                    }
                }
            }
            return result;
        }
    }
}
