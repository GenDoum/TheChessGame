using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{


    public class Chessboard : IBoard
    {
        public delegate void EvolveHandler(out string result);
        public event EvolveHandler OnEvolve;
        public void SetupEvolveEventHandler()
        {
            OnEvolve += DisplayEvolveOptions;
        }

        private void DisplayEvolveOptions(out string result)
        {
            Console.WriteLine("Entrez 1 pour changer votre pion en Reine");
            Console.WriteLine("Entrez 2 pour changer votre pion en Tour");
            Console.WriteLine("Entrez 3 pour changer votre pion en Fou");
            Console.WriteLine("Entrez 4 pour changer votre pion en Chavalier");
            result = Console.ReadLine();
        }

        public Case[,] Board { get; private set; }
        public List<CoPieces>? WhitePieces { get; private set; }
        public List<CoPieces>? BlackPieces { get; private set; }

        /// <summary>
        /// Create a new chessboard 
        /// </summary>
        /// <param name="Tcase"></param>
        /// <param name="isEmpty"></param>
        public Chessboard(Case[,] Tcase, bool isEmpty)
        {
            Board = Tcase;
            WhitePieces = new List<CoPieces>();
            BlackPieces = new List<CoPieces>();

            if (!isEmpty)
            {
                // Create a chessboard with all pieces at their initial position
                InitializeChessboard();
            }
            else
            {
                // Create an empty chessboard
                InitializeEmptyBoard();
            }
        }
        /// <summary>
        /// Initialize an empty chessboard.
        /// </summary>
        public void InitializeEmptyBoard()
        {
            for (int column = 0; column < 8; column++)
            // Loop through all rows
            {
                for (int row = 0; row < 8; row++)
                // loop through all columns
                {
                    // initialize the board with empty cases
                    Board[column, row] = new Case(column, row, null);
                }
            }
        }

        /// <summary>
        /// Initialize a chessboard with all pieces at their initial position,using multiple functions to initialize each type of pieces.
        /// </summary>
        public void InitializeChessboard()
        {
            // Call the functions to initialize the white pieces
            InitializeWhitePieces();
            // Call the functions to initialize the black pieces
            InitializeBlackPieces();
            // Fill the empty cases of the chessboard with null pieces
            FillEmptyCases();
        }

        /// <summary>
        /// Initialize all white pieces at their initial position.
        /// </summary>
        public void InitializeWhitePieces()
        {
            int identifiantBlanc = 1;
            // Add the white pieces to the chessboard and to the list of white pieces
            AddPiece(new Rook(Color.White, identifiantBlanc++), 0, 0);
            AddPiece(new Knight(Color.White, identifiantBlanc++), 1, 0);
            AddPiece(new Bishop(Color.White, identifiantBlanc++), 2, 0);
            AddPiece(new Queen(Color.White, identifiantBlanc++), 3, 0);
            AddPiece(new King(Color.White, identifiantBlanc++), 4, 0);
            AddPiece(new Bishop(Color.White, identifiantBlanc++), 5, 0);
            AddPiece(new Knight(Color.White, identifiantBlanc++), 6, 0);
            AddPiece(new Rook(Color.White, identifiantBlanc++), 7, 0);

            for (int c = 0; c < 8; c++)
            {
                // Add the white pawns to the chessboard and to the list of white pieces
                AddPiece(new Pawn(Color.White, identifiantBlanc++), c, 1);
            }
        }

        /// <summary>
        /// Initialize all black pieces at their initial position.
        /// </summary>
        public void InitializeBlackPieces()
        {
            int identifiantNoir = 1;
            // Add the black pieces to the chessboard and to the list of black pieces
            AddPiece(new Rook(Color.Black, identifiantNoir++), 0, 7);
            AddPiece(new Knight(Color.Black, identifiantNoir++), 1, 7);
            AddPiece(new Bishop(Color.Black, identifiantNoir++), 2, 7);
            AddPiece(new Queen(Color.Black, identifiantNoir++), 3, 7);
            AddPiece(new King(Color.Black, identifiantNoir++), 4, 7);
            AddPiece(new Bishop(Color.Black, identifiantNoir++), 5, 7);
            AddPiece(new Knight(Color.Black, identifiantNoir++), 6, 7);
            AddPiece(new Rook(Color.Black, identifiantNoir++), 7, 7);

            for (int c = 0; c < 8; c++)
            {
                // Add the black pawns to the chessboard and to the list of black pieces
                AddPiece(new Pawn(Color.Black, identifiantNoir++), c, 6);
            }
        }

        /// <summary>
        /// Fill the empty cases of the chessboard with null pieces.
        /// </summary>
        public void FillEmptyCases()
        {
            for (int row = 2; row <= 5; row++)
            // Loop through all rows
            {
                for (int column = 0; column < 8; column++)
                // loop through all columns
                {
                    // Fill the empty cases of the chessboard with null pieces
                    Board[column, row] = new Case(column, row, null);
                }
            }
        }


        /// <summary>
        /// Add a piece to the chessboard and to the list of pieces of the same color.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void AddPiece(Piece piece, int column, int row)
        {
            // Add the piece to the chessboard
            Board[column, row] = new Case(column, row, piece);
            if (piece.Color == Color.White)
            {
                // Add the piece to the list of white pieces
                WhitePieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
            else
            {
                // Add the piece to the list of black pieces
                BlackPieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
        }


        /// <summary>
        /// Check if the move is valid by checking if the final case is in the list of possible moves.
        /// </summary>
        /// <param name="Lcase"></param>
        /// <param name="Final"></param>
        /// <returns> Bool True if the move is in the list of possible move Valid,false if is not</returns>
        public bool IsMoveValid(List<Case> Lcase, Case Final)
        {
            return Lcase.Any(i => i.Column == Final.Column && i.Line == Final.Line);
        }


        /// <summary>
        /// Create a List of possible moves and call the function IsMoveValid to check if the move is valid.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="Initial"></param>
        /// <param name="Final"></param>
        /// <returns>Bool True if we can move the piece</returns>
        public bool MovePiece(Piece piece, Case Initial, Case Final)
        {
            List<Case> L = piece.PossibleMoves(Initial, this);
            // Create a list of possible moves
            return IsMoveValid(L, Final);
        }

        /// <summary>
        /// Check if a Pawn can be evolved and call the function Evolve to change the Pawn to another piece.
        /// </summary>
        /// <returns>Bool,True if a Pawn can Evolved , False if not</returns>
        public bool PawnCanEvolve()
        {
            //Check if pawn is on line 8 and if he is white to processes for the evolved
            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 7].Piece is Pawn && Board[col, 7].Piece.Color == Color.White)
                { //Check if pawn is on line 1 and if he is white to processes for the evolved
                    Evolve(Board[col, 7].Piece as Pawn, Board[col, 7]);
                    return true;
                }
            }

            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 0].Piece is Pawn && Board[col, 0].Piece.Color == Color.Black)
                {//Check if pawn is on line 1 and if he is black to processes for the evolved
                    Evolve(Board[col, 0].Piece as Pawn, Board[col, 0]);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Change a Pawn to another piece.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        public void Evolve(Pawn P, Case C)
        {
            string result = string.Empty;
            // Déclencher l'événement pour obtenir l'entrée de l'utilisateur
            OnEvolve?.Invoke(out result);

            Queen NewQueen;
            Rook NewRook;
            Knight NewKnight;
            Bishop NewBishop;

            while (true)
            {
                switch (result)
                {
                    case "1":
                        NewQueen = new Queen(P.Color, P.id);
                        C.Piece = NewQueen;
                        ModifPawn(P, NewQueen, C);
                        return;

                    case "2":
                        NewRook = new Rook(P.Color, P.id);
                        C.Piece = NewRook;
                        ModifPawn(P, NewRook, C);
                        return;
                    case "3":
                        NewBishop = new Bishop(P.Color, P.id);
                        C.Piece = NewBishop;
                        ModifPawn(P, NewBishop, C);
                        return;
                    case "4":
                        NewKnight = new Knight(P.Color, P.id);
                        C.Piece = NewKnight;
                        ModifPawn(P, NewKnight, C);
                        return;
                    default:
                        OnEvolve?.Invoke(out result);
                        break;
                }
            }
        }


        /// <summary>
        /// Modify the list of pieces when a Pawn is evolved,add the new piece and remove the Pawn.
        /// </summary>
        /// <param name="P"></param>
        /// <param name="pi"></param>
        /// <param name="c"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ModifPawn(Pawn P, Piece pi, Case c)
        {
            ArgumentNullException.ThrowIfNull(pi);
            ArgumentNullException.ThrowIfNull(P);

            if (pi.Color == Color.White)
            {
                //Add the new piece to the list of white pieces and remove the Pawn
                this.WhitePieces.Add(new CoPieces { CaseLink = c, piece = pi });
                this.WhitePieces.Remove(new CoPieces { CaseLink = c, piece = P });

            }

            else
            {
                //Add the new piece to the list of black pieces and remove the Pawn
                this.BlackPieces.Add(new CoPieces { CaseLink = c, piece = pi });
                this.BlackPieces.Remove(new CoPieces { CaseLink = c, piece = P });
            }

        }


        //To do for create class King
        /// <summary>
        /// Check if the King is in check position.
        /// </summary>
        /// <param name="myKing"></param>
        /// <param name="KingCase"></param>
        /// <returns></returns>

        public bool Echec(King king, Case kingCase)
        {
            List<CoPieces> enemyPieces = (king.Color == Color.White) ? BlackPieces : WhitePieces;

            foreach (var enemy in enemyPieces)
            {
                if (enemy.piece is King)
                {
                    continue; // Ignorez les mouvements du roi ennemi pour éviter les boucles infinies
                }

                // Utilisez les mouvements possibles de l'ennemi pour vérifier s'ils attaquent la case du roi
                var possibleMoves = enemy.piece.PossibleMoves(enemy.CaseLink, this);
                foreach (var move in possibleMoves)
                {
                    if (move.Column == kingCase.Column && move.Line == kingCase.Line)
                    {
                        return true;
                    }
                }
            }

            return false; // Le roi n'est pas en échec
        }


        public Chessboard CopyBoard()
        {
            // Création d'une nouvelle grille de cases pour la copie
            Case[,] newBoard = new Case[8, 8];

            // Copier chaque case dans la nouvelle grille
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Assurez-vous de copier également les pièces pour éviter la référence partagée
                    var originalPiece = Board[i, j].Piece;
                    Piece copiedPiece = null;
                    if (originalPiece != null)
                    {
                        // Vous pouvez ajouter une méthode de copie dans chaque classe de pièces ou utiliser une méthode de clone ici
                        copiedPiece = originalPiece;
                    }
                    newBoard[i, j] = new Case(i, j, copiedPiece);
                }
            }

            // Créer un nouvel échiquier avec le tableau de cases copiées
            var newChessboard = new Chessboard(newBoard, true);
            // Copier les listes de pièces
            newChessboard.WhitePieces = new List<CoPieces>(WhitePieces);
            newChessboard.BlackPieces = new List<CoPieces>(BlackPieces);

            return newChessboard;
        }

        /// <summary>
        /// Check if the King is in checkmate position.
        /// </summary>
        /// <param name="Lcase"></param>
        /// <returns></returns>
        public bool EchecMat(King king, Case kingCase)
        {
            // Obtenez tous les mouvements possibles pour le roi
            var possibleKingMoves = king.PossibleMoves(kingCase, this);

            // Vérifiez si le roi peut échapper à l'échec
            foreach (var move in possibleKingMoves)
            {
                var simulatedBoard = CopyBoard();
                simulatedBoard.MovePiece(king, kingCase, simulatedBoard.Board[move.Column, move.Line]);

                // Si le roi n'est pas en échec après ce mouvement, il n'y a pas échec et mat
                if (!simulatedBoard.Echec(king, simulatedBoard.Board[move.Column, move.Line]))
                    return false;
            }

            // Obtenez toutes les pièces alliées
            var allyPieces = (king.Color == Color.White) ? WhitePieces : BlackPieces;

            // Vérifiez si une pièce alliée peut protéger le roi
            foreach (var pieceInfo in allyPieces)
            {
                var piece = pieceInfo.piece;
                var startCase = pieceInfo.CaseLink;
                var possibleMoves = piece.PossibleMoves(startCase, this);

                foreach (var move in possibleMoves)
                {
                    var simulatedBoard = CopyBoard();
                    simulatedBoard.MovePiece(piece, startCase, simulatedBoard.Board[move.Column, move.Line]);

                    // Si le roi n'est pas en échec après ce mouvement, il n'y a pas échec et mat
                    if (!simulatedBoard.Echec(king, simulatedBoard.Board[kingCase.Column, kingCase.Line]))
                        return false;
                }
            }

            // Si aucune évasion possible n'a été trouvée, c'est échec et mat
            return true;
        }



    }
}