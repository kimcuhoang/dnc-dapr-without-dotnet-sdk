FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11 AS base
WORKDIR /app
# https://github.com/dotnet/efcore/issues/18025
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

#######################################
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11 AS build

WORKDIR /src

COPY ["source/Example.Dapr.Publisher/Example.Dapr.Publisher.csproj", "Example.Dapr.Publisher/Example.Dapr.Publisher.csproj"]

RUN dotnet restore "Example.Dapr.Publisher/Example.Dapr.Publisher.csproj"

COPY ./source/Example.Dapr.Shared ./Example.Dapr.Shared
COPY ./source/Example.Dapr.Publisher ./Example.Dapr.Publisher

WORKDIR "/src/Example.Dapr.Publisher"

RUN dotnet build "Example.Dapr.Publisher.csproj" --nologo --no-restore -c Release -o /app/build

#######################################
FROM build AS publish
RUN dotnet publish "Example.Dapr.Publisher.csproj" --nologo --no-restore -c Release -o /app/publish

#######################################
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Dapr.Publisher.dll"]