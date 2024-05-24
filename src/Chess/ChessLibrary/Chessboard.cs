using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessLibrary
{
    public class Chessboard : IBoard
    {
        public Case[,] Board { get; private set; }

        public List<CoPieces> WhitePieces { get; private set; }
        public List<CoPieces> BlackPieces { get; private set; }

        private bool isCheckingForCheck = false;

        public Chessboard(Case[,] tcase, bool isEmpty)
        {
            Board = tcase;
            WhitePieces = new List<CoPieces>();
            BlackPieces = new List<CoPieces>();

            if (!isEmpty)
            {
                InitializeChessboard();
            }
            else
            {
                InitializeEmptyBoard();
            }
        }

        private readonly int[,]? board;
        
        public int NbRows => Board?.GetLength(0) ?? 0;
        public int NbColumns => Board?.GetLength(1) ?? 0;
        
        
        public IEnumerable<Case> FlatBoard
        {
            get
            {
                List<Case> flatBoard = new();
                
                if (board == null) return flatBoard;
                
                int[,] mat = new int[NbRows, NbColumns];
                
                for(int i = 0; i < NbRows; i++)
                {
                    for (int j = 0; j < NbColumns; j++)
                    {
                        flatBoard.Add(new Case(i, j, null));
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
            InitializeWhitePieces();
            InitializeBlackPieces();
            FillEmptyCases();
        }

        public void InitializeWhitePieces()
        {
            int identifiantBlanc = 1;
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
                AddPiece(new Pawn(Color.White, identifiantBlanc++), c, 1);
            }
        }

        public void InitializeBlackPieces()
        {
            int identifiantNoir = 1;
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
                AddPiece(new Pawn(Color.Black, identifiantNoir++), c, 6);
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
                WhitePieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
            else
            {
                BlackPieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
        }

        public bool IsMoveValid(List<Case> lcase, Case final)
        {
            return lcase.Exists(i => i.Column == final.Column && i.Line == final.Line);
        }

        public bool CanMovePiece(Piece? piece, Case initial, Case final)
        {
            List<Case> possibleMoves = piece!.PossibleMoves(initial, this);
            if (IsMoveValid(possibleMoves, final))
            {
                Piece originalPiece = final.Piece;
                final.Piece = piece;
                initial.Piece = null;

                bool isKingSafe = !IsInCheck(piece.Color);

                initial.Piece = piece;
                final.Piece = originalPiece;

                return isKingSafe;
            }
            return false;
        }


        public void ModifPawn(Pawn? p, Piece pi, Case c)
        {
            ArgumentNullException.ThrowIfNull(pi);
            ArgumentNullException.ThrowIfNull(p);

            if (pi.Color == Color.White)
            {
                WhitePieces.Add(new CoPieces { CaseLink = c, piece = pi });
                WhitePieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
            else
            {
                BlackPieces.Add(new CoPieces { CaseLink = c, piece = pi });
                BlackPieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
        }

        public bool Echec(King king, Case kingCase)
        {
            if (isCheckingForCheck)
            {
                return false;
            }

            isCheckingForCheck = true;

            List<CoPieces> enemyPieces = king.Color == Color.White ? BlackPieces : WhitePieces;

            foreach (var enemy  in enemyPieces)
            {
                List<Case> possibleMoves;

                if (enemy.piece is King)
                {
                    King KingTest = (King)enemy.piece;
                    possibleMoves = KingTest.CanEat(enemy.CaseLink, this);
                }
                else if (enemy.piece is Pawn)
                {
                    Pawn PawnTest = (Pawn)enemy.piece;
                    possibleMoves = PawnTest.CanEat(enemy.CaseLink, this);
                }
                else
                {
                    possibleMoves = enemy.piece.PossibleMoves(enemy.CaseLink, this);
                }

                  if (possibleMoves.Any(move => move.Column == kingCase.Column && move.Line == kingCase.Line))
                {
                    isCheckingForCheck = false;
                    return true;
                }
             }

            isCheckingForCheck = false;
            return false;
        }



        public bool IsInCheck(Color color)
        {
            King king = FindKing(color);
            return Echec(king, FindCase(king));
        }

        public bool CanResolveCheck(Case initial, Case final, Color color)
        {
            Piece originalFinalPiece = final.Piece;
            Piece? movingPiece = initial.Piece;

            final.Piece = initial.Piece;
            initial.Piece = null;

            bool canResolve = !IsInCheck(color);

            initial.Piece = movingPiece;
            final.Piece = originalFinalPiece;

            return canResolve;
        }

        public King FindKing(Color color)
        {
            return color == Color.White ? (King)WhitePieces!.Find(x => x.piece is King)!.piece : (King)BlackPieces!.Find(x => x.piece is King)!.piece;
        }

        public Case FindCase(Piece piece)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j].Piece == piece)
                        return Board[i, j];
                }
            }
            throw new Exception("Piece not found on the board.");
        }

        public bool EchecMat(King king, Case kingCase)
        {
            var possibleKingMoves = king.PossibleMoves(kingCase, this);

            bool kingCanEscape = false;
            bool piecesCanSave = false;

            foreach (var move in possibleKingMoves)
            {
                if (TryMovePiece(kingCase, move))
                {
                    if (!Echec(king, move))
                    {
                        UndoMovePiece(kingCase, move);
                        kingCanEscape = true;
                    }
                    UndoMovePiece(kingCase, move);
                }
            }

            var allyPieces = king.Color == Color.White ? WhitePieces : BlackPieces;

            foreach (var pieceInfo in allyPieces!)
            {
                var piece = pieceInfo.piece;
                var startCase = pieceInfo.CaseLink;
                var possibleMoves = piece.PossibleMoves(startCase, this);

                foreach (var move in possibleMoves)
                {
                    if (TryMovePiece(startCase, move))
                    {
                        if (!Echec(king, kingCase))
                        {
                            UndoMovePiece(startCase, move);
                            piecesCanSave = true;
                        }
                        UndoMovePiece(startCase, move);
                    }
                }

                if (piecesCanSave || kingCanEscape)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryMovePiece(Case initial, Case final)
        {
            if (CanMovePiece(initial.Piece, initial, final))
            {
                final.Piece = initial.Piece;
                initial.Piece = null;
                return true;
            }
            return false;
        }

        private void UndoMovePiece(Case initial, Case final)
        {
            initial.Piece = final.Piece;
            final.Piece = null;
        }

        public bool CanDefendKing(List<CoPieces> teamPieces, Case kingCase)
        {

            foreach (var piece in teamPieces)
            {
                List<Case> possibleMoves;

                if (piece.piece is King)
                {
                    King kingPiece = (King)piece.piece;
                    possibleMoves = kingPiece.CanEat(piece.CaseLink, this);
                }
                else if (piece.piece is Pawn)
                {
                    Pawn pawnPiece = (Pawn)piece.piece;
                    possibleMoves = pawnPiece.CanEat(piece.CaseLink, this);
                }
                else
                {
                    possibleMoves = piece.piece.PossibleMoves(piece.CaseLink, this);
                }

                foreach (var move in possibleMoves)
                {
                    Piece originalPiece = move.Piece;
                    Case originalCase = piece.CaseLink;

                    move.Piece = piece.piece;
                    originalCase.Piece = null;

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

    }
}
