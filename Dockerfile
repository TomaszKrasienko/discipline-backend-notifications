FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY src/discipline.core/discipline.core.csproj ./discipline.core/
COPY src/discipline.api/discipline.api.csproj ./discipline.api/
RUN dotnet restore ./discipline.api/discipline.api.csproj -s https://api.nuget.org/v3/index.json  --verbosity detailed
COPY . ./
RUN dotnet publish ./discipline-backend-notifications.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "discipline.api.dll"]

ENV ASPNETCORE_ENVIRONMENT="docker"
ENV TZ="Europe/Warsaw"
EXPOSE 80
EXPOSE 443