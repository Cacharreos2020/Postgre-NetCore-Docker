#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AspNetCore_Docker/AspNetCore_Docker.csproj", "AspNetCore_Docker/"]
COPY ["PostgreDB_Connection/PostgreDB_Connection.csproj", "PostgreDB_Connection/"]
RUN dotnet restore "AspNetCore_Docker/AspNetCore_Docker.csproj"
COPY . .
WORKDIR "/src/AspNetCore_Docker"
RUN dotnet build "AspNetCore_Docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCore_Docker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCore_Docker.dll"]