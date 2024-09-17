# fan
sudo chmod +x fan.py
sudo cp fan.service /lib/systemd/system/

# server
sudo cp beocreate.server.service /lib/systemd/system/
mkdir bin/
cd bin
wget https://github.com/vincss/beocreate-remote/releases/download/latest/server-ci-build.zip 
unzip server-ci-build.zip


sudo systemctl daemon-reload
