# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["YarpGateway.API/YarpGateway.API.csproj", "YarpGateway.API/"]
# COPY ["SharedKernel/SharedKernel.csproj", "../../shared/SharedKernel/"]
# COPY ["BuildingBlocks/BuildingBlocks.csproj", "../../shared/BuildingBlocks/"]

RUN dotnet restore "YarpGateway.API/YarpGateway.API.csproj"

COPY . .
WORKDIR "/src/YarpGateway.API"
RUN dotnet publish "YarpGateway.API.csproj" -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "YarpGateway.API.dll"]
