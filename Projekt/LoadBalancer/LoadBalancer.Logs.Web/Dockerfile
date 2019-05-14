FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["LoadBalancer.Logs.Web/LoadBalancer.Logs.Web.csproj", "LoadBalancer.Logs.Web/"]
RUN dotnet restore "LoadBalancer.Logs.Web/LoadBalancer.Logs.Web.csproj"
COPY . .
WORKDIR "/src/LoadBalancer.Logs.Web"
RUN dotnet build "LoadBalancer.Logs.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LoadBalancer.Logs.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LoadBalancer.Logs.Web.dll"]