This is a playground for kubernetes

There are 2 apis which speak to each other and then the first api has the ability to fire up the console app. Get the standard output and return it from the api. 

I will also be adding a configuration file for harbor as that would be my local image registry

I will also be adding k8s config files. The point is to understand the power of k8s and make the three aps talk to each other and also configure high availability. More specs will come later on as I dig deeper.

=======================

Implementation 1 (Docker-compose) is done. 
	- This is configured and its working with docker-compse.


Implementation 2(Kubernetes with Minikube) is complete. 
 - I have a local harbor repository accessible on a port 83.
 - I've added a dns entry called harbor-local and it's pointing to the ipv4 (localhost)
 - I've also started a minikube kubernetes cluster and I've applied the two deployment files.
 ![minikube-dashboard](/Resources/minikube-dashboard.png)

