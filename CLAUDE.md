# Claude Code Project Guide

Team conventions sống ở file Copilot (single source of truth). Import lại ở đây để Claude Code đọc mỗi session:

@.github/copilot-instructions.md

## Path-scoped rules
Khi sửa file trong `**/Controllers/**/*.cs`, áp dụng thêm:

@.github/instructions/ontrollers.instructions.md

## Skills & agents
- Skills nằm ở `.claude/skills/` (đồng bộ nội dung với `.github/skills/`): `/review-pr`, `/review-security`, `/generate-tests`, `/review-and-fix`.
- Agent `senior-reviewer` ở `.claude/agents/senior-reviewer.md` — dùng cho review chặn (blocking) một diff/file.

## Demo project — CaroGame (Gomoku Web API)
Project dùng để **demo skill pack** trên code thật. Solution: `CaroGame.slnx` (KHÔNG phải `Lab4_2.sln`).

- `src/CaroGame.Api/` — ASP.NET Core Web API (controllers), .NET 8, C# 12.
  - `Models/Game.cs`, `Models/Dtos.cs` — entity + DTO records.
  - `Services/IGameStore.cs` + `GameStore.cs` — store in-memory (Singleton).
  - `Services/IGameService.cs` + `GameService.cs` — logic Gomoku, thắng khi 5 ô liên tiếp (`CheckWin`).
  - `Controllers/GamesController.cs` — 3 endpoint: tạo ván / đánh nước / xem trạng thái.
- `tests/CaroGame.Tests/` — xUnit + Moq + FluentAssertions.

> ⚠️ Code này **CỐ Ý cài sẵn ~10 lỗi** vi phạm convention (DateTime.Now, thiếu CancellationToken,
> `.Result`, controller dày nuốt lỗi, trả entity thô, JsonConvert, MD5 + secret hardcoded, magic
> number, naming sai) làm "mồi" cho skill review. **ĐỪNG tự ý sửa** trừ khi đang demo `/review-and-fix`.

Lệnh: `dotnet build`, `dotnet test`, `dotnet run --project src/CaroGame.Api`.
Demo skill trên nhánh `feature/caro-game`.
