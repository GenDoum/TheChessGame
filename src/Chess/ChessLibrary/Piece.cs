using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Class that represents a piece
    /// </summary>
    
    public abstract class Piece
    {
        /// <summary>
        /// Property that represents the color of the piece
        /// </summary>
        public Color Color { get; private set; }

        public int id { get; private set; }
        
        /// <summary>
        /// Property that represents if the piece has moved
        /// </summary>
        public bool Moved { get; protected set; }
        
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="color"></param>
        /// <param name="c"></param>
        public Piece(Color color, int indentifiant)
        {
            Color = color;
            id = indentifiant;
        }
        
        /// <summary>
        /// Method that checks if the piece can move
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public abstract bool canMove(int x, int y, int x2, int y2);

        public List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            List<Case> possibleMoves = new List<Case>();
            int x = caseInitial.Column;
            int y = caseInitial.Line;

            // Check all four diagonal directions
            int[][] directions = new int[][] { new int[] { -1, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 1, 1 } };
            foreach (var direction in directions)
            {
                for (int i = 1; i < 8; i++)
                {
                    // Calculate the new position
                    int newX = x + i * direction[0];
                    int newY = y + i * direction[1];

                    // Check if the new position is within the board
                    if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                    {
                        Case newCase = chessboard.Board[newX, newY];

                        // If the square is empty, add it to the possible moves
                        if (newCase.CaseIsFree)
                        {
                            possibleMoves.Add(newCase);
                        }
                        else
                        {
                            // If the square contains a piece of the opposite color, add it to the possible moves
                            if (newCase.Piece.Color != this.Color)
                            {
                                possibleMoves.Add(newCase);
                            }

                            // Break the loop since we can't jump over the piece
                            break;
                        }
                    }
                    else
                    {
                        // Break the loop since the new position is outside the board
                        break;
                    }
                }
            }

            return possibleMoves;
        }
        
        /// <summary>
        /// Method that checks if the piece is eaten
        /// </summary>
        /// <returns></returns>
        public bool isKilled()
        {
            return false;
        }
        
    }
}