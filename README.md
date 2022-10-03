# dotnet-datadog-primer

## Prerequisites

- install wsl
- install ubuntu 20.04 LTS 
- install git for windows
- install vscode
  - extensions:
    - wsl
    - docker
    - markdown
- install vs 2022
- install chrome 
- install dotnet 6

## This repo

- create gitignore

```cmd
dotnet new gitignore
```

- create 2 APIs

```cmd
PS C:\temp\dotnet-datadog-primer> dotnet new webapi -n WeatherApi -o WeatherApi
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\temp\dotnet-datadog-primer\WeatherApi\WeatherApi.csproj...
  Determining projects to restore...
  Restored C:\temp\dotnet-datadog-primer\WeatherApi\WeatherApi.csproj (in 1.68 sec).
Restore succeeded.


PS C:\temp\dotnet-datadog-primer> dotnet new webapi -n ProxyApi -o ProxyApi
The template "ASP.NET Core Web API" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\temp\dotnet-datadog-primer\ProxyApi\ProxyApi.csproj...
  Determining projects to restore...
  Restored C:\temp\dotnet-datadog-primer\ProxyApi\ProxyApi.csproj (in 293 ms).
Restore succeeded.
```

## docker wsl

```bash
sudo apt-get update
```
```bash
sudo apt-get install apt-transport-https ca-certificates curl gnupg lsb-release
```
```bash
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
```
```bash
echo \
"deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
$(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```

```bash
sudo apt-get update && sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose
```

```bash
sudo visudo
```

add this line
```bash
<user_name> ALL=(ALL) NOPASSWD: /usr/bin/dockerd
```
where <user_name> is the user name setup in Ubuntu

```bash
sudo usermod -a -G docker $USER
```

exit and open new shell

```bash
sudo service docker start
docker run hello-world
```


```bash
max@vin10:~$ sudo service docker start
[sudo] password for max:
 * Starting Docker: docker 

max@vin10:~$ docker run hello-world
Unable to find image 'hello-world:latest' locally
latest: Pulling from library/hello-world
2db29710123e: Pull complete
Digest: sha256:62af9efd515a25f84961b70f973a798d2eca956b1b2b026d0a4a63a3b0b6a3f2
Status: Downloaded newer image for hello-world:latest

Hello from Docker!
This message shows that your installation appears to be working correctly.

To generate this message, Docker took the following steps:
 1. The Docker client contacted the Docker daemon.
 2. The Docker daemon pulled the "hello-world" image from the Docker Hub.
    (amd64)
 3. The Docker daemon created a new container from that image which runs the
    executable that produces the output you are currently reading.
 4. The Docker daemon streamed that output to the Docker client, which sent it
    to your terminal.

To try something more ambitious, you can run an Ubuntu container with:
 $ docker run -it ubuntu bash

Share images, automate workflows, and more with a free Docker ID:
 https://hub.docker.com/

For more examples and ideas, visit:
 https://docs.docker.com/get-started/
```

## upgrade docker compose

```bash
max@vin10:~$ docker-compose -v
docker-compose version 1.25.0, build unknown
```

 - https://docs.docker.com/compose/install/other/#install-compose-standalone

```bash
sudo curl -SL https://github.com/docker/compose/releases/download/v2.11.2/docker-compose-linux-x86_64 -o /usr/local/bin/docker-compose
```

```bash
sudo rm /usr/bin/docker-compose
```

```bash
sudo chmod +x /usr/local/bin/docker-compose
```

```bash
sudo ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose
```

```bash
max@vin10:~$ docker-compose -v
Docker Compose version v2.11.2
```


