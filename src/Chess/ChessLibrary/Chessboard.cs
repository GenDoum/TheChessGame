using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessLibrary
{
    public class Chessboard : IBoard
    {
        public Case?[,] Board { get; private set; }



        public ReadOnlyCollection<CoPieces> WhitePieces => _whitePieces.AsReadOnly();

        private readonly List<CoPieces> _whitePieces = new List<CoPieces>();
        public ReadOnlyCollection<CoPieces> BlackPieces => _blackPieces.AsReadOnly();

        private readonly List<CoPieces> _blackPieces = new List<CoPieces>();


        private bool _isCheckingForCheck = false;

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

        public List<CoPieces> CopyWhitePieces()
        {
            var result = new List<CoPieces>();
            foreach (var copiece in WhitePieces)
            {
                result.Add(new CoPieces { CaseLink = copiece.CaseLink, piece = copiece.piece });
            }
            return result;
        }
        public List<CoPieces> CopyBlackPieces()
        {
            var result = new List<CoPieces>();
            foreach (var copiece in BlackPieces)
            {
                result.Add(new CoPieces { CaseLink = copiece.CaseLink, piece = copiece.piece });
            }
            return result;
        }

        public int NbRows => Board?.GetLength(0) ?? 0;
        public int NbColumns => Board?.GetLength(1) ?? 0;

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

        public void InitializeChessboard()
        {
            InitializeBlackPieces();
            InitializeWhitePieces();
            FillEmptyCases();
        }

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

        public void AddPiece(Piece? piece, int column, int row)
        {
            Board[column, row] = new Case(column, row, piece);
            if (piece != null && piece.Color == Color.White)
            {
                _whitePieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
            else
            {
                _blackPieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
        }

        public bool IsMoveValid(List<Case?> lcase, Case? final)
        {
            return lcase.Exists(i => i!.Column == final!.Column && i.Line == final.Line);
        }

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

        public bool IsInCheck(Color color)
        {
            King king = FindKing(color);
            return Echec(king, FindCase(king));
        }

        public King FindKing(Color color)
        {
            return (color == Color.White ? (King)_whitePieces.Find(x => x.piece is King).piece! : (King)_blackPieces.Find(x => x.piece is King).piece!);
        }

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

        private static void UndoMovePiece(Case? initial, Case? final)
        {
            initial!.Piece = final!.Piece;
            final.Piece = null;
        }

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

        public void ModifList(Case initial, Case final)
        {
            var list = initial.Piece!.Color == Color.White ? _whitePieces : _blackPieces;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CaseLink.Line == initial.Line && list[i].CaseLink.Column == initial.Column)
                {
                    // Si CoPieces est une struct, vous devriez créer une nouvelle instance avec les modifications appropriées
                    var updatedCoPiece = new CoPieces
                    {
                        piece = list[i].piece,
                        CaseLink = final
                    };
                    list[i] = updatedCoPiece;  // Remplacement de l'instance dans la liste si CoPieces est une struct
                    break;
                }
            }
        }
    }
}
