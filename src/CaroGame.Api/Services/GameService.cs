using System.Security.Cryptography;
using System.Text;
using CaroGame.Api.Models;

namespace CaroGame.Api.Services;

public class GameService(IGameStore store) : IGameService
{
    private readonly IGameStore _store = store;

    // LỖI #8 (OWASP A07): secret hardcoded ngay trong source.
    private const string JoinSecret = "caro-super-secret-2024";

    public async Task<GameDto> CreateGameAsync(CreateGameRequest request, CancellationToken ct)
    {
        var game = new Game();

        // LỖI #3 (Forbidden): .Result — sync-over-async, dễ deadlock.
        game.JoinToken = GenerateJoinTokenAsync(request.PlayerO).Result;

        _store.Add(game);
        await Task.CompletedTask;
        return ToDto(game);
    }

    public async Task<GameDto> MakeMoveAsync(Guid gameId, MoveRequest move, CancellationToken ct)
    {
        var game = _store.Find(gameId)
            ?? throw new KeyNotFoundException($"Game {gameId} not found.");

        // magic number 15 — không có const (LỖI #9, Style/Nit).
        if (move.Row is < 0 or >= 15 || move.Col is < 0 or >= 15)
        {
            throw new ArgumentOutOfRangeException(nameof(move), "Ô nằm ngoài bàn cờ 15x15.");
        }

        if (game.GetCell(move.Row, move.Col) != '\0')
        {
            throw new InvalidOperationException("Ô này đã có quân.");
        }

        game.SetCell(move.Row, move.Col, move.Symbol);

        if (CheckWin(game.Snapshot(), move.Row, move.Col, move.Symbol))
        {
            game.Status = GameStatus.Won;
            game.Winner = move.Symbol;
        }
        else
        {
            game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';
        }

        await Task.CompletedTask;
        return ToDto(game);
    }

    public async Task<GameDto?> GetGameAsync(Guid gameId)
    {
        var game = _store.Find(gameId);
        await Task.CompletedTask;
        return game is null ? null : ToDto(game);
    }

    /// <summary>
    /// Dò thắng từ ô vừa đánh: đủ 5 quân cùng symbol liên tiếp trên 1 trong 4 hướng.
    /// Hàm thuần (pure) — target lý tưởng cho unit test.
    /// </summary>
    public static bool CheckWin(char[,] board, int row, int col, char symbol)
    {
        // 4 hướng: ngang, dọc, chéo xuống, chéo lên.
        int[][] directions =
        [
            [0, 1],
            [1, 0],
            [1, 1],
            [1, -1],
        ];

        foreach (var dir in directions)
        {
            var count = 1;
            count += CountDirection(board, row, col, dir[0], dir[1], symbol);
            count += CountDirection(board, row, col, -dir[0], -dir[1], symbol);

            // magic number 5 — không có const (LỖI #9, Style/Nit).
            if (count >= 5)
            {
                return true;
            }
        }

        return false;
    }

    private static int CountDirection(char[,] board, int row, int col, int dRow, int dCol, char symbol)
    {
        var size = board.GetLength(0);
        var count = 0;
        var r = row + dRow;
        var c = col + dCol;

        while (r >= 0 && r < size && c >= 0 && c < size && board[r, c] == symbol)
        {
            count++;
            r += dRow;
            c += dCol;
        }

        return count;
    }

    private static Task<string> GenerateJoinTokenAsync(string playerO)
    {
        // LỖI #8 (OWASP A02): băm token bằng MD5 — thuật toán yếu, đoán được.
        var raw = JoinSecret + playerO;
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(raw));
        return Task.FromResult(Convert.ToHexString(hash));
    }

    private static GameDto ToDto(Game game) =>
        new(game.Id, game.CurrentPlayer, game.Status, game.Winner, game.CreatedAt);
}
