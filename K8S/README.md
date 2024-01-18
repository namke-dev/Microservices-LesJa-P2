```powershell
kubectl apply -f platforms-deploy.yaml
kubectl apply -f platforms-nodeport-service.yaml
```

check container endpoint

```powershell
kubectl get service
```

CLI return

```powershell
NAME                    TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)        AGE
kubernetes              ClusterIP   10.96.0.1       <none>        443/TCP        18h
platformnpservice-srv   NodePort    10.103.218.33   <none>        80:32558/TCP   18s
```

access the node port service following url:
http://localhost:32558/api/platforms
