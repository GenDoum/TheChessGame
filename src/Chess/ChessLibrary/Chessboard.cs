using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Chessboard : IRegles
    {

        public Case[,] Board { get; private set; }
        public List<Piece> WhitePieces { get; private set; }
        public List<Piece> BlackPieces { get; private set; }

        public Chessboard(Case[,] board, bool isEmpty)
        {
            Board = board;
            if (isEmpty)
            {
                for (int C = 0; C < 8; C++)
                {
                    for (int l = 0; l < 8; l++)
                        Board[C, l] = new Case(C, l, null);
                }
            }
            else
                InitialiseChessboard(board);
        }
        
        private void InitialiseChessboard(Case[,] board)
        {
            Board = board;
            WhitePieces = new List<Piece>();
            BlackPieces = new List<Piece>();

            for (int C = 0; C < 8; C++)
            {
                for (int l = 0; l < 8; l++)
                {
                    Piece piece = null;
                    if (l == 0 || l == 7)
                    {
                        piece = CreatePieceForFirstRow(C, l);
                    }
                    else if (l == 1 || l == 6)
                    {
                        piece = new Pawn(l == 1 ? Color.White : Color.Black, C + 1);
                    }

                    Board[C, l] = new Case(C, l, piece);
                    if (piece != null)
                    {
                        if (piece.Color == Color.White)
                        {
                            WhitePieces.Add(piece);
                        }
                        else
                        {
                            BlackPieces.Add(piece);
                        }
                    }
                }
            }
        }

        private Piece CreatePieceForFirstRow(int column, int row)
        {
            Color color = row == 0 ? Color.White : Color.Black;
            int id = column + 1;
            switch (column)
            {
                case 0:
                case 7:
                    return new Rook(color, id);
                case 1:
                case 6:
                    return new Knight(color, id);
                case 2:
                case 5:
                    return new Bishop(color, id);
                case 3:
                    return new Queen(color, id);
                case 4:
                    return new King(color, id);
                default:
                    return null;
            }
        }
        
        public bool IsMoveValid(List<Case> Lcase, Case Final)
        {
            foreach (var i in Lcase)
            {
                if (i == Final) { return true; }
            }
            return false;
        }
        
        public void MovePiece(Piece piece, Case Initial, Case Final)
        {
            List<Case> L = piece.PossibleMoves(Initial, this);
            if (IsMoveValid(L, Final))
            {
                Initial.Piece = null;
                Final.Piece = piece;
            }
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
            switch (result)
            {
                case "1":
                    NewQueen = new Queen(P.Color, P.id);
                    C.Piece = NewQueen;
                    break;

                case "2":
                    NewRook = new Rook(P.Color,P.id);
                    C.Piece = NewRook;
                    break;
                case "3":
                    NewBishop = new Bishop(P.Color,P.id);
                    C.Piece = NewBishop;
                    break;
                case "4":
                    NewKnight = new Knight(P.Color,P.id);
                    C.Piece = NewKnight;
                    //ModifList(ref P, NewKnight);
                    break;
                default:
                    afficheEvolved();
                    result = Console.ReadLine();
                    break;
            }
            
        }

 /*       private bool ModifList(ref Pawn P,ref Piece pi)
        {
            if
        }*/

        private void afficheEvolved()
        {
            Console.WriteLine("Entrez 1 pour changer votre pion en Renne");
            Console.WriteLine("Entrez 2 pour changer votre pion en Tour");
            Console.WriteLine("Entrez 3 pour changer votre pion en Fou");
            Console.WriteLine("Entrez 4 pour changer votre pion en Chavalier");
        }

        public void Echec()
        {

        }

    }
}