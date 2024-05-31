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
            ImagePath = color == Color.White ? "roi.png" : "roi_b.png";
        }


        public override bool CanMove(int x, int y, int x2, int y2)
        {
            if (Math.Abs(x - x2) <= 1 && Math.Abs(y - y2) <= 1)
            {
                if (x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7)
                {
                    throw new InvalidMovementException("Invalid move for King: destination out of bounds.");
                }
                return true;
            }

            throw new InvalidMovementException("Invalid move for King");
        }


        public override List<Case?> PossibleMoves(Case? caseInitial, Chessboard chessboard)
        {
            ArgumentNullException.ThrowIfNull(chessboard);

            List<Case?> result = new List<Case?>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right, Top Left, Top Right, Bot Left, Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial!.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];

                    // Vérifiez si la case est vide ou contient une pièce ennemie
                    if (potentialCase!.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
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
        public List<Case?> CanEat(Case? caseInitial, Chessboard chessboard)
        {
            List<Case?> result = new List<Case?>();
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };  // Top, Bot, Left, Right, Top Left, Top Right, Bot Left, Bot Right

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial!.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];

                    // Vérifiez si la case est vide ou contient une pièce ennemie
                    if (potentialCase!.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        // Trouver la position du roi après le déplacement
                        Case kingNewPosition = new Case(newColumn, newLine, this);
                        result.Add(kingNewPosition);
                    }
                }
            }
            return result;
        }



        public void PetitRoque(Chessboard chessboard)
        {
            // Vérifier si le roi a déjà bougé
            if (!this.FirstMove)
                return;

            // Petit roque pour le roi blanc
            if (this.Color == Color.White)
            {
                // Vérifier si la tour à la position initiale H1 n'a pas bougé
                if (chessboard.Board[7, 7].Piece is Rook rook && rook.FirstMove)
                {
                    // Vérifier si les cases entre le roi et la tour sont libres
                    if (chessboard.Board[5, 7].IsCaseEmpty() && chessboard.Board[6, 7].IsCaseEmpty())
                    {
                        // Vérifier si les cases que le roi traverse ne sont pas attaquées
                        if (!chessboard.Echec(this, chessboard.Board[5, 7]) && !chessboard.Echec(this, chessboard.Board[6, 7]))
                        {
                            // Effectuer le roque
                            /*chessboard.*/
                            chessboard.Board[6, 7].Piece = this;
                            chessboard.Board[4, 7].Piece = null; // Ancienne position du roi
                            chessboard.Board[5, 7].Piece = rook;
                            chessboard.Board[7, 7].Piece = null; // Ancienne position de la tour
                            this.FirstMove = false;
                            rook.FirstMove = false;
                        }
                    }
                }
            }
            // Répétez pour le roi noir à partir de la position initiale E8 vers la tour à A8
            // (les conditions doivent être ajustées pour le côté noir du plateau)
        }
    }
}