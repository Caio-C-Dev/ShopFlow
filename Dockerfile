FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ShopFlow.API/ShopFlow.API.csproj", "ShopFlow.API/"]
COPY ["ShopFlow.Application/ShopFlow.Application.csproj", "ShopFlow.Application/"]
COPY ["ShopFlow.Domain/ShopFlow.Domain.csproj", "ShopFlow.Domain/"]
COPY ["ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj", "ShopFlow.Infrastructure/"]

RUN dotnet restore "ShopFlow.API/ShopFlow.API.csproj"

COPY . .

WORKDIR "/src/ShopFlow.API"
RUN dotnet build "ShopFlow.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopFlow.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopFlow.API.dll"]