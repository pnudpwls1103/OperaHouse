using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    static Managers instance;
    public static Managers Instance { get { Init(); return instance; } }

    BackgroundManager _background = new BackgroundManager();
    public static BackgroundManager Background { get { return Instance._background; } }

    ObstacleManager _obstacle = new ObstacleManager();
    public static ObstacleManager Obstacle { get { return Instance._obstacle; } }

    ScoreManager _score = new ScoreManager();
    public static ScoreManager Score { get { return Instance._score; } }

    void Start()
    {
        Init();
        _background.Init();
        _obstacle.Init();
    }

    void Update()
    {
    
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if(go == null)
        {
            go = new GameObject { name = "@Managers" };
        }

        if (go.GetComponent<Managers>() == null)
        {
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        instance = go.GetComponent<Managers>();    
    }

    public void CallObstacle()
    {
        _obstacle.GernerateBrick();
        StartCoroutine(_obstacle.GeneratePoop());
    }


    public void GameOver()
    {
        GameObject gameoverParent = GameObject.Find("GameOverParent");
        gameoverParent.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRetry()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        GameObject gameoverParent = GameObject.Find("GameOverParent");
        gameoverParent.transform.GetChild(0).gameObject.SetActive(false);

        _background.Init();
        _obstacle.Init();

        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.Init();

        ButterFlyMove butterfly = GameObject.Find("Butterfly").GetComponent<ButterFlyMove>();
        butterfly.Init();

        Time.timeScale = 1f;
    }
}
