# Security Assessment — Chinook.Web

This document summarizes security findings from a quick review of the Chinook.Web Razor Pages project and provides prioritized remediation guidance.

## Summary
Overall the project is a small sample app using SQLite and EF Core. The most significant risks are missing authentication/authorization on CRUD pages, presence of the sample SQLite database in the repo (contains sample user data), and several recommended hardening and configuration improvements.

## Affected files / locations
- Chinook.Web\Pages\Genres\Create.cshtml(.cshtml.cs), Edit, Delete, Index — public CRUD handlers
- Chinook.Web\Program.cs — app pipeline and middleware configuration
- Chinook.Web\appsettings.Development.json & Chinook.Web\Data\Chinook.sqlite — local config + DB file in repo
- Chinook.Web\.gitignore — currently ignores appsettings.Development.json but the DB file is committed

## Findings & Recommendations
1) Missing authentication and authorization
- Severity: High
- Issue: Genre create/edit/delete pages have no auth; anyone who can reach the site can modify data.
- Recommendation: Require authentication and fine-grained authorization for write actions. Add authentication middleware and decorate admin/modify pages with [Authorize] or apply a policy. Use role/claim checks for sensitive actions.

2) Potential CSRF risk on POST handlers
- Severity: Medium
- Issue: Forms use <form method="post"> without an explicit antiforgery token in the markup. Razor Pages usually validate antiforgery tokens on POST handlers by default, but the absence of an explicit token or global validation may leave gaps if custom middleware is added.
- Recommendation: Ensure antiforgery protection is enabled. Add the form tag helper or explicit @Html.AntiForgeryToken() (or use <form asp-antiforgery="true">). Verify that automatic antiforgery validation is active (AutoValidateAntiforgeryTokenAttribute or ValidateAntiForgeryToken applied to handlers).

3) Over-posting / mass-assignment risk
- Severity: Medium
- Issue: PageModels bind directly to entity types via [BindProperty] (Genre). This allows clients to post fields that should be immutable.
- Recommendation: Use dedicated view-models/DTOs for input binding and map only allowed fields to entity models. Remove BindProperty where unnecessary or use BindProperty(SupportsGet=false) with explicit properties.

4) Database file (Chinook.sqlite) committed to repository
- Severity: Medium
- Issue: The repository contains Chinook.Web\Data\Chinook.sqlite which may include sample PII and increases repo size; also risk of accidentally committing credentials in other setups.
- Recommendation: Remove the DB file from the repo history (git rm --cached), add Data\Chinook.sqlite to .gitignore, and provide instructions to obtain a local copy. Treat DB files as sensitive artifacts; do not commit production data.

5) Insecure defaults / configuration hardening
- Severity: Medium/Low
- Issues & recommendations:
  - AllowedHosts = "*" — set to specific hostnames in production.
  - Consider storing connection strings in User Secrets (development) or environment variables / secret store in production rather than appsettings files.
  - Ensure app.UseHsts() and app.UseHttpsRedirection() are used for production; they are present but confirm configuration (HSTS options) and that HTTPS-only endpoints are enforced.

6) Missing recommended security headers
- Severity: Low
- Issue: No middleware present to add security headers (CSP, X-Frame-Options, X-Content-Type-Options, Referrer-Policy, Permissions-Policy).
- Recommendation: Add middleware to set these headers (or use a well-tested NuGet package) and configure a Content-Security-Policy suited to the app.

7) Logging and error exposure
- Severity: Low
- Issue: In Development environment, developer exception page may show sensitive information. Ensure production configuration shows generic error pages.
- Recommendation: Verify environment detection and ensure detailed errors are disabled in production, and logs redact PII.

8) Dependency and patching hygiene
- Severity: Low
- Issue: Keep EF Core and other packages up-to-date; run dependency scanners.
- Recommendation: Regularly run `dotnet list package --outdated` and schedule dependency updates. Use automated dependency scanning (Dependabot, Snyk, etc.).

9) File permissions on SQLite DB
- Severity: Low
- Issue: SQLite DB file permissions determine who can read/write. If the app runs under a shared account, other users/processes might access DB.
- Recommendation: Restrict filesystem permissions to the application user. If using production DB, consider a server-hosted DB instead of a file.

## Actionable remediation checklist
- [ ] Add authentication (e.g., Identity, external providers) and require authorization for write pages.
- [ ] Use view-models for POST input and validate inputs with DataAnnotations/FluentValidation.
- [ ] Ensure antiforgery tokens are present and validated for all state-changing POST handlers.
- [ ] Remove Chinook.sqlite from repo and add it to .gitignore; provide instructions to download sample DB.
- [ ] Move secrets to environment variables or secret-management (User Secrets for local dev).
- [ ] Add security headers via middleware (CSP, X-Frame-Options, X-Content-Type-Options, Referrer-Policy).
- [ ] Harden AllowedHosts and production logging configuration.
- [ ] Restrict file permissions on DB file.
- [ ] Run static analysis and dependency scanning (OWASP ZAP for web flows, dotnet security analyzers, dependency scanners).

## Reporting a security issue
If you discover a security vulnerability in this project, please open an issue labeled "security" or contact the repository owner directly. For responsible disclosure, include reproduction steps, affected versions, and suggested mitigations.

---

This is a brief security review and not a comprehensive penetration test. For production readiness, perform a full threat model, code audit, and dynamic security testing.
