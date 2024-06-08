using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Represents the king piece in chess.
    /// </summary>
    [DataContract(Name = "King")]
    public class King : Piece, IFirstMove
    {
        /// <summary>
        /// Gets or sets a value indicating whether the king has made its first move.
        /// </summary>
        [DataMember]
        public bool FirstMove { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="King"/> class.
        /// </summary>
        /// <param name="color">The color of the king.</param>
        /// <param name="id">The unique identifier for this king.</param>
        public King(Color color, int id) : base(color, id)
        {
            // Initially, the king has not moved.
            this.FirstMove = true;
            // Sets the image path based on the color.
            ImagePath = color == Color.White ? "roi.png" : "roi_b.png";
        }

        /// <summary>
        /// Determines if the king can move to the specified coordinates.
        /// </summary>
        /// <param name="x">The current x-coordinate of the king.</param>
        /// <param name="y">The current y-coordinate of the king.</param>
        /// <param name="x2">The x-coordinate of the destination.</param>
        /// <param name="y2">The y-coordinate of the destination.</param>
        /// <returns>Returns true if the move is valid; otherwise, throws an exception.</returns>
        /// <exception cref="InvalidMovementException">Thrown when the move is out of bounds or not a valid king move.</exception>
        public override bool CanMove(int x, int y, int x2, int y2)
        {
            // Check if the move is within one square in any direction.
            if (Math.Abs(x - x2) <= 1 && Math.Abs(y - y2) <= 1)
            {
                // Ensure the destination is within the board limits.
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                {
                    throw new InvalidMovementException("Invalid move for King: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidMovementException("Invalid move for King");
        }

        /// <summary>
        /// Generates a list of all possible legal moves for the king from the given position.
        /// </summary>
        /// <param name="caseInitial">The initial case of the king.</param>
        /// <param name="chessboard">The chessboard on which the king is placed.</param>
        /// <returns>A list of possible moves for the king.</returns>
        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            // Possible directions for the king's movement.
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial!.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                // Check if the new position is within the board limits.
                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];

                    // Add the move if the square is empty or contains an opponent's piece.
                    if (potentialCase!.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        // Ensure the move does not place the king in check.
                        if (!chessboard.Echec(this, new Case(newColumn, newLine, this)))
                        {
                            result.Add(potentialCase);
                        }
                    }
                }
            }

            // Add castling moves if the king has not moved and is not in check.
            if (!chessboard.Echec(this, caseInitial) && FirstMove)
            {
                int row = this.Color == Color.White ? 7 : 0;
                result.Add(chessboard.Board[7, row]);  // Add G1/G8 as a possible move for kingside castling.
                result.Add(chessboard.Board[0, row]);  // Add C1/C8 as a possible move for queenside castling.
            }

            return result;
        }


        /// <summary>
        /// Determines the list of possible pieces that the king can capture from its current position.
        /// </summary>
        /// <param name="caseInitial">The current position of the king.</param>
        /// <param name="chessboard">The chessboard on which the king is placed.</param>
        /// <returns>A list of cases where the king can capture an opponent's piece.</returns>
        public List<Case?> CanEat(Case? caseInitial, Chessboard chessboard)
        {
            List<Case?> result = new List<Case?>();
            // Possible directions for the king's movement.
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bottom, Left, Right, Top Left, Top Right, Bottom Left, Bottom Right

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial!.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                // Check if the new position is within the board limits.
                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];

                    // Check if the square is empty or contains an opponent's piece.
                    if (potentialCase!.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        // Find the position of the king after the move.
                        Case kingNewPosition = new Case(newColumn, newLine, this);
                        result.Add(kingNewPosition);
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Performs kingside castling if conditions are met.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the king is placed.</param>
        /// <exception cref="ArgumentException">Thrown when castling conditions are not met.</exception>
        public void PetitRoque(Chessboard chessboard)
        {
            // Verify if the king has already moved.
            if (!FirstMove)
                throw new ArgumentException("King has already moved");

            int row = Color == Color.White ? 7 : 0;  // Row for white or black kings.

            // Verify if the rook at the initial position H1/H8 has not moved.
            if (chessboard.Board[7, row]!.Piece is Rook rook && rook.FirstMove)
            {
                // Verify if the squares between the king and rook are empty.
                if (chessboard.Board[5, row]!.IsCaseEmpty() && chessboard.Board[6, row]!.IsCaseEmpty())
                {
                    // Verify if the squares the king traverses are not under attack.
                    if (!chessboard.Echec(this, chessboard.Board[5, row]) && !chessboard.Echec(this, chessboard.Board[6, row]))
                    {
                        // Perform castling.
                        chessboard.ProcessPostMove(chessboard.Board[6, row], chessboard.Board[4, row]);  // Move king (E1/E8 to G1/G8).
                        chessboard.ProcessPostMove(chessboard.Board[5, row], chessboard.Board[7, row]);  // Move rook (H1/H8 to F1/F8).

                        // Update positions on the board.
                        chessboard.Board[6, row]!.Piece = this;
                        chessboard.Board[4, row]!.Piece = null;  // Clear the king's old position.
                        chessboard.Board[5, row]!.Piece = rook;
                        chessboard.Board[7, row]!.Piece = null;  // Clear the rook's old position.

                        // Indicate that the king and rook have moved.
                        FirstMove = false;
                        rook.FirstMove = false;
                    }
                    else
                        throw new ArgumentException("Kingside castling not possible: squares the king traverses are attacked");
                }
                else
                    throw new ArgumentException("Kingside castling not possible: squares between king and rook are not empty");
            }
            else
                throw new ArgumentException("Kingside castling not possible: the rook has already moved");
        }

        /// <summary>
        /// Performs queenside castling if conditions are met.
        /// </summary>
        /// <param name="chessboard">The chessboard on which the king is placed.</param>
        /// <exception cref="ArgumentException">Thrown when castling conditions are not met.</exception>
        public void GrandRoque(Chessboard chessboard)
        {
            // Verify if the king has already moved.
            if (!FirstMove)
                throw new ArgumentException("King has already moved");

            int row = Color == Color.White ? 7 : 0;  // Row for white or black kings.

            // Verify if the rook at the initial position A1/A8 has not moved.
            if (chessboard.Board[0, row]!.Piece is Rook rook && rook.FirstMove)
            {
                // Verify if the squares between the king and rook are empty.
                if (chessboard.Board[1, row]!.IsCaseEmpty() && chessboard.Board[2, row]!.IsCaseEmpty() && chessboard.Board[3, row]!.IsCaseEmpty())
                {
                    // Verify if the squares the king traverses are not under attack.
                    if (!chessboard.Echec(this, chessboard.Board[1, row]) && !chessboard.Echec(this, chessboard.Board[2, row]) && !chessboard.Echec(this, chessboard.Board[3, row]))
                    {
                        // Perform castling.
                        chessboard.ProcessPostMove(chessboard.Board[2, row], chessboard.Board[4, row] );  // Move king (E1/E8 to C1/C8).
                        chessboard.ProcessPostMove(chessboard.Board[3, row],chessboard.Board[0, row]);  // Move rook (A1/A8 to D1/D8).

                        // Update positions on the board.
                        chessboard.Board[2, row]!.Piece = this;
                        chessboard.Board[4, row]!.Piece = null;  // Clear the king's old position.
                        chessboard.Board[3, row]!.Piece = rook;
                        chessboard.Board[0, row]!.Piece = null;  // Clear the rook's old position.

                        // Indicate that the king and rook have moved.
                        FirstMove = false;
                        rook.FirstMove = false;
                    }
                    else
                        throw new ArgumentException("Queenside castling not possible: squares the king traverses are attacked");
                }
                else
                    throw new ArgumentException("Queenside castling not possible: squares between king and rook are not empty");
            }
            else
                throw new ArgumentException("Queenside castling not possible: the rook has already moved");
        }
    }
}