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
    public void IsMovePossible_EmptyBoard_ReturnsCorrectMoves()
    {
        // Arrange
        var bishop = new Bishop(Color.White, 1);
        var chessboard = new Chessboard(new Case[8, 8], true);
        var caseInitial = new Case(4, 4, bishop);
        Case newCase = new Case(0,0,null);
        // Act
        var result = bishop.PossibleMoves(caseInitial, chessboard);
        var move = chessboard.IsMoveValid(result,newCase);
        // Assert
        Assert.True(move);
    }

    [Theory]
    [MemberData(nameof(TestData.ValidIsMoveValidData), MemberType = typeof(TestData))]
    public void IsMoveValid_Valid(List<Case> lcase, Case Final)
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);

        // Act & Assert
        Assert.True(chessboard.IsMoveValid(lcase, Final));
    }


    [Theory]
    [MemberData(nameof(TestData.InvalidIsMoveValidData), MemberType = typeof(TestData))]
    public void IsMoveValid_Invalid(List<Case> lcase, Case Final)
    {
        // Arrange
        var chessboard = new Chessboard(new Case[8, 8], true);

        // Act & Assert
        Assert.True(chessboard.IsMoveValid(lcase, Final));
    }

}

