using CaroGame.Api.Models;

namespace CaroGame.Api.Services;

/// <summary>Kho lưu ván cờ. Singleton — giữ state dùng chung cho mọi request.</summary>
public interface IGameStore
{
    void Add(Game game);
    Game? Find(Guid id);
}
