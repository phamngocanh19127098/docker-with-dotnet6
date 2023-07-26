

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


COPY ["./BusinessObject/BusinessObject.csproj", "/src/BusinessObject/"]
COPY ["./DataAccess/DataAccess.csproj", "/src/DataAccess/"]
COPY ["./TourBookingApi/BusTourApi.csproj", "/src/TourBookingApi/"]



RUN dotnet restore "/src/TourBookingApi/BusTourApi.csproj"

COPY . .

WORKDIR "/src/TourBookingApi/"
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime


WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "BusTourApi.dll" ]
