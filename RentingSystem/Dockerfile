#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["RentingSystem/RentingSystem.csproj", "RentingSystem/"]
COPY ["RentingSystem.Services/RentingSystem.Services.csproj", "RentingSystem.Services/"]
COPY ["RentingSystem.ViewModels/RentingSystem.ViewModels.csproj", "RentingSystem.ViewModels/"]
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