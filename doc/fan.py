# PWM FAN
# sudo screen -S fan -d -m /home/osmc/fan/fan.sh
import RPi.GPIO as GPIO
import time
import subprocess as sp
import sys

# initializing GPIO, setting mode to BOARD.
# Default pin of FAN Adapter is physical pin 32, GPIO12; 

MIN_SPEED=60
MIN_TEMP=55

MAX_SPEED=100
MAX_TEMP=70

Fan = 12  #if you connect to pin txd physical pin 8, GPIO14，then set to :Fan = 8
GPIO.setmode(GPIO.BCM)
GPIO.setup(Fan, GPIO.OUT)

p = GPIO.PWM(Fan, 50)
p.start(1)

temp = 0
speed = 0

try:
    while True:
        temp = float(sp.getoutput("cat /sys/class/thermal/thermal_zone0/temp")) / 1000

        if temp < MIN_TEMP:
            speed = MIN_SPEED
        elif temp > MIN_TEMP and temp < MAX_TEMP:
            temp_interval = (MAX_TEMP-MIN_TEMP)
            speed_interval = (MAX_SPEED-MIN_SPEED)
            temp_offset = (temp - MIN_TEMP)
            # print(f'temp_interval:{temp_interval:f} ')
            # print(f'speed_interval:{speed_interval:f} ')
            # print(f'temp_offset:{temp_offset:f} ')
            speed =  ((temp_offset/temp_interval)*speed_interval)+MIN_SPEED
        elif temp > MAX_TEMP:
            speed = MAX_SPEED

        p.ChangeDutyCycle(speed)
        print(f'{temp:2.0f} c => {speed:2.0f} %', file=sys.stderr)
        # sys.stdout.write(f'{temp:2.0f} °c => {speed:2.0f} %')
        time.sleep(2)

except KeyboardInterrupt:
    pass
p.stop()
GPIO.cleanup()
