#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotificationApp2/NotificationApp2.csproj", "NotificationApp2/"]
RUN dotnet restore "NotificationApp2/NotificationApp2.csproj"
COPY . .
WORKDIR "/src/NotificationApp2"
RUN dotnet build "NotificationApp2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationApp2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApp2.dll"]