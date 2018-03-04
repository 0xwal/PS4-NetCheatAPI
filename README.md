# netCheatAPI-for-PS4
Netcheat API for PS4


## Installation

Drop all dlls from here ```bin/release``` to ```[your netCheat folder]/APIs/```
* You need PS4API server payload to use this, you can download it from here [PS4API.bin](https://github.com/BISOON/ps4-api-server/releases) use corresponding update of PS4API.BIN
* netCheat app you can download it from here [ncUpdateDir.zip](http://netcheat.gamehacking.org/ncUpdater/).

## Note

Netcheat will read 65000 bytes in one request, so in the [API.cs](https://github.com/BISOON/netCheatAPI-for-PS4/blob/master/PS4API-NC/API.cs#L99) I just devided the number of requested bytes by 16384 to make sure the browser will not crash.


<p align=center>
  <img alt="Image1" src="https://gyazo.com/6632248f88b2106982d49d70ede0bfa9.png"/>
  
  <img alt="Image1" src="https://gyazo.com/4eca4a629a529b128081c8c3cfff919c.png"/>
</p>
