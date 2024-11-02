# WIP

docker run --tty --cgroupns=host --privileged --volume /sys/fs/cgroup:/sys/fs/cgroup -it -p 8086:8086 --restart=always $(docker build -q .)

docker exec -it _ /bin/bash

curl https://raw.githubusercontent.com/hifiberry/create/refs/heads/development/Beocreate2/beo-dsp-programs/beocreate-universal-11.xml -O

dsptoolkit set-volume 0.05
dsptoolkit write-mem 4841 1