FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FinancingPMS.csproj", "./"]
RUN dotnet restore "FinancingPMS.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "FinancingPMS.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FinancingPMS.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "FinancingPMS.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FinancingPMS.dll
