# .NET 8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8.0 upgrade.
3. Upgrade Abstractions\Orleans.ShoppingCart.Abstractions.csproj to .NET 8.0
4. Upgrade Grains\Orleans.ShoppingCart.Grains.csproj to .NET 8.0
5. Upgrade Silo\Orleans.ShoppingCart.Silo.csproj to .NET 8.0

## Settings

This section contains settings and data used by execution steps.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                         | Current Version | New Version | Description                                      |
|:----------------------------------------------------|:---------------:|:-----------:|:------------------------------------------------|
| Microsoft.ApplicationInsights.AspNetCore            |     2.20.0      |   2.23.0    | Deprecated version upgrade                       |
| Microsoft.AspNetCore.Authentication.JwtBearer       |     6.0.7       |   8.0.22    | Recommended for .NET 8.0                        |
| Microsoft.AspNetCore.Authentication.OpenIdConnect   |     6.0.7       |   8.0.22    | Recommended for .NET 8.0                        |
| Microsoft.Identity.Web                              |     1.25.1      |   4.3.0     | Deprecated version upgrade                       |
| Microsoft.Identity.Web.UI                           |     1.25.1      |   4.3.0     | Deprecated version upgrade                       |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets |  1.16.1     |             | Remove package (no supported version found)      |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### Abstractions\Orleans.ShoppingCart.Abstractions.csproj modifications

Project properties changes:
- Target framework should be changed from `net6.0` to `net8.0`

#### Grains\Orleans.ShoppingCart.Grains.csproj modifications

Project properties changes:
- Target framework should be changed from `net6.0` to `net8.0`

#### Silo\Orleans.ShoppingCart.Silo.csproj modifications

Project properties changes:
- Target framework should be changed from `net6.0` to `net8.0`

NuGet packages changes:
- Microsoft.ApplicationInsights.AspNetCore should be updated from `2.20.0` to `2.23.0` (*deprecated version upgrade*)
- Microsoft.AspNetCore.Authentication.JwtBearer should be updated from `6.0.7` to `8.0.22` (*recommended for .NET 8.0*)
- Microsoft.AspNetCore.Authentication.OpenIdConnect should be updated from `6.0.7` to `8.0.22` (*recommended for .NET 8.0*)
- Microsoft.Identity.Web should be updated from `1.25.1` to `4.3.0` (*deprecated version upgrade*)
- Microsoft.Identity.Web.UI should be updated from `1.25.1` to `4.3.0` (*deprecated version upgrade*)
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets should be removed (*no supported version found*)