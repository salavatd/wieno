using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static bool FailGame;
    public static bool WinGame;
    public static bool VibrationFlag;
    public static bool ReplayFlag;

    public GameObject PanelMenu;
    public GameObject Starter;
    public GameObject LevelWin;
    public GameObject LevelFail;

    public const int LEVELS = 20;

    void Awake()
    {
        Time.timeScale = 0;
        FailGame = false;
        WinGame = false;
        VibrationFlag = true;
        ReplayFlag = false;
    }

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var tmp = new System.Object[1024];
        for (int i = 0; i < 1024; i++) tmp[i] = new byte[1024];
        tmp = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelFail.activeSelf && !LevelWin.activeSelf && !Starter.activeSelf)
        {
            CallMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Menu")
        {
            Application.Quit();
        }

        if (FailGame)
        {
            LevelFail.SetActive(true);
            if (VibrationFlag && PlayerPrefs.GetInt("Audio") == 1 && Time.timeScale == 1)
            {
                Handheld.Vibrate();
                VibrationFlag = false;
            }
            Time.timeScale = 0;
        }

        if (WinGame)
        {
            LevelWin.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex < LEVELS) PlayerPrefs.SetInt("PlayerLevel", SceneManager.GetActiveScene().buildIndex+1);
            else if (SceneManager.GetActiveScene().buildIndex == LEVELS)
            {
                PlayerPrefs.SetInt("PlayerLevel", 1);
                PlayerPrefs.SetInt("AdOff", 1);
            }
            Time.timeScale = 0;
        }

        if (ReplayFlag)
        {
            BallPosition.Started = true;
            GreenBallsPosition.Started = true;
            RedBallsPosition.Started = true;
            VibrationFlag = true;
            ReplayFlag = false;
            if (LevelFail.activeSelf)
            {
                LevelFail.SetActive(false);
                FailGame = false;
                
            }
            else if (PanelMenu.activeSelf)
            {
                PanelMenu.SetActive(false);
            }
            Starter.SetActive(true);
        }
    }

    void CallMenu()
    {
        if (!PanelMenu.activeSelf)
        {
            PanelMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PanelMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void OnMouseDown()
    {
        if (!PanelMenu.activeSelf) Time.timeScale = 1;
        if (Starter.activeSelf)
        {
            GameObject.Find("ClickAudio").GetComponent<AudioSource>().Play();
        }
        Starter.SetActive(false);
    }
}