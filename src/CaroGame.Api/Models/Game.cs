namespace CaroGame.Api.Models;

public enum GameStatus
{
    InProgress,
    Won,
    Draw
}

/// <summary>
/// Một ván Caro (Gomoku). Entity mutable, lưu trong store in-memory.
/// </summary>
public class Game
{
    public Guid Id { get; init; } = Guid.NewGuid();

    // LỖI #10 (Naming): field private đáng lẽ phải là `_board`.
    private readonly char[,] Board = new char[15, 15];

    public char CurrentPlayer { get; set; } = 'X';

    public GameStatus Status { get; set; } = GameStatus.InProgress;

    public char? Winner { get; set; }

    // LỖI #1 (Forbidden): DateTime.Now — convention bắt buộc UtcNow.
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    /// <summary>Token để người thứ hai join ván — sinh ở GameService.</summary>
    public string JoinToken { get; set; } = string.Empty;

    public char GetCell(int row, int col) => Board[row, col];

    public void SetCell(int row, int col, char symbol) => Board[row, col] = symbol;

    public char[,] Snapshot()
    {
        var copy = new char[15, 15];
        Array.Copy(Board, copy, Board.Length);
        return copy;
    }
}
