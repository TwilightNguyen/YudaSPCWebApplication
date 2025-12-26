# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Technologies Used
1. ASP.NET Core 9.0
2. SQL Server 2022

# How to run this Project
1. Clone this source code form your repository.
2. Build solution to all Nuget Package in Visual Studio 2022 or later.
3. Set startup project to multiple projects include: YudaSPCWebApplication.BackendServer and YudaSPCWebApplication.WebPortal.
4. Run Update-Database to generate database.
5. Set Project profile to multiple projects include: YudaSPCWebApplication.BackendServer and YudaSPCWebApplication.WebPortal.

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

# References
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-9.0&WT.mc_id=dotnet-35129-website)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [IdentityServer4](https://duendesoftware.com/products/identityserver)

# Migration Database
- Add-Migration Initial -OutputDir Data/Migrations
- Update-Database