# beocreate-remote
beocreate-remote

# ToDo
- UI
    - Vertical Slider
    - Icon button ( + / - / OnOff )
    - Color
- Configuration
- Audio at Launch ?
- CI
    - https://thewissen.io/making-maui-cd-pipeline/
    - https://blog.taranissoftware.com/build-net-maui-apps-with-github-actions 


# SigmaTcp 
dsptoolkit get-meta volumeControlRegister
dsptoolkit read-hex 106

0.10 => 00 19 99 99 : 1677721
0.09 => 00 17 0A 3D : 1509949
0.05 => 00 0C CC CC : 838860
0.01 => 00 02 8F 5C : 167772


# Info 
dotnet build -f net8.0-android34.0

usage: dsptoolkit [-h] [--delay DELAY] [--host HOST] [--timeout TIMEOUT]
                  {adjust-volume,apply-fir-filter-left,apply-fir-filter-right,apply-fir-filters,apply-iir-filters,apply-iir-filters-left,apply-iir-filters-right,apply-rew-filters,apply-rew-filters-left,apply-rew-filters-right,apply-settings,check-eeprom,clear-iir-filters,get-checksum,get-limit,get-loudness,get-memory,get-meta,get-prog,get-samplerate,get-volume,get-xml,install-profile,load,loop-read-dec,loop-read-hex,loop-read-int,loop-read-reg,mute,read-dec,read-hex,read-int,read-reg,reset,save,set-limit,set-loudness,set-volume,store,store-filters,store-settings,tone-control,unmute,version,write-mem,write-reg}

                  