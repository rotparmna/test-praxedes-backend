#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["test-praxedes-backend-api/test-praxedes-backend-api.csproj", "test-praxedes-backend-api/"]
RUN dotnet restore "test-praxedes-backend-api/test-praxedes-backend-api.csproj"
COPY . .
WORKDIR "/src/test-praxedes-backend-api"
RUN dotnet build "test-praxedes-backend-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "test-praxedes-backend-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test-praxedes-backend-api.dll"]
