namespace CaroGame.Api.Models;

/// <summary>Yêu cầu tạo ván mới.</summary>
public record CreateGameRequest(string PlayerX, string PlayerO);

/// <summary>Một nước đi: đặt <paramref name="Symbol"/> vào ô (Row, Col).</summary>
public record MoveRequest(int Row, int Col, char Symbol);

/// <summary>DTO trả về cho client — không lộ entity thô.</summary>
public record GameDto(
    Guid Id,
    char CurrentPlayer,
    GameStatus Status,
    char? Winner,
    DateTime CreatedAt);
