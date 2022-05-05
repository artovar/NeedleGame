using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class AchivementScript : MonoBehaviour
{
    public void ShowAchievementsUI()
    {
        Social.Active.ShowAchievementsUI();
    }

    public void LeaderBoardButton()
    {
        // show leaderboard UI
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_max_score);
    }

}
