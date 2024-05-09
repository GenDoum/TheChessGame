using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Chessboard : IBoard
    {

        public Case[,] Board { get; private set; }
        public List<Piece>? WhitePieces { get; private set; }
        public List<Piece>? BlackPieces { get; private set; }

        public Chessboard(Case[,]Tcase , bool isEmpty)
        {
            Board = Tcase;
            WhitePieces = new List<Piece>();
            BlackPieces = new List<Piece>();

            if (!isEmpty)
            {
                InitializeChessboard();
            }
            else
            {
                InitializeEmptyBoard();
            }
        }

        private void InitializeEmptyBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Board[row, column] = new Case(row, column, null);
                }
            }
        }

        private void InitializeChessboard()
        {
            InitializeWhitePieces();
            InitializeBlackPieces();
            FillEmptyCases();
        }

        private void InitializeWhitePieces()
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

        private void InitializeBlackPieces()
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

        private void FillEmptyCases()
        {
            for (int row = 2; row <= 5; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Board[column, row] = new Case(column, row, null);
                }
            }
        }

        private void AddPiece(Piece piece, int column, int row)
        {
            Board[row, column] = new Case(row, column, piece);
            if (piece.Color == Color.White)
            {
                WhitePieces.Add(piece);
            }
            else
            {
                BlackPieces.Add(piece);
            }
        }
    


    public bool IsMoveValid(List<Case> Lcase, Case Final)
        {
            foreach (var i in Lcase)
            {
                if (i.Column == Final.Column && i.Line == Final.Line) { return true; }
            }
            return false;
        }



        public bool MovePiece(Piece piece, Case Initial, Case Final,User P)
        {
            if (P.color != piece.Color)
            {
                return false;
            }
            List<Case> L = piece.PossibleMoves(Initial, this);

            if (IsMoveValid(L, Final))
            {
                return true;
            }
            return false ;
        }

        public bool PawnCanEvolve()
        {
            //Check if pawn is on line 8 and if he is white to processes for the evolved
            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 7].Piece is Pawn && Board[col, 7].Piece.Color == Color.White)
                {
                    Evolve(Board[col, 7].Piece as Pawn, Board[col, 7]);
                    return true;
                }
            }

            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 0].Piece is Pawn && Board[col, 0].Piece.Color == Color.Black)
                {
                    Evolve(Board[col, 0].Piece as Pawn, Board[col, 0]);
                    return true;
                }
            }

            return false;
        }


        private void Evolve(Pawn P, Case C)
        {
            Queen NewQueen;
            Rook NewRook;
            Knight NewKnight;
            Bishop NewBishop;
            afficheEvolved();
            var result = Console.ReadLine();
            while (true)
            {
                switch (result)
                {
                    case "1":
                        NewQueen = new Queen(P.Color, P.id);
                        C.Piece = NewQueen;
                        ModifPawn(P, NewQueen);
                        return;

                    case "2":
                        NewRook = new Rook(P.Color, P.id);
                        C.Piece = NewRook;
                        ModifPawn(P, NewRook);
                        return;
                    case "3":
                        NewBishop = new Bishop(P.Color, P.id);
                        C.Piece = NewBishop;
                        ModifPawn(P, NewBishop);
                        return;
                    case "4":
                        NewKnight = new Knight(P.Color, P.id);
                        C.Piece = NewKnight;
                        ModifPawn(P, NewKnight);
                        return;
                    default:
                        afficheEvolved();
                        result = Console.ReadLine();
                        break;
                }
            }
        }

        private void ModifPawn(Pawn P, Piece pi)
        {
            if (pi != null)
            {
                throw new ArgumentNullException(nameof(Pawn));
            }
            if (P != null)
            {
                throw new ArgumentNullException(nameof(Pawn));
            }

            if (pi.Color == Color.White)
            {
                this.WhitePieces.Remove(P);
                this.WhitePieces.Add(pi);
            }

            else
            {
                this.BlackPieces.Remove(P);
                this.BlackPieces.Add(pi);
            }

        }

        private void afficheEvolved()
        {
            Console.WriteLine("Entrez 1 pour changer votre pion en Renne");
            Console.WriteLine("Entrez 2 pour changer votre pion en Tour");
            Console.WriteLine("Entrez 3 pour changer votre pion en Fou");
            Console.WriteLine("Entrez 4 pour changer votre pion en Chavalier");
        }


        //To do for create class King
        public bool IsCheck(King myKing)
        {
            List<Piece> enemyPieces;
            if (myKing.Color == Color.White)
            {
                // Si le roi est blanc, les pièces ennemies sont noires
                enemyPieces = BlackPieces;
            }
            else
            {
                // Si le roi est noir, les pièces ennemies sont blanches
                enemyPieces = WhitePieces;
            }

            foreach (Piece enemyPiece in enemyPieces)
            {
                if (MovePiece(enemyPiece, Case[enemyPiece]))
                {
                    return true;  // Le roi est en échec
                }
            }

            return false;  // Le roi n'est pas en échec
        }

        public bool EchecMat(List<Case> )
        {
            return false;
        }

        public bool MovePiece(Case Initial, Case Final, User P)
        {
            throw new NotImplementedException();
        }
    }
}