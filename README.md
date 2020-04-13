### EinTech

- **Add-Migration InitialCreate -Project EinTech.Api.DAL -StartupProject EinTech.Api.DAL**
- **Remove-Migration -Project EinTech.Api.DAL -StartUp EinTech.Api.DAL**
- **Update-Database**

###### Initialize tools:
Invoke-WebRequest https://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1
https://dotnet.microsoft.com/download/dotnet-core/3.1

###### Task Commands:
- **./build.ps1 -Target Run** - Will compile the files, runs integration tests, copies files to dist folder and runs web api.

- **./build.ps1 -Target BuildAndTest** - Will compile the files, runs integration tests.

- **./build.ps1 -Target Default** - Will compile the files, runs integration tests, copies files to dist folder.
