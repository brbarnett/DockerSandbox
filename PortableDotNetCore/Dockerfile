FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PortableDotNetCore/PortableDotNetCore.csproj", "PortableDotNetCore/"]
RUN dotnet restore "PortableDotNetCore/PortableDotNetCore.csproj"
COPY . .
WORKDIR "/src/PortableDotNetCore"
RUN dotnet build "PortableDotNetCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PortableDotNetCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PortableDotNetCore.dll"]