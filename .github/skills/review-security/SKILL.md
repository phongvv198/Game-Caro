---
name: review-security
description: |
  Use this skill when the user asks for an OWASP Top 10 security audit
  of a file, a diff, or a folder, especially auth / SQL / HTML rendering changes.
---

# Review Security Skill

## When to use
- User names a file (`#file:UserService.cs`) and asks for a security audit
- Before merging a PR touching credential-handling, SQL, or HTML rendering

## Workflow
1. Read the target file (or `${selection}`)
2. Walk every OWASP category below
3. For each finding: severity, file:line, description, fix snippet, OWASP link

## OWASP categories
- A01 Broken Access Control: missing [Authorize], IDOR, mass assignment
- A02 Cryptographic Failures: MD5/SHA1/DES, plaintext password, weak random
- A03 Injection: FromSqlRaw/ExecuteSqlRaw không parameterized, stored/reflected XSS
- A07 Auth Failures: hardcoded secret, weak password policy, predictable token
- A08 Data Integrity: insecure deserialization (BinaryFormatter)
- A09 Logging Failures: log password/PII
- A10 SSRF: HttpClient tới URL từ user input

## Output format
Sort theo severity Critical / High / Medium / Low. Mỗi finding kèm fix snippet + OWASP link.
Verify: chạy lại trên file đã fix, target 0 Critical / 0 High.