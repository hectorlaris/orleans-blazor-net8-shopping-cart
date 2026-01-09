# .NET 8.0 Upgrade Report

## Project target framework modifications

| Project name                                   | Old Target Framework    | New Target Framework         | Commits                   |
|:-----------------------------------------------|:-----------------------:|:----------------------------:|---------------------------|
| Orleans.ShoppingCart.Abstractions.csproj      |   net6.0                | net8.0                       | 59c7d9e8                  |
| Orleans.ShoppingCart.Grains.csproj            |   net6.0                | net8.0                       | 5c6fe8c3                  |
| Orleans.ShoppingCart.Silo.csproj              |   net6.0                | net8.0                       | 7b326340, f017bdbe        |

## NuGet Packages

| Package Name                                         | Old Version | New Version | Commit Id                                 |
|:----------------------------------------------------|:-----------:|:-----------:|-------------------------------------------|
| Microsoft.ApplicationInsights.AspNetCore            |   2.20.0    |  2.23.0     | f017bdbe                                  |
| Microsoft.AspNetCore.Authentication.JwtBearer       |   6.0.7     |  8.0.22     | f017bdbe                                  |
| Microsoft.AspNetCore.Authentication.OpenIdConnect   |   6.0.7     |  8.0.22     | f017bdbe                                  |
| Microsoft.Identity.Web                              |   1.25.1    |  4.3.0      | f017bdbe                                  |
| Microsoft.Identity.Web.UI                           |   1.25.1    |  4.3.0      | f017bdbe                                  |
| Microsoft.VisualStudio.Azure.Containers.Tools.Targets |  1.16.1  | (Removed)   | f017bdbe                                  |

## All commits

| Commit ID              | Description                                |
|:-----------------------|:-------------------------------------------|
| 22551a12               | Commit upgrade plan                        |
| 015d44c1               | Store final changes for step 'Ensure SDK version compatibility with global.json files' |
| 59c7d9e8               | Upgrade target framework to .NET 8.0      |
| 5c6fe8c3               | Update target framework to .NET 8.0       |
| 7b326340               | Update target framework from .NET 6.0 to 8.0 |
| f017bdbe               | Update Orleans.ShoppingCart.Silo.csproj packages |

## Project feature upgrades

### Orleans.ShoppingCart.Abstractions.csproj

The project has been successfully upgraded to .NET 8.0:
- Target framework changed from net6.0 to net8.0
- Maintained existing nullable reference types and implicit usings configuration

### Orleans.ShoppingCart.Grains.csproj

The project has been successfully upgraded to .NET 8.0:
- Target framework changed from net6.0 to net8.0
- Retained all Orleans grain functionality with the latest framework

### Orleans.ShoppingCart.Silo.csproj

The project has been successfully upgraded to .NET 8.0:
- Target framework changed from net6.0 to net8.0
- Updated authentication packages to versions compatible with .NET 8.0
- Upgraded Microsoft Identity Web packages to the latest supported versions
- Updated Application Insights package to remove deprecated version
- Removed incompatible Azure Container Tools package

## Next steps

- Your Orleans shopping cart application is now running on .NET 8.0 (LTS)
- Consider testing the application thoroughly to ensure all functionality works as expected
- Review any deprecated API usage warnings that may appear during compilation
- Consider leveraging new .NET 8.0 performance improvements and features in future development