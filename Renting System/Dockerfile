FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Renting System/Renting System.csproj", "Renting System/"]
RUN dotnet restore "Renting System/Renting System.csproj"
COPY . .
WORKDIR "/src/Renting System"
RUN dotnet build "Renting System.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Renting System.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Renting System.dll"]