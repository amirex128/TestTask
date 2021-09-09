FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WorkerService/WorkerService.csproj", "WorkerService/"]
COPY ["infrastructure.EntityFramework/infrastructure.EntityFramework.csproj", "infrastructure.EntityFramework/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["infrastructure.Rabbitmq/infrastructure.Rabbitmq.csproj", "infrastructure.Rabbitmq/"]
RUN dotnet restore "WorkerService/WorkerService.csproj"
COPY . .
WORKDIR "/src/WorkerService"
RUN dotnet build "WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerService.dll"]