# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Minimal ASP.NET Core web app (.NET 10) exposing a single `GET /` endpoint that returns "Hello World!". The app is defined in `GitHubActionsExample/Program.cs` using the minimal API + minimal hosting model (top-level statements, no `Startup`/controllers). The solution uses the newer XML `.slnx` format rather than a classic `.sln`.

`Program.cs` ends with `public partial class Program { }` — this is intentional and required so `WebApplicationFactory<Program>` in the test project can boot the app. Don't remove it.

The point of this repo is the **CI pipeline**, not the app: `.github/workflows/ci.yml` runs build + tests + a SonarCloud quality gate on every push to `main` and every PR. The pipeline is designed to fail ("build geçmesin") when a test fails, when there are compiler warnings, or when the SonarCloud Quality Gate is not met (`sonar.qualitygate.wait=true`). See `README.md` for the one-time SonarCloud setup (project keys in the workflow's `REPLACE_*` placeholders + `SONAR_TOKEN` secret).

## Commands

Run all commands from the repository root.

- Build: `dotnet build`
- Test: `dotnet test` (single test: `dotnet test --filter "FullyQualifiedName~<TestName>"`)
- Test with OpenCover coverage (as CI does): `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings`
- Run (http, http://localhost:5048): `dotnet run --project GitHubActionsExample`
- Run https (https://localhost:7254): `dotnet run --project GitHubActionsExample --launch-profile https`

## Layout & conventions

- `GitHubActionsExample/` — the web app. `GitHubActionsExample.Tests/` — xUnit tests (`EndpointTests.cs` boots the app via `WebApplicationFactory<Program>` and asserts `GET /`). Both are registered in `GitHubActionsExample.slnx`.
- The app `.csproj` enables `EnableNETAnalyzers`, `AnalysisLevel=latest`, and `TreatWarningsAsErrors=true` — this is the local half of the quality gate, so any analyzer/compiler warning breaks `dotnet build`. Keep new code warning-clean.
- `Nullable` and `ImplicitUsings` are enabled across projects.
- `coverlet.runsettings` forces coverlet's `XPlat Code Coverage` collector to emit **OpenCover** XML (the format SonarCloud reads); don't switch it back to the default cobertura without updating `sonar.cs.opencover.reportsPaths` in the workflow.