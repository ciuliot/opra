FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./src/server/*.csproj .
RUN dotnet restore

# copy everything else and build
WORKDIR /app
COPY ./src/server .
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "opra.dll"]