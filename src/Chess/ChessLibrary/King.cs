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
            (int, int)[] directions = { (0, 1), (0, -1), (-1, 0), (1, 0), (-1, 1), (1, 1), (-1, -1), (1, -1) };

            foreach (var (colInc, lineInc) in directions)
            {
                int newColumn = caseInitial!.Column + colInc;
                int newLine = caseInitial.Line + lineInc;

                if (newColumn >= 0 && newColumn < 8 && newLine >= 0 && newLine < 8)
                {
                    Case? potentialCase = chessboard.Board[newColumn, newLine];

                    if (potentialCase!.IsCaseEmpty() || (potentialCase.Piece != null && potentialCase.Piece.Color != this.Color))
                    {
                        if (!chessboard.Echec(this, new Case(newColumn, newLine, this)))
                        {
                            result.Add(potentialCase);
                        }
                    }
                }
            }
            if (!chessboard.Echec(this,caseInitial)) {
                int row = this.Color == Color.White ? 7 : 0;
                result.Add(chessboard.Board[7, row]);  // Ajoute G1/G8 comme mouvement de petit roque
                result.Add(chessboard.Board[0, row]);  // Ajoute C1/C8 comme mouvement de grand roque
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
            if (!FirstMove)
                throw new ArgumentException("Le roi a déjà bougé");

            int row = Color == Color.White ? 7 : 0; // Rangée pour les rois blancs ou noirs
                                                         // Vérifier si la tour à la position initiale H8 n'a pas bougé
            if (chessboard.Board[7, row]!.Piece is Rook rook && rook.FirstMove)
            {
                // Vérifier si les cases entre le roi et la tour sont libres
                if (chessboard.Board[5, row]!.IsCaseEmpty() && chessboard.Board[6, row]!.IsCaseEmpty())
                {
                    // Vérifier si les cases que le roi traverse ne sont pas attaquées
                    if (!chessboard.Echec(this, chessboard.Board[5, row]) && !chessboard.Echec(this, chessboard.Board[6, row]))
                    {
                        // Effectuer le roque
                        chessboard.ProcessPostMove(chessboard.Board[4, row], chessboard.Board[6, row]); // Déplacement du roi (E1/E8 -> G1/G8)
                        chessboard.ProcessPostMove(chessboard.Board[7, row], chessboard.Board[5, row]); // Déplacement de la tour (H1/H8 -> F1/F8)

                        // Mettre à jour les positions sur l'échiquier
                        chessboard.Board[6, row]!.Piece = this;
                        chessboard.Board[4, row]!.Piece = null; // Ancienne position du roi
                        chessboard.Board[5, row]!.Piece = rook;
                        chessboard.Board[7, row]!.Piece = null; // Ancienne position de la tour

                        // Indiquer que le roi et la tour ont bougé
                        FirstMove = false;
                        rook.FirstMove = false;
                    }
                    else
                        throw new ArgumentException("Petit roque impossible, les cases que le roi traverse sont attaquées");
                }
                else
                    throw new ArgumentException("Petit roque impossible, les cases entre le roi et la tour ne sont pas libres");
            }
            else
                throw new ArgumentException("Petit roque impossible,la tour a deja bougé");
        }

        public void GrandRoque(Chessboard chessboard)
        {
            if (!FirstMove)
                throw new ArgumentException("Le roi a déjà bougé");

            int row = Color == Color.White ? 7 : 0; // Rangée pour les rois blancs ou noirs
                                                    // Vérifier si la tour à la position initiale H8 n'a pas bougé
            if (chessboard.Board[0, row]!.Piece is Rook rook && rook.FirstMove)
            {
                // Vérifier si les cases entre le roi et la tour sont libres
                if (chessboard.Board[1, row]!.IsCaseEmpty() && chessboard.Board[2, row]!.IsCaseEmpty() && chessboard.Board[3,row]!.IsCaseEmpty())
                {
                    // Vérifier si les cases que le roi traverse ne sont pas attaquées
                    if (!chessboard.Echec(this, chessboard.Board[1, row]) && !chessboard.Echec(this, chessboard.Board[2, row]) && !chessboard.Echec(this, chessboard.Board[3, row]))
                    {
                        // Effectuer le roque
                        chessboard.ProcessPostMove(chessboard.Board[4, row], chessboard.Board[2, row]); // Déplacement du roi (E1/E8 -> G1/G8)
                        chessboard.ProcessPostMove(chessboard.Board[0, row], chessboard.Board[3, row]); // Déplacement de la tour (H1/H8 -> F1/F8)

                        // Mettre à jour les positions sur l'échiquier
                        chessboard.Board[2, row]!.Piece = this;
                        chessboard.Board[4, row]!.Piece = null; // Ancienne position du roi
                        chessboard.Board[3, row]!.Piece = rook;
                        chessboard.Board[7, row]!.Piece = null; // Ancienne position de la tour

                        // Indiquer que le roi et la tour ont bougé
                        FirstMove = false;
                        rook.FirstMove = false;
                    }
                    else
                        throw new ArgumentException("Petit roque impossible, les cases que le roi traverse sont attaquées");
                }
                else
                    throw new ArgumentException("Petit roque impossible, les cases entre le roi et la tour ne sont pas libres");
            }
            else
                throw new ArgumentException("Petit roque impossible,la tour a deja bougé");
        }
    }
}