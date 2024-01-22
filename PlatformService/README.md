## PlatformService Note

### Build Platform docker image

1. Create Dockerfile at Docker/Dockerfile
2. Run command line

```powershell
docker build -t namke/platformservice -f Docker/Dockerfile .
```

### Run local docker container

```powershell
docker run -p 8080:80 -d namke/platformservice
```

View list running container

```powershell
docker ps
```

Stop and start a container

```powershell
docker stop <containerId>
docker start <containerId>
```

### Push docker image to Docker Hub

```powershell
docker push namke/platformservice
```

### Use end-point to test if docker container run fine:

http://localhost:8080/api/platforms

### Use kubectl to deploy `platformservice` image's container, file setting and note is at K8S folder

### Use HttpClient Service to send request (PlatfromReadDto obj) to Commands Serivce in case create new platform object

Method defined at SyncDataServices\Http\HttpCommandDataClient.cs

### add initial migration

```powershell
dotnet ef migrations add initialmigration
```

### Add package RabitMQ Client

```powershell
dotnet add package RabbitMQ.Client
```

### Test gRPC localy on https

```powershell
dotnet run --launch-profile https
```
