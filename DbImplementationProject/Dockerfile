FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DbImplementationProject/DbImplementationProject.csproj", "DbImplementationProject/"]
RUN dotnet restore "DbImplementationProject/DbImplementationProject.csproj"
COPY . .
WORKDIR "/src/DbImplementationProject"
RUN dotnet build "DbImplementationProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DbImplementationProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DbImplementationProject.dll"]
