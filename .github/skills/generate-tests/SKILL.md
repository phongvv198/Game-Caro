---
name: generate-tests
description: |
  Use this skill when the user asks to generate or update unit tests
  for a service, validator, or controller.
---

# Generate Tests Skill

## When to use
- The user names a file/service and asks for tests
- The user asks to raise coverage above 80%

## Workflow
1. Read the target file and its public methods
2. Read team naming/structure rules in copilot-instructions.md
3. Generate AAA tests: 1 happy + 2 edge + 1 exception per method
4. Use xUnit + Moq + FluentAssertions

## Convention
- Test name: MethodName_Scenario_ExpectedResult
- Arrange / Act / Assert rõ ràng
- Mock dependency qua Moq; assert bằng FluentAssertions