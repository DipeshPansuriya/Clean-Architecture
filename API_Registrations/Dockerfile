#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API_Registrations/API_Registrations.csproj", "API_Registrations/"]
COPY ["Application_Database/Application_Database.csproj", "Application_Database/"]
COPY ["Application_Core/Application_Core.csproj", "Application_Core/"]
COPY ["Application_Services/Application_Services.csproj", "Application_Services/"]
COPY ["Registrations_Command/Registrations_Command.csproj", "Registrations_Command/"]
COPY ["Application_Infrastructure/Application_Infrastructure.csproj", "Application_Infrastructure/"]
RUN dotnet restore "API_Registrations/API_Registrations.csproj"
COPY . .
WORKDIR "/src/API_Registrations"
RUN dotnet build "API_Registrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_Registrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_Registrations.dll"]