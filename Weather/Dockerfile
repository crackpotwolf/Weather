FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Weather/Weather.csproj", "Weather/"]
RUN dotnet restore "Weather/Weather.csproj"
COPY ./Weather ./Weather

WORKDIR "/src/Weather"
RUN dotnet build "Weather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

WORKDIR "/app/wwwroot"
RUN npm install

WORKDIR /app
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Weather.dll