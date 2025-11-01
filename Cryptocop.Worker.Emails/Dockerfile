FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Cryptocop.Worker.Emails/Cryptocop.Worker.Emails.csproj", "Cryptocop.Worker.Emails/"]
COPY ["Cryptocop.Models/Cryptocop.Models.csproj", "Cryptocop.Models/"]
RUN dotnet restore "Cryptocop.Worker.Emails/Cryptocop.Worker.Emails.csproj"

COPY . .
WORKDIR "/src"

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptocop.Worker.Emails.dll"]
