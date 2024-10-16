# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["MediConnectBackend.csproj", "./"]

# Restore any dependencies
RUN dotnet restore

# Copy the rest of the application files
COPY . .

# Build the app
RUN dotnet build "MediConnectBackend.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "MediConnectBackend.csproj" -c Release -o /app/publish

# Use the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediConnectBackend.dll"]
