
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["ContentService.API/ContentService.API.csproj", "ContentService.API/"]
COPY ["ContentService.Application/ContentService.Application.csproj", "ContentService.Application/"]
COPY ["ContentService.Domain/ContentService.Domain.csproj", "ContentService.Domain/"]
COPY ["ContentService.Infrastructure/ContentService.Infrastructure.csproj", "ContentService.Infrastructure/"]



RUN dotnet restore "ContentService.API/ContentService.API.csproj"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .
WORKDIR "/src/ContentService.API"
RUN dotnet publish "ContentService.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ContentService.API.dll"]
