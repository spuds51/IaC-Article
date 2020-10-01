ARG SDK=3.1
ARG RUNTIME=3.1

FROM mcr.microsoft.com/dotnet/core/sdk:$SDK AS build
WORKDIR /app

# Copy local files into build image
COPY . ./

# Build binaries to /out directory
WORKDIR /app
RUN dotnet publish Hungrr.Orders.Api.csproj -c Release -o out

# Build runtime image  
FROM mcr.microsoft.com/dotnet/core/aspnet:$RUNTIME AS runtime
WORKDIR /app
# Copy from the build image above into the runtime image
COPY --from=build /app/out/ .

ENTRYPOINT ["dotnet", "Hungrr.Orders.Api.dll"]