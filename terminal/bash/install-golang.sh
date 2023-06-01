#!/bin/bash

if [[ $(/usr/bin/id -u) -ne 0 ]]; then
    echo "Please run as root or use sudo."
    exit
fi

# Code to download can be found at:
# https://www.e-tinkers.com/2019/06/better-way-to-install-golang-go-on-raspberry-pi/

bin="/usr/bin/go"
local="/usr/local"
go="$local/go"

sudo rm -r $bin &>/dev/null
sudo rm -r $go &>/dev/null

export golang="$(curl https://golang.org/dl/|grep armv6l|grep -v beta|head -1|awk -F\> {'print $3'}|awk -F\< {'print $1'})"

wget https://golang.org/dl/$golang
sudo tar -C $local -xzf $golang
rm $golang
unset golang

ln -s "$go/bin/go" $bin &>/dev/null

rm "install-golang.sh"