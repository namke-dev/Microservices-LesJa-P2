## K8S Note

### Create kubectl deploy and service file

### Run kubectl node, include deploy and node port service

```powershell
kubectl apply -f platforms-deploy.yaml
kubectl apply -f platforms-nodeport-service.yaml
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
