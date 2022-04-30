using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{

    public Text scoreText;

    private int score;

    private GameObject target_save;

    // Use this for initialization
    void Start()
    {
        target_save = GameObject.Find("Target");
        target_save.transform.position = new Vector3(100, 100, 100);
        InitializeVariables();
        if (GameController.instance != null && MusicController.instance != null)
        {
            if (GameController.instance.isMusicOn)
            {
                MusicController.instance.StopAllSound();
                MusicController.instance.PlayFailedSound();
            }
        }
    }

    void InitializeVariables()
    {
        score = GameController.instance.currentScore;
        scoreText.text = score.ToString();
    }

    public void RewardButton()
    {
        GameObject rewarded = GameObject.Find("RewardedAd");
        target_save.tag = "Retry";
        rewarded.GetComponent<AdMobRewarded>().RequestRewarded();

    }

    public void RestartButton()
    {

        Destroy(target_save);
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
