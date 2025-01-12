FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/FIAP.PhaseOne.Api/*.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR /app/src/FIAP.PhaseOne.Api
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "FIAP.PhaseOne.Api.dll"]
