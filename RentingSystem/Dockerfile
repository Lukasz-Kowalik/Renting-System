#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RentingSystem/RentingSystem.csproj", "RentingSystem/"]
COPY ["DataLogic/DataLogic.csproj", "DataLogic/"]
COPY ["RentingSystemAPI/RentingSystemAPI.csproj", "RentingSystemAPI/"]
RUN dotnet restore "RentingSystem/RentingSystem.csproj"
COPY . .
WORKDIR "/src/RentingSystem"
RUN dotnet build "RentingSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentingSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentingSystem.dll"]