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
        Case[,] tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(tcase, true);

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
        Case[,] tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(tcase, false);

        // Act
        chessboard.InitializeChessboard();

        // Assert
        // Vérifie que les pions sont à leur place initiale
        for (int i = 0; i < 8; i++)
        {
            Assert.IsType<Pawn>(chessboard.Board[i, 1].Piece);
            Assert.Equal(Color.White, chessboard.Board[i, 1].Piece!.Color);

            Assert.IsType<Pawn>(chessboard.Board[i, 6].Piece);
            Assert.Equal(Color.Black, chessboard.Board[i, 6].Piece!.Color);
        }
    }

    [Theory]
    [InlineData(0, 1, typeof(Pawn), Color.White)]
    [InlineData(0, 6, typeof(Pawn), Color.Black)]
    public void TestPiecePlacement(int x, int y, Type expectedType, Color expectedColor)
    {
        // Arrange
        Case[,] tcase = new Case[8, 8];
        Chessboard chessboard = new Chessboard(tcase, false);

        // Act
        chessboard.InitializeChessboard();

        // Assert
        Assert.IsType(expectedType, chessboard.Board[x, y].Piece);
        Assert.Equal(expectedColor, chessboard.Board[x, y].Piece!.Color);
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
        Case final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(Lcase, final);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public void TestIsMoveValidValidMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(lcase, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsMoveValid_InvalidMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case final = new Case(3, 3, null);

        // Act
        bool result = chessboard.IsMoveValid(lcase, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestIsMoveValid_MoveToOccupiedCase()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, new Pawn(Color.White, 1)),
            new Case(2, 2, null)
        };
        Case final = new Case(1, 1, new Pawn(Color.White, 1));

        // Act
        bool result = chessboard.IsMoveValid(lcase, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIsMoveValid_EmptyPossibleMoves()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> lcase = new List<Case>();
        Case final = new Case(1, 1, null);

        // Act
        bool result = chessboard.IsMoveValid(lcase, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestIsMoveValid_FinalCaseOutOfBounds()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        List<Case> lcase = new List<Case>
        {
            new Case(0, 0, null),
            new Case(1, 1, null),
            new Case(2, 2, null)
        };
        Case final = new Case(8, 8, null); // Out of bounds case

        // Act
        bool result = chessboard.IsMoveValid(lcase, final);

        // Assert
        Assert.False(result);
    }
    [Fact]
    public void TestMovePiece_InvalidPawnMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Case initial = new Case(0, 1, piece);
        Case final = new Case(1, 2, null); // Diagonal move without capture
        chessboard.AddPiece(piece, 0, 1);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece_ValidRookMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Rook(Color.White, 1);
        Case initial = new Case(0, 0, piece);
        Case final = new Case(0, 5, null);
        chessboard.AddPiece(piece, 0, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidRookMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Rook(Color.White, 1);
        Case initial = new Case(0, 0, piece);
        Case final = new Case(1, 1, null); // Diagonal move
        chessboard.AddPiece(piece, 0, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece_ValidKnightMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Knight(Color.White, 1);
        Case initial = new Case(1, 0, piece);
        Case final = new Case(2, 2, null);
        chessboard.AddPiece(piece, 1, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_ValidKingMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new King(Color.White, 1);
        Case initial = new Case(4, 4, piece);
        Case final = new Case(4, 5, null);
        chessboard.AddPiece(piece, 4, 4);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidKingMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new King(Color.White, 1);
        Case initial = new Case(4, 4, piece);
        Case final = new Case(4, 6, null); // Move more than one square
        chessboard.AddPiece(piece, 4, 4);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }
    [Fact]
    public void TestMovePiece_ValidBishopMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Bishop(Color.White, 1);
        Case initial = new Case(2, 0, piece);
        Case final = new Case(5, 3, null);
        chessboard.AddPiece(piece, 2, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidBishopMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Bishop(Color.White, 1);
        Case initial = new Case(2, 0, piece);
        Case final = new Case(4, 1, null); // Horizontal move
        chessboard.AddPiece(piece, 2, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece_ValidQueenMove_Diagonal()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Queen(Color.White, 1);
        Case initial = new Case(3, 0, piece);
        Case final = new Case(6, 3, null);
        chessboard.AddPiece(piece, 3, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_ValidQueenMove_Vertical()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Queen(Color.White, 1);
        Case initial = new Case(3, 0, piece);
        Case final = new Case(3, 4, null);
        chessboard.AddPiece(piece, 3, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidQueenMove()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Queen(Color.White, 1);
        Case initial = new Case(3, 0, piece);
        Case final = new Case(4, 2, null); // L-shaped move
        chessboard.AddPiece(piece, 3, 0);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece_ValidPawnMove_TwoStepsForward()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Case initial = new Case(0, 1, piece);
        Case final = new Case(0, 3, null);
        chessboard.AddPiece(piece, 0, 1);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidPawnMove_DiagonalWithoutCapture()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Case initial = new Case(0, 1, piece);
        Case final = new Case(1, 2, null); // Diagonal move without capture
        chessboard.AddPiece(piece, 0, 1);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestMovePiece_ValidPawnMove_CaptureDiagonally()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Piece? opponentPiece = new Pawn(Color.Black, 2);
        Piece? king = new King(Color.White, 1);
        Case initial = new Case(0, 1, piece);
        Case final = new Case(1, 2, opponentPiece);
        
        chessboard.AddPiece(piece, 0, 1);
        chessboard.AddPiece(opponentPiece, 1, 2);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestMovePiece_InvalidPawnMove_Backward()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Piece? piece = new Pawn(Color.White, 1);
        Case initial = new Case(0, 1, piece);
        Case final = new Case(0, 0, null); // Backward move
        chessboard.AddPiece(piece, 0, 1);
        Piece? king = new King(Color.White, 1);
        chessboard.AddPiece(king, 7, 7);

        // Act
        bool result = chessboard.CanMovePiece(piece, initial, final);

        // Assert
        Assert.False(result);
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
    public void TestModifPawn_WhitePawnToQueen()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Pawn? pawn = new Pawn(Color.White, 1);
        Case caseForPawn = new Case(0, 7, pawn);
        Queen newPiece = new Queen(Color.White, 2);
        chessboard.AddPiece(pawn, 0, 7); // Ajoute le pion à l'échiquier

        // Act
        chessboard.ModifPawn(pawn, newPiece, caseForPawn);

        // Assert
        Assert.Contains(chessboard.WhitePieces, copieces => copieces.piece == newPiece && copieces.CaseLink == caseForPawn);
        Assert.DoesNotContain(chessboard.WhitePieces, copieces => copieces.piece == pawn && copieces.CaseLink == caseForPawn);
    }

    [Fact]
    public void TestModifPawn_BlackPawnToQueen()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Pawn? pawn = new Pawn(Color.Black, 1);
        Case caseForPawn = new Case(0, 0, pawn);
        Queen newPiece = new Queen(Color.Black, 2);
        chessboard.AddPiece(pawn, 0, 0); // Ajoute le pion à l'échiquier

        // Act
        chessboard.ModifPawn(pawn, newPiece, caseForPawn);

        // Assert
        Assert.Contains(chessboard.BlackPieces, copieces => copieces.piece == newPiece && copieces.CaseLink == caseForPawn);
        Assert.DoesNotContain(chessboard.BlackPieces, copieces => copieces.piece == pawn && copieces.CaseLink == caseForPawn);
    }

    [Fact]
    public void TestModifPawn_NullPawn()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Queen newPiece = new Queen(Color.White, 2);
        Case caseForPawn = new Case(0, 7, newPiece);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => chessboard.ModifPawn(null, newPiece, caseForPawn));
    }

    [Fact]
    public void TestModifPawn_NullNewPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Pawn? pawn = new Pawn(Color.White, 1);
        Case caseForPawn = new Case(0, 7, pawn);
        chessboard.AddPiece(pawn, 0, 7); // Ajoute le pion à l'échiquier

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => chessboard.ModifPawn(pawn, null, caseForPawn));
    }

    [Fact]
    public void TestModifPawn_ReplaceExistingPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Pawn? pawn = new Pawn(Color.White, 1);
        Queen existingPiece = new Queen(Color.White, 2);
        Case caseForPawn = new Case(0, 7, existingPiece);
        Queen newPiece = new Queen(Color.White, 3);
        chessboard.AddPiece(pawn, 0, 7); // Ajoute le pion à l'échiquier
        chessboard.AddPiece(existingPiece, 0, 7); // Ajoute une pièce existante sur la même case

        // Act
        chessboard.ModifPawn(pawn, newPiece, caseForPawn);

        // Assert
        Assert.Contains(chessboard.WhitePieces, copieces => copieces.piece == newPiece && copieces.CaseLink == caseForPawn);
        Assert.DoesNotContain(chessboard.WhitePieces, copieces => copieces.piece == pawn && copieces.CaseLink == caseForPawn);
        Assert.DoesNotContain(chessboard.WhitePieces, copieces => copieces.piece == existingPiece && copieces.CaseLink == caseForPawn);
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
        chessboard.AddPiece(king, 0, 0);
        chessboard.AddPiece(rook, 0, 7);

        // Act
        bool result = chessboard.Echec(king, kingCase);

        // Assert
        Assert.True(result); // The rook should put the king in check
    }

    [Fact]
    public void TestEchecParPlusieursPieces()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Rook? rook = new Rook(Color.Black, 2);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(queen, 4, 7);
        chessboard.AddPiece(rook, 4, 3);

        // Act
        bool result = chessboard.Echec(king, new Case(4, 4, king));
        bool result2 = chessboard.EchecMat(king, new Case(4, 4, king));
        // Assert
        Assert.True(result); // Both the queen and the rook should put the king in check
        Assert.False(result2); // The king should not be in checkmate
    }
    [Fact]
    public void TestEchecFauxParBlocage()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Pawn? blockingPawn = new Pawn(Color.White, 2);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(queen, 4, 7);
        chessboard.AddPiece(blockingPawn, 4, 5);

        // Act
        bool result = chessboard.Echec(king, new Case(4, 4, king));

        // Assert
        Assert.False(result); // The pawn blocks the queen's attack on the king
    }
    [Fact]
    public void TestEchecVraiApresDeblocage()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Pawn? blockingPawn = new Pawn(Color.White, 2);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(queen, 4, 7);
        chessboard.AddPiece(blockingPawn, 4, 5);

        // Act
        chessboard.Board[4, 5].Piece = null; // Remove the blocking pawn
        bool result = chessboard.Echec(king, new Case(4, 4, king));

        // Assert
        Assert.True(result); // Removing the pawn should now expose the king to check from the queen
    }
    [Fact]
    public void TestNonEchecMalgreProximite()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Knight? knight = new Knight(Color.Black, 1);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(knight, 4, 5); // Position where the knight does not threaten the king directly

        // Act
        bool result = chessboard.Echec(king, new Case(4, 4, king));

        // Assert
        Assert.False(result); // The knight is not putting the king in check from this position
    }
    [Fact]
    public void TestSauvegardeRoiParDeplacementPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? king = new King(Color.White, 1);
        Queen? queen = new Queen(Color.Black, 1);
        Knight? knight = new Knight(Color.White, 2);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(queen, 4, 7);
        chessboard.AddPiece(knight, 2, 5);

        bool result = chessboard.Echec(king, new Case(4, 4, king));
        bool result2 = chessboard.EchecMat(king, new Case(4, 4, king));
        // Simulate moving knight to block the attack
        chessboard.Board[2, 5].Piece = null;
        chessboard.AddPiece(knight, 4, 6); // Knight is moved to block the queen

        // Act
        bool result3 = chessboard.Echec(king, new Case(4, 4, king));

        // Assert
        Assert.True(result); // The knight's new position saves the king from check
        Assert.False(result2); // The king should not be in checkmate
        Assert.False(result3); // The king should not be in check
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
    public void TestEchecMatParReineEtTour()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? whiteKing = new King(Color.White, 1);
        Queen? blackQueen = new Queen(Color.Black, 2);
        Rook? blackRook = new Rook(Color.Black, 3);

        Case kingCase = new Case(0, 0, whiteKing);

        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackQueen, 1, 1);
        chessboard.AddPiece(blackRook, 0, 1);

        // Act
        bool result = chessboard.EchecMat(whiteKing, kingCase);

        // Assert
        Assert.True(result); // The queen and rook should put the king in checkmate
    }
    [Fact]
    public void TestEchecMatParDeuxTours()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King? whiteKing = new King(Color.White, 1);
        Rook? blackRook1 = new Rook(Color.Black, 2);
        Rook? blackRook2 = new Rook(Color.Black, 3);

        Case kingCase = new Case(0, 0, whiteKing);

        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackRook1, 0, 2);
        chessboard.AddPiece(blackRook2, 1, 2);

        // Act
        bool result = chessboard.EchecMat(whiteKing, kingCase);

        // Assert
        Assert.True(result); // The two rooks should put the king in checkmate
    }

}

