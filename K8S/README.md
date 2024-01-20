## K8S Note

This folder contain created kubectl deploy and service file

### Run kubectl node, include deploy and node port service

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
kubectl.exe rollout restart deployment platforms-deploy
```

### Dowload and apply Nginx

```powershell
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/aws/deploy.yaml
```

### Config temp url ro test Nginx

I try to add url ip to my local (127.0.0.1) for "acme.com" in
"C:\Windows\System32\drivers\etc\hosts"

Test APIGateway at this endpoint:
http://acme.com/api/platforms
