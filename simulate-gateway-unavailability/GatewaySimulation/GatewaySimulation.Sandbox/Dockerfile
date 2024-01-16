FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GatewaySimulation.Sandbox/GatewaySimulation.Sandbox.csproj", "GatewaySimulation.Sandbox/"]
RUN dotnet restore "GatewaySimulation.Sandbox/GatewaySimulation.Sandbox.csproj"
COPY . .
WORKDIR "/src/GatewaySimulation.Sandbox"
RUN dotnet build "GatewaySimulation.Sandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatewaySimulation.Sandbox.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewaySimulation.Sandbox.dll"]
