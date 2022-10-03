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

## install dd-trace

 - https://github.com/DataDog/dd-trace-dotnet/releases/
 - download latest version and install
 - https://github.com/DataDog/dd-trace-dotnet/releases/download/v2.15.0/datadog-dotnet-apm-2.15.0-x64.msi

## login in datadog demo account

 - https://datadoghq.eu/



## run the datadog agent via docker-compose

 - in the root of this project I created a docker-compose file with the datadog agent

```yaml
version: "3.9"
services:
  dd:
    image: datadog/agent
    container_name: datadog-agent
    environment:
    - DD_API_KEY=${DD_API_KEY}
    - DD_APM_ENABLED=true
    - DD_LOGS_ENABLED=true
    - DD_LOGS_CONFIG_CONTAINER_COLLECT_ALL=true
    - DD_LOGS_CONFIG_DOCKER_CONTAINER_USE_FILE=false
    - "DD_CONTAINER_EXCLUDE=name:datadog-agent"
    - "DD_APM_DD_URL=https://trace.agent.datadoghq.eu"
    - DD_APM_NON_LOCAL_TRAFFIC=true
    - DD_DOGSTATSD_NON_LOCAL_TRAFFIC=true
    - DD_LOGS_INJECTION=true
    - DD_SITE=datadoghq.eu
    volumes:
    - /var/run/docker.sock:/var/run/docker.sock:ro
    - /var/lib/docker/containers:/var/lib/docker/containers:ro
    - /proc/:/host/proc/:ro
    - /opt/datadog-agent/run:/opt/datadog-agent/run:rw
    - /sys/fs/cgroup/:/host/sys/fs/cgroup:ro
    ports:
    - "8126:8126/tcp"
```


 - AND an .env file (not commited in github)

```
DD_API_KEY=YOUR_KEY
```


 - navigate in the wsl the project folder and up the compose

```bash
max@vin10:~$ cd /mnt/c/temp/dotnet-datadog-primer/
max@vin10:/mnt/c/temp/dotnet-datadog-primer$ ls
ProxyApi  README.md  WeatherApi  docker-compose.yml
max@vin10:/mnt/c/temp/dotnet-datadog-primer$ docker-compose up
```

## visual studio 

 - open WeatherApi project
 - run and trust certificates
 - visit swagger and test the get endpoint

## configure the WeatherApi project 

 - customize launchsettings.json adding these env variables to the kestrel profile


```
"CORECLR_ENABLE_PROFILING": "1",
"CORECLR_PROFILER": "{846F5F1C-F9AE-4B07-969E-05C26BC060D8}",
"DD_LOGS_INJECTION": "true",
"DD_RUNTIME_METRICS_ENABLED": "true",
"DD_TRACE_AGENT_URL": "http://localhost:8126",
"DD_ENV": "localhost",
"DD_SERVICE": "WeatherApi",
"DD_VERSION": "v1"
```

## configure and edit ProxyApi project 

 - customize launchsettings.json adding these env variables to the kestrel profile


```
"CORECLR_ENABLE_PROFILING": "1",
"CORECLR_PROFILER": "{846F5F1C-F9AE-4B07-969E-05C26BC060D8}",
"DD_LOGS_INJECTION": "true",
"DD_RUNTIME_METRICS_ENABLED": "true",
"DD_TRACE_AGENT_URL": "http://localhost:8126",
"DD_ENV": "localhost",
"DD_SERVICE": "ProxyApi",
"DD_VERSION": "v1"
```

  - edit code and call the get endpoint
  - check datadog traces 

## dockerize projects

 - add docker support in both projects
 - add services in docker compose yaml
 - disable https redirection middleware (out of scope https and docker)
 - enable swagger always


```yaml
  proxy:
    build:
      context: ./ProxyApi/
      dockerfile: ./Dockerfile
    environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ASPNETCORE_URLS=http://+:80  
    - AppSettings__WeatherApiUrl=http://api

    - "DD_ENV=compose"
    - "DD_SERVICE=ProxyApi"
    - "DD_VERSION=V1"            

    - "CORECLR_ENABLE_PROFILING=1"
    - "CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}"
    - "DD_LOGS_INJECTION=true"
    - "DD_RUNTIME_METRICS_ENABLED=true"
    - "DD_AGENT_HOST=datadog-agent"
    - "DD_TRACE_AGENT_PORT=8126"
    - "CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so"
    - "DD_DOTNET_TRACER_HOME=/opt/datadog"
    ports:
    - 6001:80
  api:
    build:
      context: ./WeatherApi/
      dockerfile: ./Dockerfile
    environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ASPNETCORE_URLS=http://+:80

    - "DD_ENV=compose"
    - "DD_SERVICE=WeatherApi"
    - "DD_VERSION=V1"            

    - "CORECLR_ENABLE_PROFILING=1"
    - "CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}"
    - "DD_LOGS_INJECTION=true"
    - "DD_RUNTIME_METRICS_ENABLED=true"
    - "DD_AGENT_HOST=datadog-agent"
    - "DD_TRACE_AGENT_PORT=8126"
    - "CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so"
    - "DD_DOTNET_TRACER_HOME=/opt/datadog"
    ports:
    - 6002:80
```

 - edit dockerfile adding the download of the tracer.deb and the installation

```dockerfile


FROM build AS publish
RUN dotnet publish "WeatherApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN TRACER_VERSION=2.15.0 && curl -Lo /tmp/datadog-dotnet-apm.deb https://github.com/DataDog/dd-trace-dotnet/releases/download/v${TRACER_VERSION}/datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb

FROM base AS final

# Copy the tracer from build target
COPY --from=publish /tmp/datadog-dotnet-apm.deb /tmp/datadog-dotnet-apm.deb
# Install the tracer
RUN mkdir -p /opt/datadog \
    && mkdir -p /var/log/datadog \
    && dpkg -i /tmp/datadog-dotnet-apm.deb \
    && rm /tmp/datadog-dotnet-apm.deb

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApi.dll"]
```

 - docker-compose build
 - docker-compose up -d
 - navigate the proxy swagger (6001) and test the get endpoint