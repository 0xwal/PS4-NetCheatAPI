# netCheatAPI-for-PS4
Netcheat API for PS4 4.05


## Installation

Drop all dlls from here ```bin/release``` to ```[your netCheat folder]/APIs/```
* You need PS4API server payload to use this, you can download it from here [PS4API.bin](https://github.com/BISOON/ps4-api-server/releases)
* netCheat app you can find the source code [here](https://github.com/Dnawrkshp/NetCheatPS3) or google it.

## Note

Netcheat will read 65000 bytes in one request, so in the [API.cs](https://github.com/BISOON/netCheatAPI-for-PS4/blob/master/PS4API-NC/API.cs#L99) I just devided the number of requested bytes by 16384 to make sure the browser will not crash.
