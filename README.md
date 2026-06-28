# mate-dogfood-dotnet

A small, intentionally outdated **.NET** app used as a dogfooding fixture for
[MATE (Keep Up)](https://github.com/elefores). It gives keepup a realistic
target to run against in CI.

## The app

A console app (`src/Program.cs`) that validates a `Customer` with FluentValidation,
serialises it with Newtonsoft.Json, and logs with Serilog.

## Intentionally outdated dependencies

`MateDogfood.csproj` pins everything a few versions behind current:

| Dependency | Pinned | Why it matters |
|------------|--------|----------------|
| `FluentValidation` | `9.5.4` | **Breaking gap.** `CascadeMode.StopOnFirstFailure` was removed in v11 (replaced by `CascadeMode.Stop`). `src/CustomerValidator.cs` uses the old name, so the upgrade must touch code. |
| `Newtonsoft.Json` | `12.0.3` | Behind the current 13.x line. |
| `Serilog` | `2.12.0` | Behind the current 3.x/4.x line. |
| `Serilog.Sinks.Console` | `4.1.0` | Constrained in `kup.toml`. |

This makes keepup exercise the **AI code-upgrade path**, not just a version bump.

## keepup config

See [`kup.toml`](kup.toml): it includes C# `.csproj` files, shows a
version-constrained ignore rule, sets grouping thresholds, and selects the
Claude Code agent.

## CI

[`.github/workflows/keepup.yml`](.github/workflows/keepup.yml) runs keepup on a
weekly schedule (and on demand). Set these repository secrets for it to run:

- `KUP_LICENSE_KEY` — signed keepup license key
- `CLAUDE_CODE_OAUTH_TOKEN` — Claude Code agent token
- `ACCESS_TOKEN` — GitHub token used to push branches and open PRs
