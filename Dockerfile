FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as build
WORKDIR /app

RUN apk update && apk add nodejs nodejs-npm

COPY *.csproj ./
RUN dotnet restore

COPY ./ClientApp/package*.json ./ClientApp/

RUN cd ClientApp \
    && npm install

COPY . .
RUN dotnet publish -c Release -o build

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app

COPY --from=build /app/build .

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "./WhatIsNext.dll" ]
