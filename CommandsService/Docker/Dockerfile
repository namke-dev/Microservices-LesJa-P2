# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Assuming the Dockerfile is in the "Docker" subdirectory
# Copy the project file into the image
COPY ../CommandsService.csproj ./

# Restore the dependencies
RUN dotnet restore

# Copy the remaining source code
COPY .. ./

# Build the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CommandsService.dll"]

