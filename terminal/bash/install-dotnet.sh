#!/bin/bash

if [[ $(/usr/bin/id -u) -ne 0 ]]; then
    echo "Please run as root or use sudo."
    exit
fi

version=https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-5.0.0-rc.2-linux-arm32-binaries

wget $version
mkdir -p /usr/share/dotnet
tar -zxf dotnet-sdk-latest-linux-arm.tar.gz -C /usr/share/dotnet
ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# mkdir -p $HOME/dotnet
# tar zxf dotnet-sdk-6.0.100-preview.3.21202.5-linux-x64.tar.gz -C $HOME/dotnet
# export DOTNET_ROOT=$HOME/dotnet
# export PATH=$PATH:$HOME/dotnet