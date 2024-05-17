using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestBoard
{
    [Fact]
    public void TestInitializeEmptyBoard()
    {
        // Arrange
        Case[,] Tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(Tcase, true);

        // Act
        chessboard.InitializeEmptyBoard();

        // Assert
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Assert.Null(chessboard.Board[i, j].Piece);
            }
        }
    }

    [Fact]
    public void TestInitializeChessboard()
    {
        // Arrange
        Case[,] Tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(Tcase, false);

        // Act
        chessboard.InitializeChessboard();

        // Assert
        // Vérifie que les pions sont à leur place initiale
        for (int i = 0; i < 8; i++)
        {
            Assert.IsType<Pawn>(chessboard.Board[i, 1].Piece);
            Assert.Equal(Color.White, chessboard.Board[i, 1].Piece.Color);

            Assert.IsType<Pawn>(chessboard.Board[i, 6].Piece);
            Assert.Equal(Color.Black, chessboard.Board[i, 6].Piece.Color);
        }
    }
    
    [Theory]
    [InlineData(0, 1, typeof(Pawn), Color.White)]
    [InlineData(0, 6, typeof(Pawn), Color.Black)]
    public void TestPiecePlacement(int x, int y, Type expectedType, Color expectedColor)
    {
        // Arrange
        Case[,] Tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(Tcase, false);

        // Act
        chessboard.InitializeChessboard();

        // Assert
        Assert.IsType(expectedType, chessboard.Board[x, y].Piece);
        Assert.Equal(expectedColor, chessboard.Board[x, y].Piece.Color);
    }
    
    [Theory]
    [InlineData(0, 0, typeof(Rook))]
    [InlineData(1, 0, typeof(Knight))]
    [InlineData(2, 0, typeof(Bishop))]
    [InlineData(3, 0, typeof(Queen))]
    [InlineData(4, 0, typeof(King))]
    [InlineData(5, 0, typeof(Bishop))]
    [InlineData(6, 0, typeof(Knight))]
    [InlineData(7, 0, typeof(Rook))]
    public void TestInitializeWhitePieces(int column, int row, Type pieceType)
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);

        // Act
        chessboard.InitializeWhitePieces();

        // Assert
        // Check that the white pieces are correctly placed on the board
        Assert.IsType(pieceType, chessboard.Board[column, row].Piece);
    }

    [Theory]
    [InlineData(0, 7, typeof(Rook))]
    [InlineData(1, 7, typeof(Knight))]
    [InlineData(2, 7, typeof(Bishop))]
    [InlineData(3, 7, typeof(Queen))]
    [InlineData(4, 7, typeof(King))]
    [InlineData(5, 7, typeof(Bishop))]
    [InlineData(6, 7, typeof(Knight))]
    [InlineData(7, 7, typeof(Rook))]
    public void TestInitializeBlackPieces(int column, int row, Type pieceType)
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);

        // Act
        chessboard.InitializeBlackPieces();

        // Assert
        // Check that the black pieces are correctly placed on the board
        Assert.IsType(pieceType, chessboard.Board[column, row].Piece);
    }
    
    [Fact]
    public void TestFillEmptyCases()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);

        // Act
        chessboard.FillEmptyCases();

        // Assert
        for (int row = 2; row <= 5; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                Assert.Null(chessboard.Board[column, row].Piece);
            }
        }
    }

    [Fact]
    public void TestAddPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        int column = 0;
        int row = 0;

        // Act
        chessboard.AddPiece(piece, column, row);

        // Assert
        Assert.Equal(piece, chessboard.Board[column, row].Piece);
        //Assert.Contains(chessboard.WhitePieces, copieces => copieces.piece == piece && copieces.CaseLink == chessboard.Board[column, row]);
    }
    
    [Fact]
    public void TestIsMoveValid()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case Final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.True(result);
    }
        [Fact]
    public void TestIsMoveValidValidMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case Final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsMoveValid_InvalidMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case Final = new Case(3, 3, null);

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestIsMoveValid_MoveToOccupiedCase()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, new Pawn(Color.White, 1)),
            new Case(2, 2, null)
        };
        Case Final = new Case(1, 1, new Pawn(Color.White, 1));

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsMoveValid_EmptyPossibleMoves()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>();
        Case Final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestIsMoveValid_FinalCaseOutOfBounds()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> Lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case Final = new Case(8, 8, null); // Out of bounds case

        // Act
        bool result = chessboard.IsMoveValid(Lcase, Final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Case Initial = new Case(0, 0, piece);
        Case Final = new Case(0, 1, null);

        // Act
        bool result = chessboard.CanMovePiece(piece, Initial, Final);

        // Assert
        Assert.True(result);
    }

    
    [Fact]
    public void TestModifPawn()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Pawn? pawn = new Pawn(Color.White, 1);
        Case caseForPawn = new Case(0, 7, pawn);
        Queen newPiece = new Queen(Color.White, 2);

        // Act
        chessboard.ModifPawn(pawn, newPiece, caseForPawn);

        // Assert
        Assert.Contains(chessboard.WhitePieces, copieces => copieces.piece == newPiece && copieces.CaseLink == caseForPawn);
        Assert.DoesNotContain(chessboard.WhitePieces, copieces => copieces.piece == pawn && copieces.CaseLink == caseForPawn);

        // Test null pawn
        Assert.Throws<ArgumentNullException>(() => chessboard.ModifPawn(null, newPiece, caseForPawn));

        // Test null new piece
        Assert.Throws<ArgumentNullException>(() => chessboard.ModifPawn(pawn, null, caseForPawn));
    }
    
    [Fact]
    public void TestEchec()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.False(result); // Assuming there are no other pieces on the board, the king should not be in check
    }
    [Fact]
    public void TestEchecParPion()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Pawn? pawn = new Pawn(Color.Black, 1);
        Case kingCase = new Case(4, 4, king);
        Case pawnCase = new Case(3, 5, pawn);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(pawn, 3, 5);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The pawn should put the king in check
    }
    [Fact]
    public void TestEchecParCavalier()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Knight? knight = new Knight(Color.Black, 1);
        Case kingCase = new Case(4, 4, king);
        Case knightCase = new Case(2, 3, knight);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(knight, 2, 3);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The knight should put the king in check
    }
    [Fact]
    public void TestEchecParReine()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Case kingCase = new Case(4, 4, king);
        Case queenCase = new Case(4, 7, queen);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(queen, 4, 7);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The queen should put the king in check
    }
    [Fact]
    public void TestEchecParFou()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Bishop? bishop = new Bishop(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        Case bishopCase = new Case(2, 2, bishop);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(bishop, 2, 2);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The bishop should put the king in check
    }
    [Fact]
    public void TestEchecParTour()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Rook? rook = new Rook(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        Case rookCase = new Case(0, 7, rook);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(rook, 0, 7);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The rook should put the king in check
    }

    // [Fact]
    // public void TestCopyBoard()
    // {
    //     // Arrange
    //     Chessboard originalChessboard = new Chessboard(new Case[8, 8], true);
    //     originalChessboard.InitializeChessboard();
    //
    //     // Act
    //     Chessboard copiedChessboard = originalChessboard.CopyBoard();
    //
    //     // Assert
    //     for (int i = 0; i < 8; i++)
    //     {
    //         for (int j = 0; j < 8; j++)
    //         {
    //             Assert.Equal(originalChessboard.Board[i, j].Piece?.GetType(), copiedChessboard.Board[i, j].Piece?.GetType());
    //             Assert.Equal(originalChessboard.Board[i, j].Piece?.Color, copiedChessboard.Board[i, j].Piece?.Color);
    //         }
    //     }
    // }

    [Fact]
    public void TestEchecMat()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);

        // Act
        bool result = chessboard.EchecMat(king, kingCase);

        // Assert
        Assert.False(result); // Assuming there are no other pieces on the board, the king should not be in checkmate
    }
    [Fact]
    public void TestEchecMatParReineEtTour()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Rook? rook = new Rook(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(queen, 1, 1);
        chessboard.AddPiece(rook, 0, 1);

        // Act
        bool result = chessboard.EchecMat(king, kingCase);

        // Assert
        Assert.True(result); // The queen and rook should put the king in checkmate
    }

    [Fact]
    public void TestEchecMatParFouEtTour()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Bishop? bishop = new Bishop(Color.Black, 1);
        Rook? rook = new Rook(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(bishop, 1, 1);
        chessboard.AddPiece(rook, 0, 2);

        // Act
        bool result = chessboard.EchecMat(king, kingCase);

        // Assert
        Assert.False(result); // The bishop and rook should put the king in checkmate
    }
    [Fact]
    public void TestEchecMatParReineEtFou()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Bishop? bishop = new Bishop(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(queen, 1, 1);
        chessboard.AddPiece(bishop, 2, 2);

        // Act
        bool result = chessboard.EchecMat(king, kingCase);

        // Assert
        Assert.True(result); // The queen and bishop should put the king in checkmate
    }
    [Fact]
    public void TestEchecMatParCavalierEtTour()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Knight? knight = new Knight(Color.Black, 1);
        Rook? rook = new Rook(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(knight, 2, 1);
        chessboard.AddPiece(rook, 0, 2);

        // Act
        bool result = chessboard.EchecMat(king, kingCase);

        // Assert
        Assert.False(result); // The knight and rook should put the king in checkmate
    }

    [Fact]
    public void TestEchecMatFauxAvecTour()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Rook? rook1 = new Rook(Color.White, 2);
        Rook? rook2 = new Rook(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        Case rookCase1 = new Case(2, 1, rook1);
        Case rookCase2 = new Case(0, 2, rook2);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(rook1, 2, 1);
        chessboard.AddPiece(rook2, 0, 2);

        bool result = chessboard.EchecMat(king, kingCase);
        Assert.False(result);
    }

    [Fact]
    public void TestEchecMatFauxAvecRene()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen1 = new Queen(Color.White, 2);
        Queen? queen2 = new Queen(Color.Black, 1);
        Case kingCase = new Case(0, 0, king);
        Case rookCase1 = new Case(2, 1, queen1);
        Case rookCase2 = new Case(0, 2, queen2);
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(queen1, 1, 0);
        chessboard.AddPiece(queen2, 0, 2);

        bool result = chessboard.EchecMat(king, kingCase);
        Assert.False(result);
    }


}

