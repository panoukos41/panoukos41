#!/bin/bash

apt install unzip
curl -s https://ohmyposh.dev/install.sh | bash -s

file=$(eval echo ~${SUDO_USER}/.bashrc)

if [ $(grep -c '#oh-my-posh' $file) -eq 0 ]; then

cp panos.omp.json ~/.panos.omp.json

echo '' >> $file
echo '#oh-my-posh' >> $file
echo 'eval "$(oh-my-posh init bash --config ~/.panos.omp.json)"' >> $file
echo '' >> $file
echo 'alias cls="clear"' >> $file
echo '' >> $file

fi
