FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the csproj file and restore dependencies
COPY ["Cryptocop.Worker.Payments.csproj", "."]
RUN dotnet restore "Cryptocop.Worker.Payments.csproj"

# Copy all source files
COPY . .
WORKDIR "/src"

# Build the project
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptocop.Worker.Payments.dll"]
