There are 2 dotnet apis and a console app.

There is a LoadBalancer type kubernetes service called api which routes traffic to all pods with labels app: api. 

 - Such a pod is the Main Api. 

There is a second kubernetes service of type ClusterIP which routes traffic to all pods with label app: api2. 
- Such a pod is api2.

Main api also has the ability to fire up the console app. Get the standard output and return it from the api. 

This Main api speaks to api1 and the console app.
 
===============================================================

Implementation 1 (Docker-compose) is done. 
	- This is configured and its working with docker-compse.


Implementation 2(Kubernetes with Minikube) is complete. 
 - I have a local harbor repository accessible on a port 83.
 - I've added a dns entry called harbor-local and it's pointing to the ipv4 (localhost)
 - I've also started a minikube kubernetes cluster and I've applied the two deployment files.
 ![minikube-dashboard](/Resources/minikube-dashboard.png)

