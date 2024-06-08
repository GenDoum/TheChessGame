using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace ChessLibrary
{
    [DataContract]
    [KnownType(typeof(CoPieces))]
    /// <summary>
    /// Represents the chessboard in chess.
    /// </summary>
    public class Chessboard : IBoard, INotifyPropertyChanged
    {

        [DataMember]
        public List<Case?> SerializableBoard
        {
            get { return FlatBoard.ToList(); }
            set { Board = ConvertListToBoard(value, 8, 8); }
        }

        /// <summary>
        /// Converts a list of cases to a 2D array representing the board.
        /// </summary>
        /// <param name="list">The list of cases to convert.</param>
        /// <param name="rows">The number of rows in the board.</param>
        /// <param name="columns">The number of columns in the board.</param>
        /// <returns>A 2D array representing the board.</returns>
        public Case?[,] ConvertListToBoard(List<Case?> list, int rows, int columns)
        {
            var array = new Case?[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[j, i] = list[i * columns + j];
                }
            }

            return array;
        }

        private Case?[,] _board;

        /// <summary>
        /// Gets or sets the 2D array representing the board.
        /// </summary>
        public Case?[,] Board
        {
            get { return _board; }
            set
            {
                if (_board != value)
                {
                    _board = value;
                    OnPropertyChanged(nameof(Board));
                }
            }
        }

        /// <summary>
        /// Gets a read-only collection of white pieces.
        /// </summary>
        public ReadOnlyCollection<CoPieces> WhitePieces => _whitePieces.AsReadOnly();

        [DataMember]
        private readonly List<CoPieces> _whitePieces = new List<CoPieces>();

        /// <summary>
        /// Gets a read-only collection of black pieces.
        /// </summary>
        public ReadOnlyCollection<CoPieces> BlackPieces => _blackPieces.AsReadOnly();

        [DataMember]
        private readonly List<CoPieces> _blackPieces = new List<CoPieces>();

        /// <summary>
        /// Event triggered when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool _isCheckingForCheck = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chessboard"/> class.
        /// </summary>
        /// <param name="tcase">The initial state of the board.</param>
        /// <param name="isEmpty">Indicates whether the board should be initialized as empty.</param>
        public Chessboard(Case?[,] tcase, bool isEmpty)
        {
            Board = tcase;

            if (!isEmpty)
            {
                InitializeChessboard();
            }
            else
            {
                InitializeEmptyBoard();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chessboard"/> class without parameters.
        /// </summary>
        public Chessboard()
        {
            Board = new Case?[8, 8];
            InitializeChessboard();
        }


        /// <summary>
        /// Copies the list of white pieces.
        /// </summary>
        /// <returns>A list of copied white pieces.</returns>
        public List<CoPieces> CopyWhitePieces()
        {
            var result = new List<CoPieces>();
            foreach (var copiece in WhitePieces)
            {
                result.Add(new CoPieces { CaseLink = copiece.CaseLink, piece = copiece.piece });
            }
            return result;
        }

        /// <summary>
        /// Copies the list of black pieces.
        /// </summary>
        /// <returns>A list of copied black pieces.</returns>
        public List<CoPieces> CopyBlackPieces()
        {
            var result = new List<CoPieces>();
            foreach (var copiece in BlackPieces)
            {
                result.Add(new CoPieces { CaseLink = copiece.CaseLink, piece = copiece.piece });
            }
            return result;
        }

        /// <summary>
        /// Gets the number of rows on the board.
        /// </summary>
        public int NbRows => Board?.GetLength(0) ?? 0;

        /// <summary>
        /// Gets the number of columns on the board.
        /// </summary>
        public int NbColumns => Board?.GetLength(1) ?? 0;

        /// <summary>
        /// Flattens the board into a list.
        /// </summary>
        public IEnumerable<Case?> FlatBoard
        {
            get
            {
                List<Case?> flatBoard = new();

                for (int j = 0; j < NbColumns; j++)
                {
                    for (int i = 0; i < NbRows; i++)
                    {
                        flatBoard.Add(Board[i, j]);
                    }
                }

                return flatBoard;
            }
        }

        /// <summary>
        /// Initializes an empty board.
        /// </summary>
        public void InitializeEmptyBoard()
        {
            for (int column = 0; column < 8; column++)
            {
                for (int row = 0; row < 8; row++)
                {
                    Board[column, row] = new Case(column, row, null);
                }
            }
        }

        /// <summary>
        /// Initializes the chessboard with pieces in their starting positions.
        /// </summary>
        public void InitializeChessboard()
        {
            InitializeBlackPieces();
            InitializeWhitePieces();
            FillEmptyCases();
        }

        /// <summary>
        /// Initializes the black pieces on the board.
        /// </summary>
        public void InitializeBlackPieces()
        {
            int identifiantBlanc = 1;
            AddPiece(new Rook(Color.Black, identifiantBlanc++), 0, 0);
            AddPiece(new Knight(Color.Black, identifiantBlanc++), 1, 0);
            AddPiece(new Bishop(Color.Black, identifiantBlanc++), 2, 0);
            AddPiece(new Queen(Color.Black, identifiantBlanc++), 3, 0);
            AddPiece(new King(Color.Black, identifiantBlanc++), 4, 0);
            AddPiece(new Bishop(Color.Black, identifiantBlanc++), 5, 0);
            AddPiece(new Knight(Color.Black, identifiantBlanc++), 6, 0);
            AddPiece(new Rook(Color.Black, identifiantBlanc++), 7, 0);

            for (int c = 0; c < 8; c++)
            {
                AddPiece(new Pawn(Color.Black, identifiantBlanc++), c, 1);
            }
        }

        /// <summary>
        /// Initializes the white pieces on the board.
        /// </summary>
        public void InitializeWhitePieces()
        {
            int identifiantNoir = 17;
            AddPiece(new Rook(Color.White, identifiantNoir++), 0, 7);
            AddPiece(new Knight(Color.White, identifiantNoir++), 1, 7);
            AddPiece(new Bishop(Color.White, identifiantNoir++), 2, 7);
            AddPiece(new Queen(Color.White, identifiantNoir++), 3, 7);
            AddPiece(new King(Color.White, identifiantNoir++), 4, 7);
            AddPiece(new Bishop(Color.White, identifiantNoir++), 5, 7);
            AddPiece(new Knight(Color.White, identifiantNoir++), 6, 7);
            AddPiece(new Rook(Color.White, identifiantNoir++), 7, 7);

            for (int c = 0; c < 8; c++)
            {
                AddPiece(new Pawn(Color.White, identifiantNoir++), c, 6);
            }
        }

        /// <summary>
        /// Fills the board with empty cases for rows 2 to 5.
        /// </summary>
        public void FillEmptyCases()
        {
            for (int row = 2; row <= 5; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Board[column, row] = new Case(column, row, null);
                }
            }
        }

        /// <summary>
        /// Adds a piece to the specified position on the board and updates the piece lists.
        /// </summary>
        /// <param name="piece">The piece to add.</param>
        /// <param name="column">The column to place the piece.</param>
        /// <param name="row">The row to place the piece.</param>
        public void AddPiece(Piece? piece, int column, int row)
        {
            Board[column, row] = new Case(column, row, piece);
            if (piece != null && piece.Color == Color.White)
            {
                _whitePieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
            if (piece != null && piece.Color == Color.Black)
            {
                _blackPieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
        }

        /// <summary>
        /// Checks if the move to the final case is valid based on the list of possible moves.
        /// </summary>
        /// <param name="lcase">List of possible cases to move to.</param>
        /// <param name="final">The final case to check.</param>
        /// <returns>True if the move is valid, otherwise false.</returns>
        public bool IsMoveValid(List<Case?> lcase, Case? final)
        {
            return lcase.Exists(i => i!.Column == final!.Column && i.Line == final.Line);
        }

        /// <summary>
        /// Attempts to move a piece from the initial case to the final case.
        /// </summary>
        /// <param name="piece">The piece to move.</param>
        /// <param name="initial">The initial case.</param>
        /// <param name="final">The final case.</param>
        /// <returns>True if the move is valid, otherwise false.</returns>
        public bool CanMovePiece(Piece? piece, Case? initial, Case? final)
        {
            List<Case?> possibleMoves = piece!.PossibleMoves(initial, this);

            if (IsMoveValid(possibleMoves, final))
            {
                Piece originalPiece = final!.Piece!;
                final.Piece = piece;
                initial!.Piece = null;

                initial.Piece = piece;
                final.Piece = originalPiece;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Modifies a pawn to another piece and updates the piece lists accordingly.
        /// </summary>
        /// <param name="p">The pawn to modify.</param>
        /// <param name="pi">The new piece.</param>
        /// <param name="c">The case where the modification takes place.</param>
        public void ModifPawn(Pawn? p, Piece pi, Case? c)
        {
            ArgumentNullException.ThrowIfNull(pi);
            ArgumentNullException.ThrowIfNull(p);

            if (pi.Color == Color.White)
            {
                _whitePieces.Add(new CoPieces { CaseLink = c, piece = pi });
                _whitePieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
            else
            {
                _blackPieces.Add(new CoPieces { CaseLink = c, piece = pi });
                _blackPieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
        }

        /// <summary>
        /// Checks if the specified king is in check.
        /// </summary>
        /// <param name="king">The king to check.</param>
        /// <param name="kingCase">The case where the king is located.</param>
        /// <returns>True if the king is in check, otherwise false.</returns>
        public bool Echec(King? king, Case? kingCase)
        {
            if (_isCheckingForCheck)
            {
                return false;
            }

            _isCheckingForCheck = true;

            var enemyPieces = king!.Color == Color.White ? _blackPieces : _whitePieces;

            foreach (var enemy in enemyPieces)
            {
                List<Case?> possibleMoves;

                if (enemy.piece is King kingTest)
                {
                    possibleMoves = kingTest.CanEat(enemy.CaseLink, this);
                }
                else if (enemy.piece is Pawn pawnTest)
                {
                    possibleMoves = pawnTest.CanEat(enemy.CaseLink, this);
                }
                else
                {
                    possibleMoves = enemy.piece!.PossibleMoves(enemy.CaseLink, this);
                }

                if (possibleMoves.Any(move => move!.Column == kingCase!.Column && move.Line == kingCase.Line))
                {
                    _isCheckingForCheck = false;
                    return true;
                }
            }

            _isCheckingForCheck = false;
            return false;
        }

        /// <summary>
        /// Checks if the specified color is in check.
        /// </summary>
        /// <param name="color">The color to check.</param>
        /// <returns>True if the color is in check, otherwise false.</returns>
        public bool IsInCheck(Color color)
        {
            King king = FindKing(color);
            return Echec(king, FindCase(king));
        }

        /// <summary>
        /// Finds the king of the specified color on the board.
        /// </summary>
        /// <param name="color">The color of the king to find.</param>
        /// <returns>The king of the specified color.</returns>
        public King FindKing(Color color)
        {
            return (color == Color.White ? (King)_whitePieces.Find(x => x.piece is King).piece! : (King)_blackPieces.Find(x => x.piece is King).piece!);
        }

        /// <summary>
        /// Finds the case where the specified piece is located.
        /// </summary>
        /// <param name="piece">The piece to locate.</param>
        /// <returns>The case where the piece is located.</returns>
        public Case? FindCase(Piece piece)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j]!.Piece == piece)
                        return Board[i, j];
                }
            }
            throw new ArgumentException("Piece not found on the board.");
        }

        /// <summary>
        /// Checks if the specified king is in checkmate.
        /// </summary>
        /// <param name="king">The king to check.</param>
        /// <param name="kingCase">The case where the king is located.</param>
        /// <returns>True if the king is in checkmate, otherwise false.</returns>
        public bool EchecMat(King? king, Case? kingCase)
        {
            var possibleKingMoves = king!.PossibleMoves(kingCase, this);

            bool kingCanEscape = false;
            bool piecesCanSave = false;

            foreach (var move in possibleKingMoves.Where(move => TryMovePiece(kingCase, move)))
            {
                if (!Echec(king, move))
                {
                    kingCanEscape = true;
                }
                UndoMovePiece(kingCase, move);
            }

            var allyPieces = king.Color == Color.White ? WhitePieces : BlackPieces;

            foreach (var pieceInfo in allyPieces)
            {
                var piece = pieceInfo.piece;
                var startCase = pieceInfo.CaseLink;
                var possibleMoves = piece!.PossibleMoves(startCase, this);

                foreach (var move in possibleMoves.Where(move => TryMovePiece(startCase, move)))
                {
                    if (!Echec(king, kingCase))
                    {
                        piecesCanSave = true;
                    }
                    UndoMovePiece(startCase, move);
                }

                if (piecesCanSave || kingCanEscape)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Attempts to move a piece from the initial case to the final case.
        /// </summary>
        /// <param name="initial">The initial case.</param>
        /// <param name="final">The final case.</param>
        /// <returns>True if the move is successful, otherwise false.</returns>
        private bool TryMovePiece(Case? initial, Case? final)
        {
            if (CanMovePiece(initial!.Piece, initial, final))
            {
                final!.Piece = initial.Piece;
                initial.Piece = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Undoes the move of a piece from the final case back to the initial case.
        /// </summary>
        /// <param name="initial">The initial case.</param>
        /// <param name="final">The final case.</param>
        private static void UndoMovePiece(Case? initial, Case? final)
        {
            initial!.Piece = final!.Piece;
            final.Piece = null;
        }

        /// <summary>
        /// Checks if any piece in the team can defend the king by moving to a position that resolves a check.
        /// </summary>
        /// <param name="teamPieces">The list of team pieces.</param>
        /// <param name="kingCase">The case where the king is located.</param>
        /// <returns>True if any piece can defend the king, otherwise false.</returns>
        public bool CanDefendKing(List<CoPieces> teamPieces, Case? kingCase)
        {
            foreach (var piece in teamPieces)
            {
                List<Case?> possibleMoves;

                if (piece.piece is King kingPiece)
                {
                    possibleMoves = kingPiece.CanEat(piece.CaseLink, this);
                }
                else if (piece.piece is Pawn pawnPiece)
                {
                    possibleMoves = pawnPiece.CanEat(piece.CaseLink, this);
                }
                else
                {
                    possibleMoves = piece.piece!.PossibleMoves(piece.CaseLink, this);
                }

                foreach (var move in possibleMoves)
                {
                    Piece originalPiece = move!.Piece!;
                    Case? originalCase = piece.CaseLink;

                    move.Piece = piece.piece;
                    originalCase!.Piece = null;

                    bool resolvesCheck = !IsInCheck(piece.piece.Color);

                    originalCase.Piece = piece.piece;
                    move.Piece = originalPiece;

                    if (resolvesCheck)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Modifies the list of pieces after a piece has moved.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        public void ModifList(Case initial, Case final)
        {
            var list = final.Piece!.Color == Color.White ? _whitePieces : _blackPieces;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CaseLink!.Line == initial.Line && list[i].CaseLink!.Column == initial.Column)
                {
                    var updatedCoPiece = new CoPieces
                    {
                        piece = final.Piece,
                        CaseLink = final
                    };
                    list[i] = updatedCoPiece;  // Replace the instance in the list if CoPieces is a struct
                    break;
                }
            }
        }

        /// <summary>
        /// Removes a piece from the list after it has been captured.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        public void RemovePieceFromList(Case initial)
        {
            var list = initial.Piece!.Color == Color.White ? _blackPieces : _whitePieces;
            int indexToRemove = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CaseLink!.Line == initial.Line && list[i].CaseLink!.Column == initial.Column)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove != -1)
            {
                list.RemoveAt(indexToRemove);
            }
        }

        /// <summary>
        /// Processes the post-move operations after a piece has been moved.
        /// </summary>
        /// <param name="initial">The initial case of the piece.</param>
        /// <param name="final">The final case of the piece.</param>
        public void ProcessPostMove(Case? initial, Case? final)
        {
            if (final!.Piece != null && final.Piece.Color != initial!.Piece!.Color)
            {
                RemovePieceFromList(initial);
            }
            final.Piece = initial!.Piece;
            initial.Piece = null;
            ModifList(initial, final);

            if (final.Piece is IFirstMove firstMover)
            {
                firstMover.FirstMove = false;
            }
        }

        /// <summary>
        /// Resets all possible moves on the board to false.
        /// </summary>
        public void ResetPossibleMoves()
        {
            foreach (var c in Board)
            {
                if (!c.IsPossibleMove) continue;
                c.IsPossibleMove = false;
            }
        }

    }
}
