FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
ADD ../ .

RUN dotnet restore "src/FridgeRegistry.WebAPI/FridgeRegistry.WebAPI.csproj"
RUN dotnet build src/FridgeRegistry.WebAPI -c Release -o /app/build

FROM build AS publish
RUN dotnet publish src/FridgeRegistry.WebAPI -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FridgeRegistry.WebAPI.dll"]
