FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src

COPY ./src ./

WORKDIR /src/Revision.WebApi

RUN dotnet restore

RUN dotnet publish -c Release -o output

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS serve
WORKDIR /app
COPY --from=build /src/Revision.WebApi/output .

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "Revision.WebApi.dll" ]