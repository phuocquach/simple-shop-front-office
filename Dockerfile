FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/Mine.Ecommerce.MvcWeb"
RUN dotnet restore "./Mine.Ecommerce.Web.csproj"
RUN dotnet build "Mine.Ecommerce.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mine.Ecommerce.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mine.Ecommerce.Web.dll"]