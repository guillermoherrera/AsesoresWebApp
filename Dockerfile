#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AsesoresAPI.csproj", ""]
RUN dotnet restore "./AsesoresAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AsesoresAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsesoresAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsesoresAPI.dll"]

ENV TZ=America/Mexico_City
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
