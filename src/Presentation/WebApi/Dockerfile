FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source

COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Repositories/Repositories.csproj", "src/Core/Repositories/"]
COPY ["src/Core/Services/Services.csproj", "src/Core/Services/"]
COPY ["src/Infrastructure/Persistence/Persistence.csproj", "src/Infrastructure/Persistence/"]
COPY ["src/Presentation/WebApi/WebApi.csproj", "src/Presentation/WebApi/"]

RUN dotnet restore "src/Presentation/WebApi/WebApi.csproj"

COPY . .

WORKDIR "/source/src/Presentation/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
