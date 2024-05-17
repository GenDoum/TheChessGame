using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Classe représentant le roi
    /// </summary>
    public class King : Piece, IFirstMove
    {
        public bool FirstMove { get; set; }
        /// <summary>
        /// Constructeur de la classe King
        /// </summary>
        /// <param name="color"></param>
        /// <param name="id"></param>
        public King(Color color, int id) : base(color, id)
        {
            this.FirstMove = true;
        }


        public override bool CanMove(int x, int y, int x2, int y2)
        {
            if (Math.Abs(x - x2) <= 1 && Math.Abs(y - y2) <= 1)
            {
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                {
                    throw new InvalidOperationException("Invalid move for King: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidOperationException("Invalid move for King");
        }

 
        public override List<Case> PossibleMoves(Case caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right, Top Left, Top Right, Bot Left, Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];

                    // Vérifiez si la case est vide ou contient une pièce ennemie
                    if (potentialCase.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        // Trouver la position du roi après le déplacement
                        Case kingNewPosition = new Case(newColumn, newLine, this);

                        // Empêcher le roi de se déplacer dans une position attaquée par un autre roi
                        if (potentialCase.Piece is King && potentialCase.Piece.Color != this.Color)
                        {
                            continue; // Ignorez ce mouvement si c'est une case attaquée par un autre roi
                        }

                        if (!chessboard.Echec(this, kingNewPosition))
                        {
                            result.Add(potentialCase);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Vérifiez si le roi peut manger une pièce ennemie
        /// </summary>
        /// <param name="caseInitial"></param>
        /// <param name="chessboard"></param>
        /// <returns></returns>
        public List<Case> CanEat(Case caseInitial, Chessboard chessboard)
        {
            List<Case> result = new List<Case>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right, Top Left, Top Right, Bot Left, Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case potentialCase = chessboard.Board[newColumn, newLine];

                    // Vérifiez si la case est vide ou contient une pièce ennemie
                    if (potentialCase.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        // Trouver la position du roi après le déplacement
                        Case kingNewPosition = new Case(newColumn, newLine, this);
                        result.Add(kingNewPosition);
                    }
                }
            }
            return result;
        }
    }
}