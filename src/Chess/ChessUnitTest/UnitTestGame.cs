using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUnitTest;
using ChessLibrary;


public class UnitTestGame
{
    [Theory]
    [MemberData(nameof(TestData.InvalidBishopPositionsData), MemberType = typeof(TestData))]
    public void CanMove_InvalidMove_ThrowsException(int x1, int y1, int x2, int y2)
    {
        // Arrange
        var bishop = new Bishop(ChessLibrary.Color.White, 1);

        // Act & Assert
        Assert.Throws<InvalidMovementException>(() => bishop.CanMove(x1, y1, x2, y2));
    }

    [Fact]
    public void PossibleMoves_BoardWithOtherPieces_ReturnsCorrectMoves()
    {
        // Arrange
        var bishop = new Bishop(ChessLibrary.Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, bishop);
        chessboard.Board[4, 4] = caseInitial;

        chessboard.Board[5, 5] = new Case(5, 5, new Pawn(ChessLibrary.Color.Black, 2));

        chessboard.Board[3, 3] = new Case(3, 3, new Pawn(ChessLibrary.Color.White, 3));

        // Act
        var result = bishop.PossibleMoves(caseInitial, chessboard);

        // Assert
        Assert.Contains(result, c => c!.Column == 5 && c.Line == 5);
        Assert.DoesNotContain(result, c => c!.Column == 2 && c.Line == 2);
    }

