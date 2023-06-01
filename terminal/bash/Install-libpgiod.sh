#!/bin/bash

if [[ $(/usr/bin/id -u) -ne 0 ]]; then
    echo "Please run as root or use sudo."
    exit
fi

# https://raspberrypi.stackexchange.com/questions/111521/why-is-libgpiod-not-working-on-my-raspberry-pi

sudo apt install gpiod libgpiod-dev libgpiod-doc

# The standalone applications
# gpiodetect, gpioinfo, gpioget, gpioset, gpiofind and gpiomon