---
name: review-and-fix
description: |
  Use this skill when the user pastes a PR or branch name and asks for an
  end-to-end review + auto-fix of blocking findings.
argument-hint: branch=<branch-name>
---

# Review and Fix

## Phases
1. Review  -> persona senior-reviewer -> REVIEW.md, tag [BLOCK]/[SUGGEST]/[NIT]
2. Fix     -> persona fixer  -> loop từng [BLOCK], commit "fix: ... [ai]"
3. Verify  -> persona tester -> dotnet test; test hỏng thì regenerate qua generate-tests

## Retry budget
| Phase  | Attempts | Self-heal | Escalation |
|--------|---------:|-----------|------------|
| Review | 1 | none | surface to user |
| Fix    | 3 per [BLOCK] | đọc lại build error, đơn giản hoá fix | mark [DEFER], đi tiếp |
| Verify | 2 | siết assertion, regenerate test | test còn đỏ -> escalate |

## Stop conditions
- Mọi [BLOCK] đã fix hoặc [DEFER], tests xanh
- Hoặc chạm trần 90 phút wall-clock -> dừng, surface state
