using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSAuth : MonoBehaviour
{

    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(configuration);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();

        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Authentication Success");
            }
        });
    }
}

