# stop
echo .. Stopping ..
sudo systemctl stop fan
sudo systemctl stop beocreate-server

# fan
echo .. Install fan ..
sudo cp fan.service /lib/systemd/system/

# server
echo .. Install beocreate-server ..
sudo cp beocreate-server.service /lib/systemd/system/
cd ..
mkdir bin/
cd bin
rm server-ci-build.zip 
wget https://github.com/vincss/beocreate-remote/releases/latest/download/server-ci-build.zip 
unzip -o server-ci-build.zip
chmod +x BeocreateRemote.Server


sudo systemctl daemon-reload

echo .. Start beocreate-server ..
sudo systemctl enable beocreate-server
sudo systemctl start beocreate-server

echo .. Start fan ..
sudo systemctl enable fan
sudo systemctl start fan
