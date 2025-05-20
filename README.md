This is a playground for kubernetes

There are 2 apis which speak to each other and then the first api has the ability to fire up the console app. Get the standard output and return it from the api. 

I will also be adding a configuration file for harbor as that would be my local image registry

I will also be adding k8s config files. The point is to understand the power of k8s and make the three aps talk to each other and also configure high availability. More specs will come later on as I dig deeper.

Brief Summary of the Kubernetes Architecture and how it applies to this
solutions's setup.

1. Execution layer in the face of the pod definition. This
   holds the kubelet, which is the agent that runs the pods on the node. The
pods responsibilities are:
    - start, stop, restart, monitor (health check) the pods running on the node
2. Orchestration layer in the face of the deployment definition. The deployment
   controller(which is located on the control plane) deals with this. Its
responsibilities are: 
    - Maintain a certain number of replicas that ensure high availability. Also
      it's responsible for rolling updates and rollbacks.
3. Network layer in the face of the service definition. 
