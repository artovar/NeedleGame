using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
public class GPGSAuth : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = false;
            platform = PlayGamesPlatform.Activate();
        }

        ((GooglePlayGames.PlayGamesPlatform)Social.Active).localUser.Authenticate(success =>
        {
            if (success)
            {
                print("Login succesful");
                Social.Active.ShowLeaderboardUI();
            }
            else
            {
                print("Login INCORRECT");
            }
        });
    }
}