    [Fact]
    public void IsCheck_WhitePlayerInCheckByRook_ReturnsTrue()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        Rook blackRook = new Rook(Color.Black, 2);
        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackRook, 0, 7);
        Game game = new Game();
        game.Board = chessboard;

        // Act
        bool result = game.IsCheck(game.Player2);

        // Assert
        Assert.True(result); // The rook should put the black king in check
        Assert.True(game.BlackCheck);
    }

    [Fact]
    public void IsCheck_NoPlayerInCheck_ReturnsFalse()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        King blackKing = new King(Color.Black, 2);
        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackKing, 7, 7);
        Game game = new Game();
        game.Board = chessboard;

        // Act
        bool whiteCheck = game.IsCheck(game.Player1);
        bool blackCheck = game.IsCheck(game.Player2);

        // Assert
        Assert.False(whiteCheck); // White king should not be in check
        Assert.False(blackCheck); // Black king should not be in check
        Assert.False(game.WhiteCheck);
        Assert.False(game.BlackCheck);
    }

    [Fact]
    public void GameOver_KingCanBeDefended_ReturnsFalse()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        King blackKing = new King(Color.Black, 2);
        Rook blackRook = new Rook(Color.Black, 2);
        Pawn whitePawn = new Pawn(Color.White, 3);
        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackRook, 0, 1);
        chessboard.AddPiece(whitePawn, 1, 1);
        chessboard.AddPiece(blackKing, 7, 7);
        Game game = new Game();
        game.Board = chessboard;

        // Act
        bool result = game.GameOver(game.Player2);

        // Assert
        Assert.False(result); // The white king should be able to be defended by the pawn
    }

    [Fact]
    public void GameOver_NoCheckmate_ReturnsFalse()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        King blackKing = new King(Color.Black, 2);
        chessboard.AddPiece(whiteKing, 0, 0);
        chessboard.AddPiece(blackKing, 7, 7);
        Game game = new Game();
        game.Board = chessboard;

        // Act
        bool result = game.GameOver(game.Player1);

        // Assert
        Assert.False(result); // There should be no checkmate
    }


    [Fact]
    public void MovePiece_InvalidMove_ThrowsInvalidOperationException()
    {
        // Arrange
        var game = new Game();
        var chessboard = new Chessboard(new Case[8, 8], true);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => game.MovePiece(null, null, chessboard, game.Player1));
    }

    [Fact]
    public void MovePiece_NotPlayersTurn_ResetsPossibleMovesAndRaisesError()
    {
        // Arrange
        var game = new Game();
        var chessboard = new Chessboard(new Case[8, 8], true);
        var initialCase = new Case(0, 0, new Rook(Color.White, 1));
        var finalCase = new Case(0, 1, null);
        chessboard.AddPiece(initialCase.Piece, 0, 0);

        bool errorRaised = false;
        game.ErrorPlayerTurnNotified += (sender, args) => { errorRaised = true; };


        // Act
        game.MovePiece(initialCase, finalCase, chessboard, game.Player2);

        // Assert
        Assert.True(errorRaised);
        Assert.Null(finalCase.Piece); // The piece should not have moved
    }
    [Fact]
    public void MovePiece_ValidMove_MovesPieceSuccessfully()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Rook whiteRook = new Rook(Color.White, 1);
        Case initialCase = new Case(0, 0, whiteRook);
        Case finalCase = new Case(0, 1, null);
        King king = new King(Color.White, 2);
        King king2 = new King(Color.Black, 1);
        chessboard.AddPiece(whiteRook, 0, 0);
        chessboard.AddPiece(king, 0, 7);
        chessboard.AddPiece(king2 , 7, 7);       


        Game game = new Game();
        game.Board = chessboard;

        // Act
        game.MovePiece(initialCase, finalCase, chessboard, game.Player1);

        // Assert
        Assert.Null(initialCase.Piece); // The piece should have moved
        Assert.NotNull(finalCase.Piece);
        Assert.IsType<Rook>(finalCase.Piece);
        Assert.Equal(whiteRook, finalCase.Piece); // The moved piece should be the same rook
    }

    [Fact]
    public void MovePiece_ValidMove_MovesPieceSuccessfully2()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        Rook whiteRook = new Rook(Color.White, 1);
        Case initialCase = new Case(0, 0, whiteRook);
        Case finalCase = new Case(0, 1, null);
        King king = new King(Color.White, 2);
        King king2 = new King(Color.Black, 1);
        chessboard.AddPiece(whiteRook, 0, 0);
        chessboard.AddPiece(king, 0, 7);
        chessboard.AddPiece(king2 , 7, 7);

        Game game = new Game();
        game.Board = chessboard;

        // Act
        game.MovePiece(initialCase, finalCase, chessboard, game.Player1);

        // Assert
        Assert.Null(initialCase.Piece); // The piece should have moved
        Assert.NotNull(finalCase.Piece);
        Assert.IsType<Rook>(finalCase.Piece);
        Assert.Equal(whiteRook, finalCase.Piece); // The moved piece should be the same rook
    }
    [Fact]
    public void MovePiece_PerformWhiteKingsideCastling_MovesKingAndRookSuccessfully()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        Rook whiteRook = new Rook(Color.White, 2);
        chessboard.AddPiece(whiteKing, 4, 7);
        chessboard.AddPiece(whiteRook, 7, 7);


        Case initialKingCase = chessboard.Board[4, 7]!;
        Case initialRookCase = chessboard.Board[7, 7]!;
        Case finalKingCase = chessboard.Board[6, 7]!;
        Case finalRookCase = chessboard.Board[5, 7]!;


        Game game = new Game();
        game.Board = chessboard;

        // Act
        game.MovePiece(initialKingCase, initialRookCase, chessboard, game.Player1);

        // Assert
        Assert.Null(initialKingCase!.Piece);
        Assert.Null(initialRookCase!.Piece);
        Assert.NotNull(finalKingCase!.Piece);
        Assert.NotNull(finalRookCase!.Piece);
        Assert.IsType<King>(finalKingCase.Piece);
        Assert.IsType<Rook>(finalRookCase.Piece);
        Assert.Equal(whiteKing, finalKingCase.Piece); // Ensure the king is moved correctly
        Assert.Equal(whiteRook, finalRookCase.Piece); // Ensure the rook is moved correctly
    }


    [Fact]
    public void MovePiece_PerformWhiteQueensideCastling_MovesKingAndRookSuccessfully()
    {
        // Arrange
        Chessboard chessboard = new Chessboard(new Case[8, 8], true);
        King whiteKing = new King(Color.White, 1);
        Rook whiteRook = new Rook(Color.White, 2);

        chessboard.AddPiece(whiteKing, 4, 7);
        chessboard.AddPiece(whiteRook, 0, 7);
        Case initialKingCase = chessboard.Board[4, 7]!;
        Case initialRookCase = chessboard.Board[0, 7]!;
        Case finalKingCase = chessboard.Board[2, 7]!;
        Case finalRookCase = chessboard.Board[3, 7]!;
        chessboard.Board[4, 7] = initialKingCase;
        chessboard.Board[0, 7] = initialRookCase;
        chessboard.Board[2, 7] = finalKingCase;
        chessboard.Board[3, 7] = finalRookCase;

        Game game = new Game();
        game.Board = chessboard;

        // Act
        game.MovePiece(initialKingCase, initialRookCase, chessboard, game.Player1);

        // Assert
        Assert.Null(initialKingCase!.Piece);
        Assert.Null(initialRookCase!.Piece);
        Assert.NotNull(finalKingCase!.Piece);
        Assert.NotNull(finalRookCase!.Piece);
        Assert.IsType<King>(finalKingCase.Piece);
        Assert.IsType<Rook>(finalRookCase.Piece);
        Assert.Equal(whiteKing, finalKingCase.Piece); // Ensure the king is moved correctly
        Assert.Equal(whiteRook, finalRookCase.Piece); // Ensure the rook is moved correctly
    }

    
}