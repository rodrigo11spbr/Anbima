FROM  mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build
ARG buildconfig="Release"
WORKDIR /src
COPY ["AnbimaConsumer.API/AnbimaConsumer.API.csproj","AnbimaConsumer.API/"]
COPY ["AnbimaConsumer.Application/AnbimaConsumer.Application.csproj","AnbimaConsumer.Application/"]
COPY ["AnbimaConsumer.Domain/AnbimaConsumer.Domain.csproj","AnbimaConsumer.Domain"]
RUN dotnet restore "AnbimaConsumer.API/AnbimaConsumer.API.csproj"
COPY . .
WORKDIR "/src/AnbimaConsumer.API"
RUN dotnet publish "AnbimaConsumer.API.csproj" -c $buildconfig -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine as final

EXPOSE 1433
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:5000"
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AnbimaConsumer.API.dll"]