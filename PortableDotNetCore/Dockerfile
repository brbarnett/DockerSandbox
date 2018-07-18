# Start by pulling a slimmed down version of the .NET Core 2.1 runtime base image. 
# Note that this specific tag has the ASP.NET Core bits as well, which
# is a bit heavier than if we were just pulling 2.1 (which you could use
# for console apps, for example)
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

# Change working directory _inside the image_ to /app
WORKDIR /app

# Expose port 80. This has no effect other than signifying to the team
# that you intend to listen on port 80
EXPOSE 80

# Now download the full-featured .NET Core SDK base image. This has all the
# build bits that the slim base image above does not have
FROM microsoft/dotnet:2.1-sdk AS build

# Change working directory inside the image to /app
WORKDIR /src

# Keep in mind our build context is at the root of the solution for project reference
# dependency purposes. All source paths need to be fully-qualified from that root directory.
# Here we're copying the *.csproj file from the main web app into the /app/PortableDotNetCore/
# directory first so that we can run `dotnet restore` to download project dependencies
COPY ["PortableDotNetCore/PortableDotNetCore.csproj", "PortableDotNetCore/"]

# Run `dotnet restore` on the main web project file
RUN dotnet restore "PortableDotNetCore/PortableDotNetCore.csproj"

# Copy the rest of the solution files into the /src directory of the image. We do this because 
# we'll need to reference other projects that are referenced by our main web project. 
# Alternatively, you could slim this down by copying only the relevant project files
COPY . .

# Change working directory to our main project
WORKDIR "/src/PortableDotNetCore"

# Build the project in release mode
RUN dotnet build "PortableDotNetCore.csproj" -c Release -o /app

# Publish the main project
FROM build AS publish
RUN dotnet publish "PortableDotNetCore.csproj" -c Release -o /app

# Copy build artifacts from the `publish` to `final` image. `final` is an alias of `base`, which
# is the slimmed down microsoft/dotnet:2.1-aspnetcore-runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# Run the following command in the container on startup. This will start the Kestrel server
# and serve the application in the foreground
ENTRYPOINT ["dotnet", "PortableDotNetCore.dll"]