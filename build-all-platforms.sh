# Tested in Windows and PowerShell Core Only

cd src/SharpLoad.Application

dotnet publish SharpLoad.Application.csproj -o ../../Release/win-x64-nodependencies -c Release -r win-x64 -p:PublishSingleFile=true
dotnet publish SharpLoad.Application.csproj -o ../../Release/win-x64-netcore31runtime -c Release -r win-x64 --no-self-contained

dotnet publish SharpLoad.Application.csproj -o ../../Release/linux-x64-nodependencies -c Release -r linux-x64 -p:PublishSingleFile=true
dotnet publish SharpLoad.Application.csproj -o ../../Release/linux-x64-netcore31runtime -c Release -r linux-x64 --no-self-contained

dotnet publish SharpLoad.Application.csproj -o ../../Release/linux-arm-nodependencies -c Release -r linux-arm -p:PublishSingleFile=true
dotnet publish SharpLoad.Application.csproj -o ../../Release/linux-arm-netcore31runtime -c Release -r linux-arm --no-self-contained

dotnet publish SharpLoad.Application.csproj -o ../../Release/osx-x64-nodependencies -c Release -r osx-x64 -p:PublishSingleFile=true
dotnet publish SharpLoad.Application.csproj -o ../../Release/osx-x64-netcore31runtime -c Release -r osx-x64 --no-self-contained
