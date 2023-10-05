FROM microsoft/aspnetcore
ARG source
WORKDIR /app
EXPOSE 80
# COPY ${source:-obj/Docker/publish} .
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DB.Routing.Api.dll"]

