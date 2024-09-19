# Resolver test task

## Getting Started

- Install the latest [.NET Core SDK](https://dotnet.microsoft.com/en-us/download)
- Install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- Install [Git](https://git-scm.com/)

### Prepare to run

- Using Visual Studio 2022 open ResolverTechTask.sln file
- Navigate to _"Test > Configure Run Settings > Select Solution Wide runsettings File"_
- Select runsettings file from ".\AutomationFramework\Settings\app.runsettings"
- Restore NuGet packages and Build solution

## Solution Description

### Solution NuGet packages

- `Bogus` - Fake data generator
- `Serilog` - Logger
- `FluentAssertions` - provides mo flexible a readable assertions

### Solution Projects

- `AutomationFramework` - contains interfaces and classes describing UI elements and providing a set of methods to interact with them. Does not know anything about application and could be exported to NuGet package for reusing in other company test solutions
- `ApplicationFramework` - contains all project related information (Application Data, POMs, Components, etc.)
- `ResolverTechTask` - project with UI tests

## Test Execution

- `ResolverTechTask` project technically has 6 regular tests. Tests could be executed severally (one by one) or simultaneously. Project has `Assembly.cs` file where you can configure a number of parallel runs

## Repository Description

There is a configured `GitHub Actions` in `resolver-tech-task` repo that triggers all tests execution each time you pull changes to the `main` branch. Additionally repo has configured `GitHub Pages` that hosting last test run results represented by Allure Report. Last run results could be found by following [URL](https://shaddarion.github.io/resolver-tech-task/)
