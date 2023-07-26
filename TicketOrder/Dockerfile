

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


# copy all the layers' csproj files into respective folders
COPY ["./BusinessObject/BusinessObject.csproj", "/src/BusinessObject/"]
COPY ["./DataAccess/DataAccess.csproj", "/src/DataAccess/"]
COPY ["./TourBookingApi/TicketOrderApi.csproj", "/src/TourBookingApi/"]



RUN dotnet restore "/src/TourBookingApi/TicketOrderApi.csproj"

COPY . .

WORKDIR "/src/TourBookingApi/"
RUN dotnet build -c Release -o /app/build


# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime


WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "TicketOrderApi.dll" ]