using CaroGame.Api.Services;
using FluentAssertions;
using Xunit;

namespace CaroGame.Tests;

public class GameServiceTests
{
    private static char[,] EmptyBoard() => new char[15, 15];

    [Fact]
    public void CheckWin_FiveInARowHorizontally_ReturnsTrue()
    {
        // Arrange
        var board = EmptyBoard();
        for (var col = 0; col < 5; col++)
        {
            board[7, col] = 'X';
        }

        // Act
        var won = GameService.CheckWin(board, 7, 4, 'X');

        // Assert
        won.Should().BeTrue();
    }

    [Fact]
    public void CheckWin_OnlyFourInARow_ReturnsFalse()
    {
        // Arrange
        var board = EmptyBoard();
        for (var col = 0; col < 4; col++)
        {
            board[7, col] = 'O';
        }

        // Act
        var won = GameService.CheckWin(board, 7, 3, 'O');

        // Assert
        won.Should().BeFalse();
    }
}
