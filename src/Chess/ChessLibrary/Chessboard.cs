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

        public Chessboard(Case[,] board)
        {
            Board = board;
            WhitePieces = new List<Piece>();
            BlackPieces = new List<Piece>();

            int identifiant_blanc = 1;
            int identifiant_noir = 1;
            for (int C = 0; C < 8; C++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if ((C == 0 && l == 0) || (C == 7 && l == 0))
                    {
                        Rook whiteRook = new Rook(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whiteRook);
                        WhitePieces.Add(whiteRook);
                        identifiant_blanc++;
                    }
                    else if ((C == 1 && l == 0) || (C == 6 && l == 0))
                    {
                        Knight whiteKnight = new Knight(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whiteKnight);
                        WhitePieces.Add(whiteKnight);
                        identifiant_blanc++;
                    }
                    else if ((C == 2 && l == 0) || (C == 5 && l == 0))
                    {
                        Bishop whiteBishop = new Bishop(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whiteBishop);
                        WhitePieces.Add(whiteBishop);
                        identifiant_blanc++;
                    }
                    else if (C == 3 && l == 0)
                    {
                        Queen whiteQueen = new Queen(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whiteQueen);
                        WhitePieces.Add(whiteQueen);
                        identifiant_blanc++;
                    }
                    else if (C == 4 && l == 0)
                    {
                        King whiteKing = new King(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whiteKing);
                        WhitePieces.Add(whiteKing);
                        identifiant_blanc++;
                    }
                    else if (l == 1)
                    {
                        Pawn whitePawn = new Pawn(Color.White, identifiant_blanc);
                        Board[C, l] = new Case(C, l, whitePawn);
                        WhitePieces.Add(whitePawn);
                        identifiant_blanc++;
                    }
                    else if ((C == 0 && l == 7) || (C == 7 && l == 7))
                    {
                        Rook blackRook = new Rook(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackRook);
                        BlackPieces.Add(blackRook);
                        identifiant_noir++;
                    }
                    else if ((C == 1 && l == 7) || (C == 6 && l == 7))
                    {
                        Knight blackKnight = new Knight(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackKnight);
                        BlackPieces.Add(blackKnight);
                        identifiant_noir++;
                    }
                    else if ((C == 2 && l == 7) || (C == 5 && l == 7))
                    {
                        Bishop blackBishop = new Bishop(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackBishop);
                        BlackPieces.Add(blackBishop);
                        identifiant_noir++;
                    }
                    else if (C == 3 && l == 7)
                    {
                        Queen blackQueen = new Queen(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackQueen);
                        BlackPieces.Add(blackQueen);
                        identifiant_noir++;
                    }
                    else if (C == 4 && l == 7)
                    {
                        King blackKing = new King(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackKing);
                        BlackPieces.Add(blackKing);
                        identifiant_noir++;
                    }
                    else if (l == 6)
                    {
                        Pawn blackPawn = new Pawn(Color.Black, identifiant_noir);
                        Board[C, l] = new Case(C, l, blackPawn);
                        BlackPieces.Add(blackPawn);
                        identifiant_noir++;
                    }
                    else
                    {
                        Board[C, l] = new Case(C, l, null);
                    }
                }
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
            
            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 7].Piece is Pawn && Board[col, 7].Piece.Color == Color.White)
                {
                    return true;  
                }
            }

           
            for (int col = 0; col < 8; col++)
            {
                if (Board[col, 0].Piece is Pawn && Board[col, 0].Piece.Color == Color.Black)
                {
                    return true;  
                }
            }

            return false;  
        }

        // public User Turn()
        // {
        //     throw new NotImplementedException();
        // }
    }
}