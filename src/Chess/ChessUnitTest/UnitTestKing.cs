namespace ChessUnitTest;
using ChessLibrary;

public class UnitTestKing
{
    [Theory]
    [MemberData(nameof(TestData.ValidKingPositionsData), MemberType = typeof(TestData))]
    public void CanMove_OneSquareAway_ReturnsTrue(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var king = new King(Color.White, 1);
        
        // Act
        var result = king.CanMove(x1, y1, x2, y2);
        
        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidKingPositionsData), MemberType = typeof(TestData))]
    public void CanMove_TwoSquaresAway_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var king = new King(Color.White, 1);
        
        // Act and Assert
        Assert.Throws<InvalidMovementException>(() => king.CanMove(x1, y1, x2, y2));
    }
    
    [Fact]
    public void TestKingPossibleMoves_WithEnemyPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);
        chessboard.AddPiece(king, 0, 0);
        Pawn enemyPawn = new Pawn(Color.Black, 2);
        chessboard.AddPiece(enemyPawn, 0, 1);

        // Act
        var possibleMoves = king.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.Contains(possibleMoves, c => c!.Column == 0 && c.Line == 1);
    }

    [Fact]
    public void TestKingPossibleMoves_WithNullChessboard()
    {
        // Arrange
        King king = new King(Color.White, 1);
        Case kingCase = new Case(0, 0, king);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => king.PossibleMoves(kingCase, null!));
    }
    
    [Fact]
    public void KingCanCaptureOppositeColorPiece()
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);
        var king = new King(Color.White, 1);
        var pawn = new Pawn(Color.Black, 2);
        chessboard.Board[4, 4] = new Case(4, 4, king);
        chessboard.Board[5, 5] = new Case(5, 5, pawn);

        // Act
        var result = king.PossibleMoves(chessboard.Board[4, 4], chessboard);

        // Assert
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 5);
    }
    
    [Fact]
    public void CanEat_ValidMoves_ReturnsCorrectCases()
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);
        var king = new King(Color.White, 1);
        chessboard.Board[4, 4] = new Case(4, 4, king);

        // Add some enemy pieces around the king
        chessboard.Board[3, 3] = new Case(3, 3, new Pawn(Color.Black, 2));
        chessboard.Board[4, 3] = new Case(4, 3, new Pawn(Color.Black, 3));
        chessboard.Board[5, 3] = new Case(5, 3, new Pawn(Color.Black, 4));

        // Act
        var result = king.CanEat(chessboard.Board[4, 4], chessboard);

        // Assert
        Assert.Equal(8, result.Count); // King should be able to eat in 8 directions
        Assert.Contains(result, c => c!.Column == 3 && c.Line == 3); // King should be able to eat the pawn at (3,3)
        Assert.Contains(result, c => c!.Column == 4 && c.Line == 3); // King should be able to eat the pawn at (4,3)
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 3); // King should be able to eat the pawn at (5,3)
    }
    
    [Fact]
    public void TestKingPossibleMoves_WithEmptyCase()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Case kingCase = new Case(4, 4, king);
        chessboard.AddPiece(king, 4, 4);

        // Act
        var possibleMoves = king.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.Contains(possibleMoves, c => c!.Column == 5 && c.Line == 5);
    }

    [Fact]
    public void TestKingPossibleMoves_WithOppositeColorPiece()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Pawn enemyPawn = new Pawn(Color.Black, 2);
        Case kingCase = new Case(4, 4, king);
        chessboard.AddPiece(king, 4, 4);
        chessboard.AddPiece(enemyPawn, 5, 5);

        // Act
        var possibleMoves = king.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.Contains(possibleMoves, c => c!.Column == 5 && c.Line == 5);
    }

    [Fact]
    public void TestKingPossibleMoves_WithOppositeColorKing()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        King blackKing = new King(Color.Black, 2);
        Case kingCase = new Case(4, 4, whiteKing);
        chessboard.AddPiece(whiteKing, 4, 4);
        chessboard.AddPiece(blackKing, 5, 5);

        // Act
        var possibleMoves = whiteKing.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.DoesNotContain(possibleMoves, c => c!.Column == 5 && c.Line == 4);
    }

    [Fact]
    public void TestKingPossibleMoves_WithOutOfBoundsCase()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King king = new King(Color.White, 1);
        Case kingCase = new Case(7, 7, king);
        chessboard.AddPiece(king, 7, 7);

        // Act
        var possibleMoves = king.PossibleMoves(kingCase, chessboard);

        // Assert
        Assert.DoesNotContain(possibleMoves, c => c!.Column == 8 && c.Line == 8);
    }
    [Fact]
    public void PetitRoque_WhiteKingPerfectConditions_CastlingPerformed()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);

        // Arrange
        var king = new King(Color.White,1);
        var rook = new Rook(Color.White,2);
        chessboard.AddPiece(king,4, 7);  // Position initiale du roi
        chessboard.AddPiece(rook, 7, 7);  // Position initiale de la tour
        // Act
        king.PetitRoque(chessboard);

        // Assert
        Assert.Equal(king, chessboard.Board[6, 7]!.Piece);  // Le roi doit être déplacé en G1
        Assert.Equal(rook, chessboard.Board[5, 7]!.Piece);  // La tour doit être déplacée en F1
    }

    [Fact]
    public void PetitRoque_WhiteKingInvalidConditions_CastlingNotPerformed()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White,1);
        var rook = new Rook(Color.White,2);
        var pawn = new Pawn(Color.White, 3);
        chessboard.Board[4, 7] = new Case(4, 7, king);  // Position initiale du roi
        chessboard.Board[7, 7] = new Case(7, 7, rook);  // Position initiale de la tour
        chessboard.Board[5, 7] = new Case(5, 7, pawn);  // Case entre le roi et la tour bloquée par un pion
        chessboard.Board[6, 7] = new Case(6, 7, null);
        chessboard.AddPiece(king, 4, 7);
        chessboard.AddPiece(rook, 7, 7);
        chessboard.AddPiece(pawn, 5, 7);

        // Assert
        Assert.Throws<ArgumentException>(()=>king.PetitRoque(chessboard));
        Assert.Null(chessboard.Board[6, 7]!.Piece);  // Le roi ne doit pas être déplacé
        Assert.NotNull(chessboard.Board[5, 7]!.Piece);  // La case entre doit toujours avoir le pion
    }
    [Fact]
    public void GrandRoque_KingAlreadyMoved_ThrowsArgumentException()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White,1);
        var rook = new Rook(Color.White,2);
        king.FirstMove = false;  // Le roi a déjà bougé
        chessboard.Board[4, 7] = new Case(4, 7, king);
        chessboard.Board[0, 7] = new Case(0, 7, rook);

        // Assert
        Assert.Throws<ArgumentException>(() => king.GrandRoque(chessboard));
    }

    [Fact]
    public void GrandRoque_RookAlreadyMoved_ThrowsArgumentException()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White,1);
        var rook = new Rook(Color.White,2);
        rook.FirstMove = false;  // La tour a déjà bougé
        chessboard.Board[4, 7] = new Case(4, 7, king);
        chessboard.Board[0, 7] = new Case(0, 7, rook);

        // Assert
        Assert.Throws<ArgumentException>(() => king.GrandRoque(chessboard));
    }

    [Fact]
    public void GrandRoque_IntermediateSquaresNotFree_ThrowsArgumentException()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White, 1);
        var rook = new Rook(Color.White, 2);
        var pawn = new Pawn(Color.White, 3);
        chessboard.Board[4, 7] = new Case(4, 7, king);
        chessboard.Board[0, 7] = new Case(0, 7, rook);
        chessboard.Board[1, 7] = new Case(1, 7, pawn);  // Blocage par un pion
        chessboard.Board[2, 7] = new Case(2, 7, null);
        chessboard.Board[3, 7] = new Case(3, 7, null);

        // Assert
        Assert.Throws<ArgumentException>(() => king.GrandRoque(chessboard));
    }

    [Fact]
    public void GrandRoque_PathAttacked_ThrowsArgumentException()
    {
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White, 1);
        var rook = new Rook(Color.White, 2);
        var queen = new Queen(Color.Black, 1);
        chessboard.Board[4, 7] = new Case(4, 7, king);
        chessboard.Board[0, 7] = new Case(0, 7, rook);
        chessboard.Board[1, 7] = new Case(1, 7, null);
        chessboard.Board[2, 7] = new Case(2, 7, null);
        chessboard.Board[3, 7] = new Case(3, 7, null);

        chessboard.Board[2,0] = new Case(2, 0, queen);
        chessboard.AddPiece(queen, 2, 0);
        chessboard.AddPiece(rook, 0, 7);
        chessboard.AddPiece(king, 4, 7);


        // Assert
        Assert.Throws<ArgumentException>(() => king.GrandRoque(chessboard));
    }

    [Fact]
    public void GrandRoque_ValidConditions_PerformsCastling()
    {
        var chessboard = new Chessboard(new Case[8, 8], true);
        // Arrange
        var king = new King(Color.White, 1);
        var rook = new Rook(Color.White, 2);
        chessboard.Board[4, 7] = new Case(4, 7, king);
        chessboard.Board[0, 7] = new Case(0, 7, rook);
        chessboard.AddPiece(king, 4, 7);
        chessboard.AddPiece(rook, 0, 7);


        // Act
        king.GrandRoque(chessboard);

        // Assert
        Assert.NotNull(chessboard.Board[2, 7]!.Piece);
        Assert.Equal(typeof(King), chessboard.Board[2, 7]!.Piece!.GetType());
        Assert.NotNull(chessboard.Board[3, 7]!.Piece);
        Assert.Equal(typeof(Rook), chessboard.Board[3, 7]!.Piece!.GetType());
    }
}