FROM debian:12-slim

ENV container docker
ENV DEBIAN_FRONTEND noninteractive

RUN echo ... Start ...

RUN echo ... Install packages ...

# https://github.com/robertdebock/docker-debian-systemd
RUN apt-get update ; \
    apt-get install -y systemd systemd-sysv sudo curl \
    python3-dev python3-pip \
    libasound2-dev libxslt1-dev libxml2-dev zlib1g-dev libxml2-dev libxslt-dev git gcc alsa-utils; \
    apt-get clean ; \
    pip3 install xmltodict spidev requests RPi.GPIO pyalsaaudio --break-system-packages ; \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/* ; \
    rm -rf /lib/systemd/system/multi-user.target.wants/* ; \
    rm -rf /etc/systemd/system/*.wants/* ; \
    rm -rf /lib/systemd/system/local-fs.target.wants/* ; \
    rm -rf /lib/systemd/system/sockets.target.wants/*udev* ; \
    rm -rf /lib/systemd/system/sockets.target.wants/*initctl* ; \
    rm -rf /lib/systemd/system/sysinit.target.wants/systemd-tmpfiles-setup* ; \
    rm -rf /lib/systemd/system/systemd-update-utmp*

RUN echo ... Install dsptoolkit ...
RUN touch /boot/config.txt
RUN curl https://raw.githubusercontent.com/hifiberry/hifiberry-dsp/master/install-dsptoolkit -o install-dsptoolkit ; sh install-dsptoolkit

RUN echo ... Customize ... 
RUN sed -i "s/ --localhost//g" /lib/systemd/system/sigmatcp.service
RUN sed -i "s/Type=simple/Type=simple\nRestart=always\nRestartSec=5/g" /lib/systemd/system/sigmatcp.service

RUN echo ... End ...

VOLUME [ "/sys/fs/cgroup" ]
CMD ["/lib/systemd/systemd"]


