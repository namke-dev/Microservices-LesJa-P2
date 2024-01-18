## Build Platform docker image

```powershell
docker build -t namke/platformservice -f Docker/Dockerfile .
```

Run docker container

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

push docker image

```powershell
docker push namke/platformservice
```

get to this end-point to test if docker container run fine:

http://localhost:8080/api/platforms

### Next section, we use kubectl to deploy `platformservice` image's container
