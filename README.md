# How to run:

## Download and install docker

### Windows:

First, you need to download docker from [here](https://www.docker.com/get-started).

### Linux:

All necessary information is on the docker [webpage](https://docs.docker.com/engine/install/ubuntu/).

```
 $ sudo apt-get update
 $ sudo apt-get install docker-ce docker-ce-cli containerd.io
```

## Run the project

To start the project you have to be in the root path of the project.

To build:

```
docker-compose build
```

To build the project and run it:

```
docker-compose up
```

And close it:

```
docker-compose down
```

Web is available at: 
```
http://localhost:3000/
```
API is available at: 
```
http://localhost:8000/
```

