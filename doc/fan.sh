echo "Waiting for sigmatcp to initialize before launching fan"
/bin/sleep 30
echo "Starting fan control"
sudo python /home/osmc/beocreate-remote/doc/fan.py 
