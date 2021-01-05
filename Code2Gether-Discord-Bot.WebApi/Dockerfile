#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Code2Gether-Discord-Bot.WebApi/Code2Gether-Discord-Bot.WebApi.csproj", "Code2Gether-Discord-Bot.WebApi/"]
RUN dotnet restore "Code2Gether-Discord-Bot.WebApi/Code2Gether-Discord-Bot.WebApi.csproj"
COPY . .
WORKDIR "/src/Code2Gether-Discord-Bot.WebApi"
RUN dotnet build "Code2Gether-Discord-Bot.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Code2Gether-Discord-Bot.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Code2Gether-Discord-Bot.WebApi.dll"]