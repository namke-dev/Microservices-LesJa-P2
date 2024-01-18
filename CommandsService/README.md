## Commands Service Note

### Defined protocol to communicate with PlatformsService

Create an endpoint to test if PlatformsService request hit the CommandsService, then write to console

The endpoint: http://localhost:5266/api/c/platforms

### build and deploy Commands Service

- Create Dockerfile at Docker\Dockerfile
- run cmd

```powershell
docker build -t namke/commandservice -f Docker/Dockerfile .
```
