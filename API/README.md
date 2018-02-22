# net-core-webapi-docker

dockerized default net core web api with swagger

```
git clone https://github.com/hebermattos/net-core-webapi-docker.git
cd net-core-webapi-docker
docker build -t api .
docker run --rm -p 5000:80 api
```

go to localhost:5000/swagger

## windows container

if you cannot acess, check https://blog.sixeyed.com/published-ports-on-windows-containers-dont-do-loopback/
