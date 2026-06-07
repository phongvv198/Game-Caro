---
applyTo: "**/Controllers/**/*.cs"
description: Conventions cho ASP.NET Core controllers
---

# Controller conventions

- Controller mỏng: chỉ orchestrate, KHÔNG chứa business logic.
- KHÔNG gọi thẳng DbContext trong controller — đi qua service.
- Action async phải nhận và truyền `CancellationToken`.
- Return type: `IActionResult` hoặc `Results<...>` (typed results), không trả entity thô.
- HTTP semantics đúng: 200 OK, 201 Created (kèm Location), 204 NoContent, 400/404/409 hợp lý.
- Validate input ở edge (model binding + ProblemDetails cho lỗi).
- KHÔNG bắt `Exception` chung rồi nuốt — để middleware xử lý.