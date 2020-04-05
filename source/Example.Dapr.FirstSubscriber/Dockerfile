FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11 AS base
WORKDIR /app
# https://github.com/dotnet/efcore/issues/18025
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

#######################################
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201-alpine3.11 AS build

WORKDIR /src

COPY ["source/Example.Dapr.FirstSubscriber/Example.Dapr.FirstSubscriber.csproj", "Example.Dapr.FirstSubscriber/Example.Dapr.FirstSubscriber.csproj"]

RUN dotnet restore "Example.Dapr.FirstSubscriber/Example.Dapr.FirstSubscriber.csproj"

COPY ./source/Example.Dapr.Shared ./Example.Dapr.Shared
COPY ./source/Example.Dapr.FirstSubscriber ./Example.Dapr.FirstSubscriber

WORKDIR "/src/Example.Dapr.FirstSubscriber"

RUN dotnet build "Example.Dapr.FirstSubscriber.csproj" --nologo --no-restore -c Release -o /app/build

#######################################
FROM build AS publish
RUN dotnet publish "Example.Dapr.FirstSubscriber.csproj" --nologo --no-restore -c Release -o /app/publish

#######################################
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Dapr.FirstSubscriber.dll"]