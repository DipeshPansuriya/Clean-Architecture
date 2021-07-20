#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Application_API/Application_API.csproj", "Application_API/"]
COPY ["Application_Database/Application_Database.csproj", "Application_Database/"]
COPY ["Application_Command/Application_Command.csproj", "Application_Command/"]
COPY ["Application_Core/Application_Core.csproj", "Application_Core/"]
COPY ["Application_Domain/Application_Domain.csproj", "Application_Domain/"]
COPY ["Application_Infrastructure/Application_Infrastructure.csproj", "Application_Infrastructure/"]
RUN dotnet restore "Application_API/Application_API.csproj"
COPY . .
WORKDIR "/src/Application_API"
RUN dotnet build "Application_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application_API.dll"]