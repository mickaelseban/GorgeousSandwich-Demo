FROM mcr.microsoft.com/dotnet/core/sdk AS build-env
WORKDIR /orders-service

COPY . ./
RUN dotnet publish API -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /apporders-service
COPY --from=build-env /orders-service/API/out .

ENTRYPOINT ["dotnet", "API.dll"]