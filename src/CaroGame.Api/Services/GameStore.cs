using System.Collections.Concurrent;
using CaroGame.Api.Models;

namespace CaroGame.Api.Services;

/// <summary>In-memory store, thread-safe qua ConcurrentDictionary.</summary>
public class GameStore : IGameStore
{
    private readonly ConcurrentDictionary<Guid, Game> _games = new();

    public void Add(Game game) => _games[game.Id] = game;

    public Game? Find(Guid id) => _games.TryGetValue(id, out var game) ? game : null;
}
