## K8S Note

This folder contain created kubectl deploy and service file

### Deploy service (included node port service)

```powershell
kubectl apply -f platforms-deploy.yaml
kubectl apply -f platforms-nodeport-service.yaml
kubectl apply -f commands-deploy.yaml
```

### Check node service port

```powershell
kubectl get service
```

CLI return

```powershell
NAME                    TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)        AGE
kubernetes              ClusterIP   10.96.0.1       <none>        443/TCP        18h
platformnpservice-srv   NodePort    10.103.218.33   <none>        80:32558/TCP   18s
```

### Access the node port service by url:

http://localhost:32558/api/platforms

---

### delete deploy and service

```powershell
kubectl delete deployment platforms-deploy
kubectl delete service platformnpservice-srv
```

### restart deployment to apply change:

```powershell
kubectl rollout restart deployment platforms-deploy
kubectl rollout restart deployment commands-deploy

```

### Dowload and apply Nginx

```powershell
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/aws/deploy.yaml
kubectl apply -f ingress-srv.yaml
```

### Config temp url ro test Nginx

I try to add url ip to my local (127.0.0.1) for "acme.com" in
"C:\Windows\System32\drivers\etc\hosts"

Test APIGateway at this endpoint:
http://acme.com/api/platforms

### Create persistent volume claim for storing data

```powershell
kubectl.exe apply -f .\local-persistent-volume-storage.yaml
```

### Create serect password for mssql in kubectl

```powershell
kubectl create secret generic mssql --from-literal=SA_PASSWORD="Abc@123456"
```

### Deployment MSSQL for platform service

```powershell
kubectl.exe apply -f .\mssql-platform-deploy.yaml
```

test connection: localhost,1433
account: sa
password: Abc@123456

### Deploy RabitMQ service

```powershell
kubectl apply -f rabbitmq-deploy.yaml
```

Access to Rabbitmq through browser: http://localhost:15672

User: guest

Passwork: guest
