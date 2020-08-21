REM dotnet publish -c Release
docker build -t asesoresapiimage:latest -f Dockerfile .
docker login --username admin --password 4l3x1sT3x4s  registry.fconfia.com
docker tag asesoresapiimage registry.fconfia.com/asesoresapiimage
docker push registry.fconfia.com/asesoresapiimage:latest
pause