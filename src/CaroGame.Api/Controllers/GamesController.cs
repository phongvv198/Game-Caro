using CaroGame.Api.Models;
using CaroGame.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CaroGame.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IGameService gameService, IGameStore store, ILogger<GamesController> logger)
    : ControllerBase
{
    private readonly IGameService _gameService = gameService;

    // LỖI #4/#6: controller cầm thẳng store (data layer) để trả entity thô về sau.
    private readonly IGameStore _store = store;
    private readonly ILogger<GamesController> _logger = logger;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameRequest request, CancellationToken ct)
    {
        var game = await _gameService.CreateGameAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
    }

    [HttpPost("{id:guid}/moves")]
    public async Task<IActionResult> Move(Guid id, [FromBody] MoveRequest request, CancellationToken ct)
    {
        try
        {
            // LỖI #4 (controllers.instructions): business logic nằm trong controller —
            // kiểm tra lượt & trạng thái đáng lẽ thuộc về GameService.
            var current = await _gameService.GetGameAsync(id);
            if (current is null)
            {
                return NotFound();
            }

            if (current.Status != GameStatus.InProgress)
            {
                return BadRequest("Ván đã kết thúc.");
            }

            if (request.Symbol != current.CurrentPlayer)
            {
                return BadRequest($"Chưa tới lượt '{request.Symbol}'.");
            }

            var result = await _gameService.MakeMoveAsync(id, request, ct);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // LỖI #5 (controllers.instructions): nuốt Exception chung, trả 200 OK —
            // đáng lẽ để middleware xử lý.
            _logger.LogInformation("Move failed: {Error}", JsonConvert.SerializeObject(ex.Message));
            return Ok();
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        // LỖI #6 (controllers.instructions): lấy thẳng từ store và trả entity Game thô,
        // không qua service, không map sang DTO.
        var game = _store.Find(id);
        if (game is null)
        {
            return NotFound();
        }

        // LỖI #7 (Forbidden): dùng Newtonsoft thay vì System.Text.Json.
        _logger.LogInformation("Get game: {Json}", JsonConvert.SerializeObject(game));
        return Ok(game);
    }
}
