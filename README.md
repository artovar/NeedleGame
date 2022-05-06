# NeedleGame : PLEASE READ
Pin the Needle Unity Game

Hi Rob, I'm going to leave a few comments arround the project parts that I end up doing and how to test it.

Ads: Ads completly work fine on the Google Play apk. It shows you real ads from google and Unity Ads test.

Games Services: When you Build and Run the game locally it works. You will see the sign in, a leaderboard and one achievemnt.

GoogleSheets: It works on debugging in Unity (running on the device emulator). This is the Google Sheet where the scores and players names are written: https://docs.google.com/spreadsheets/d/1lK3D_AZ_wFdjKhaVovHmNxZPl7qG4QLDYyJLRYDHbm0/edit?usp=sharing


This problems on production are due to OAuth, credentials and Google Play/Cloud permissions issues that I'm not able to handle on time. I've been debugging for 2 days every single stage of the project and I couldn´t find the connection issue. I believe it has something to do with SHA-1 fingerprints that are different on local and on production.

The work is done and I coded everything that was required, it's just a matter of wrong configuration. I hope this is not a big problem.

Thanks
