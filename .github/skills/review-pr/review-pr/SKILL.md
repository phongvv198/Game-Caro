---
name: review-pr
description: |
  Use this skill when the user asks for a checklist-driven review of a diff
  or a pull request. Targets the team's blocking categories: security,
  performance, correctness, style.
---

# Review PR Skill

## When to use
- The user pastes a diff, a branch name, or a PR URL and asks for review
- The CI workflow invokes this skill on every PR

## Workflow
1. Read the diff (`git diff origin/main..HEAD` or `${selection}`)
2. Read `copilot-instructions.md` for team conventions
3. Walk the four-category checklist below
4. Group findings by severity, write the summary to PR comment / `REVIEW.md`

## Checklist
### Security
- SQL injection / parameterised query
- Hardcoded secret / connection string
- Missing `[Authorize]`, input validation, output encoding
### Performance
- N+1 query, missing AsNoTracking, hot-path allocations
- Pagination on list endpoints
### Correctness
- Null checks, race conditions (Singleton + mutable state)
- DateTime UTC, CancellationToken propagation
- Sync-over-async (`.Result`, `.Wait()`)
### Style
- Magic number with no const
- Naming convention (PascalCase service, _camelCase field)

## Output format
Group theo severity (Block / Suggest / Nit). Mỗi issue: File:line, vấn đề, fix snippet. Kết bằng top-3 priorities.