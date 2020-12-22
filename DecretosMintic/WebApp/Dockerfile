FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY ./Assets /app/Assets

COPY --from=build-env /app/out .
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
RUN chmod -R 755 Rotativa/

ENTRYPOINT ["dotnet", "WebApp.dll"]
