#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["movie-api/movie-api.csproj", "movie-api/"]
RUN dotnet restore "movie-api/movie-api.csproj"
COPY . .
WORKDIR "/src/movie-api"
RUN dotnet build "movie-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "movie-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "movie-api.dll"]