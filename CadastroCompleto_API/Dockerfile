ARG VERSION=6.0-alpine3.14

    #Runtime stage
    FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
    EXPOSE 8080

    ENV DOTNET_RUNNING_IN_CONTAINER=true \
      ASPNETCORE_URLS=http://+:8080
    
    RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib
    
    ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false