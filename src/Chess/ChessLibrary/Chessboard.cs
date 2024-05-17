using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessLibrary
{
    public class Chessboard : IBoard
    {
        public Case[,] Board { get; private set; }
        
        public ReadOnlyCollection<CoPieces> WhitePieces 
            => whitePieces.AsReadOnly();
        public ReadOnlyCollection<CoPieces> BlackPieces 
            => blackPieces.AsReadOnly();

        List<CoPieces>? whitePieces = new List<CoPieces>();
        List<CoPieces>? blackPieces = new List<CoPieces>();

        public Chessboard(Case[,] tcase, bool isEmpty)
        {
            Board = tcase;
            whitePieces = new List<CoPieces>();
            blackPieces = new List<CoPieces>();

            if (!isEmpty)
            {
                InitializeChessboard();
            }
            else
            {
                InitializeEmptyBoard();
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
                // Add the piece to the list of white pieces
                if (whitePieces != null)
                    whitePieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
            else
            {
                // Add the piece to the list of black pieces
                if (blackPieces != null)
                    blackPieces.Add(new CoPieces { CaseLink = new Case(column, row, piece), piece = piece });
            }
        }

        public bool IsMoveValid(List<Case> lcase, Case final)
        {
            return lcase.Exists(i => i.Column == final.Column && i.Line == final.Line);
        }

        public bool CanMovePiece(Piece? piece, Case initial, Case final)
        {
            List<Case> L = piece!.PossibleMoves(initial, this);
            return IsMoveValid(L, final);
        }

        public void ModifPawn(Pawn? p, Piece pi, Case c)
        {
            ArgumentNullException.ThrowIfNull(pi);
            ArgumentNullException.ThrowIfNull(p);

            if (pi.Color == Color.White)
            {
                //Add the new piece to the list of white pieces and remove the Pawn
                this.whitePieces!.Add(new CoPieces { CaseLink = c, piece = pi });
                this.whitePieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
            else
            {
                //Add the new piece to the list of black pieces and remove the Pawn
                this.blackPieces!.Add(new CoPieces { CaseLink = c, piece = pi });
                this.blackPieces.Remove(new CoPieces { CaseLink = c, piece = p });
            }
        }

        public bool Echec(King king, Case kingCase)
        {
            List<CoPieces> enemyPieces = (king.Color == Color.White) ? blackPieces : whitePieces;

            foreach (var enemy in enemyPieces)
            {
                if (enemy.piece is King)
                {
                    King KingTest = (King)enemy.piece;
                    var possibleMoves = KingTest.CanEat(enemy.CaseLink, this);
                    foreach (var move in possibleMoves)
                    {
                        if (move.Column == kingCase.Column && move.Line == kingCase.Line)
                        {
                            return true;
                        }
                    }

                }
                else if (enemy.piece is Pawn)
                {
                    Pawn PawnTest = (Pawn)enemy.piece;
                    var possibleMoves = PawnTest.CanEat(enemy.CaseLink, this);
                    foreach (var move in possibleMoves)
                    {
                        if (move.Column == kingCase.Column && move.Line == kingCase.Line)
                        {
                            return true;
                        }
                    }
                }
                else
                {
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
            }

            return false; // Le roi n'est pas en échec
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

        private King FindKing(Color color)
        {
            return color == Color.White ? (King)whitePieces!.Find(x => x.piece is King)!.piece : (King)blackPieces!.Find(x => x.piece is King)!.piece;
        }

        private Case FindCase(Piece piece)
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

        public Chessboard CopyBoard()
        {
            Case[,] newBoard = new Case[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var originalPiece = Board[i, j].Piece;
                    Piece? copiedPiece = null;
                    if (originalPiece != null)
                    {
                        copiedPiece = originalPiece;
                    }
                    newBoard[i, j] = new Case(i, j, copiedPiece);
                }
            }

            var newChessboard = new Chessboard(newBoard, true);
            // Copier les listes de pièces
            newChessboard.whitePieces = new List<CoPieces>(WhitePieces!);
            newChessboard.blackPieces = new List<CoPieces>(BlackPieces!);

            return newChessboard;
        }

        public bool EchecMat(King king, Case kingCase)
        {
            // Obtenez tous les mouvements possibles pour le roi
            var possibleKingMoves = king.PossibleMoves(kingCase, this);

            // Vérifiez si le roi peut échapper à l'échec
            foreach (var move in possibleKingMoves)
            {
                if (TryMovePiece(kingCase, move))
                {
                    if (!Echec(king, move))
                    {
                        UndoMovePiece(kingCase, move);
                        return false;
                    }
                    UndoMovePiece(kingCase, move);
                }
            }

            // Obtenez toutes les pièces alliées
            var allyPieces = king.Color == Color.White ? WhitePieces : BlackPieces;
            List<CoPieces> list2 = new List<CoPieces>(allyPieces);
            // Vérifiez si une pièce alliée peut protéger le roi
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
                            return false;
                        }
                        UndoMovePiece(startCase, move);
                    }
                }
            }

            // Si aucune évasion possible n'a été trouvée, c'est échec et mat
            return true;
        }

        // Méthode pour simuler un mouvement de pièce
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

        // Méthode pour annuler un mouvement de pièce
        private void UndoMovePiece(Case initial, Case final)
        {
            initial.Piece = final.Piece;
            final.Piece = null;
        }
    }
}
