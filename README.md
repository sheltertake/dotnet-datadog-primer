# dotnet-datadog-primer

## Prerequisites

- install wsl
- install ubuntu 20.04 LTS 
- install git for windows
- install vscode
  - extensions:
    - wsl
    - docker
    - markdown
- install vs 2022
- install chrome 
- install dotnet 6

## This repo

- create gitignore

```cmd
dotnet new gitignore
```

- create 2 APIs

```cmd
PS C:\temp\dotnet-datadog-primer> dotnet new webapi -n WeatherApi -o WeatherApi
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\temp\dotnet-datadog-primer\WeatherApi\WeatherApi.csproj...
  Determining projects to restore...
  Restored C:\temp\dotnet-datadog-primer\WeatherApi\WeatherApi.csproj (in 1.68 sec).
Restore succeeded.


PS C:\temp\dotnet-datadog-primer> dotnet new webapi -n ProxyApi -o ProxyApi
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\temp\dotnet-datadog-primer\ProxyApi\ProxyApi.csproj...
  Determining projects to restore...
  Restored C:\temp\dotnet-datadog-primer\ProxyApi\ProxyApi.csproj (in 293 ms).
Restore succeeded.
```