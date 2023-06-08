FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalesManagementSystem.Application/SalesManagementSystem.Application.csproj", "SalesManagementSystem.Application/"]
RUN dotnet restore "SalesManagementSystem.Application/SalesManagementSystem.Application.csproj"
COPY . .
WORKDIR "/src/SalesManagementSystem.Application"
RUN dotnet build "SalesManagementSystem.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesManagementSystem.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesManagementSystem.Application.dll"]
