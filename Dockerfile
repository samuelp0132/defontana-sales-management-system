# .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY SalesManagementSystem/SalesManagementSystem.Application/SalesManagementSystem.Application.csproj ./SalesManagementSystem.Application/
COPY SalesManagementSystem/SalesManagementSystem.Domain/SalesManagementSystem.Domain.csproj ./SalesManagementSystem.Domain/
COPY SalesManagementSystem/SalesManagementSystem.Infrastructure/SalesManagementSystem.Infrastructure.csproj ./SalesManagementSystem.Infrastructure/

# .NET Core Restore
RUN dotnet restore ./SalesManagementSystem.Application/

# Copy All Files
COPY . .

# .NET Core Build and Publish
RUN dotnet publish SalesManagementSystem/SalesManagementSystem.Application -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

ENTRYPOINT ["dotnet", "SalesManagementSystem.Application.dll"]