using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public int score;
    public Text scoreText;
    public GameObject notification;

    private bool doubleBack;

    void Awake()
    {
        CreateInstance();
    }

    // Use this for initialization
    void Start()
    {
        if (GameController.instance != null && MusicController.instance != null)
        {
            if (GameController.instance.isMusicOn)
            {
                MusicController.instance.PlayGameplaySound();
            }
            else
            {
                MusicController.instance.StopAllSound();
            }
        }
        GameObject retry_target = GameObject.FindGameObjectWithTag("Retry");
        if (retry_target != null)
        {
            GameObject remove_target = GameObject.FindGameObjectWithTag("Target");
            retry_target.transform.position = remove_target.transform.position;
            Destroy(remove_target);
            retry_target.tag = "Target";
            retry_target.GetComponent<Target>().enabled = true;
            score = GameController.instance.currentScore;
            UpdateGameplayController();
        }
        else
        {
            InitialGameplayVariables();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameplayController();

        if (doubleBack == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(GameObject.Find("Target"));

                //Advertisement.Banner.Hide();
                SceneManager.LoadScene("Main Menu");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            notification.SetActive(true);
            doubleBack = true;
            StartCoroutine(ShowTimer());
        }
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void UpdateGameplayController()
    {
        GameController.instance.currentScore = score;
        scoreText.text = score.ToString();
    }

    void InitialGameplayVariables()
    {
        GameController.instance.currentScore = 0;
        score = GameController.instance.currentScore;
        scoreText.text = score.ToString();
    }

    IEnumerator ShowTimer()
    {
        yield return new WaitForSeconds(2f);
        doubleBack = false;
        notification.SetActive(false);
    }


}
