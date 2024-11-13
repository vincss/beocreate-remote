# WIP

# build
docker run --tty --cgroupns=host --privileged --volume /sys/fs/cgroup:/sys/fs/cgroup  -v /storage/downloads/beocreate:/root/beocreate-server/ -it -p 8086:8086 -p 5000:5000 --restart=always $(docker build -q .)
# run 
docker run --tty --cgroupns=host --privileged -v /sys/fs/cgroup:/sys/fs/cgroup  -v /storage/downloads/beocreate:/root/beocreate-server/ -v /etc/localtime:/etc/localtime:ro -it -p 8086:8086 -p 5000:5000 --restart=always beocreate_1

docker exec -it _ /bin/bash

curl https://raw.githubusercontent.com/hifiberry/create/refs/heads/development/Beocreate2/beo-dsp-programs/beocreate-universal-11.xml -O

dsptoolkit set-volume 0.05
dsptoolkit write-mem 4841 1

dsptoolkit save

dsptoolkit read-mem 4841 

journalctl -u beocreate-server -f