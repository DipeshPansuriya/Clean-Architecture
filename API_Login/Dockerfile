#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API_Login/API_Login.csproj", "API_Login/"]
COPY ["Application_Database/Application_Database.csproj", "Application_Database/"]
COPY ["Application_Core/Application_Core.csproj", "Application_Core/"]
COPY ["Application_Common/Application_Common.csproj", "Application_Common/"]
COPY ["Application_Infrastructure/Application_Infrastructure.csproj", "Application_Infrastructure/"]
RUN dotnet restore "API_Login/API_Login.csproj"
COPY . .
WORKDIR "/src/API_Login"
RUN dotnet build "API_Login.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Login.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Login.dll"]