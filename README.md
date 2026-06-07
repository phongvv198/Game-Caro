# Game Caro (Gomoku) — Web API

[![CI](https://github.com/phongvv198/Game-Caro/actions/workflows/ci.yml/badge.svg)](https://github.com/phongvv198/Game-Caro/actions/workflows/ci.yml)

ASP.NET Core Web API (.NET 8, C# 12) demo cho skill pack review. Thắng khi có 5 ô liên tiếp.

## Cấu trúc
- `src/CaroGame.Api/` — Web API (Controllers, Models, Services).
- `tests/CaroGame.Tests/` — xUnit + Moq + FluentAssertions.

## Lệnh
```bash
dotnet build CaroGame.slnx
dotnet test  CaroGame.slnx
dotnet run --project src/CaroGame.Api
```

## Quy ước nhánh
- `main` được bảo vệ: PR + 1 approval + resolve hết comment + check `build-test` pass mới merge được.
- Phát triển trên nhánh `feature/*`.
