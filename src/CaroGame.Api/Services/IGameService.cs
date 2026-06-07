using CaroGame.Api.Models;

namespace CaroGame.Api.Services;

public interface IGameService
{
    Task<GameDto> CreateGameAsync(CreateGameRequest request, CancellationToken ct);

    Task<GameDto> MakeMoveAsync(Guid gameId, MoveRequest move, CancellationToken ct);

    // LỖI #2 (async rule): public async method thiếu CancellationToken.
    Task<GameDto?> GetGameAsync(Guid gameId);
}
