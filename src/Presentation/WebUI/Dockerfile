FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ARG BUILD_MODE=Release
WORKDIR /source

COPY ["src/Core/Localization/Localization.csproj","src/Core/Localization/"]
COPY ["src/Presentation/WebUI/WebUI.csproj","src/Presentation/WebUI/"]

RUN dotnet restore "src/Presentation/WebUI/WebUI.csproj"

COPY . .

WORKDIR "/source/src/Presentation/WebUI/"
RUN dotnet build "WebUI.csproj" -c $BUILD_MODE -o /app/build

FROM build as publish
RUN dotnet publish "WebUI.csproj" -c $BUILD_MODE -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebUI.dll"]