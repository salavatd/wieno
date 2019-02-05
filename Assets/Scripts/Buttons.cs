using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject OnAudio;
    public GameObject OffAudio;
    int tempLevel;

    void Start()
    {
        if (gameObject.name == "Audio")
        {
            if (!PlayerPrefs.HasKey("Audio")) PlayerPrefs.SetInt("Audio", 1);
            if (PlayerPrefs.GetInt("Audio") == 0)
            {
                OnAudio.SetActive(false);
                OffAudio.SetActive(true);
                AudioListener.volume = 0f;
            }
            else
            {
                OnAudio.SetActive(true);
                OffAudio.SetActive(false);
                AudioListener.volume = 1f;
            }
        }
    }

    void OnMouseDown()
    {
        transform.localScale = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f);
    }

    void OnMouseUp()
    {
        transform.localScale = new Vector3(transform.localScale.x / 1.1f, transform.localScale.y / 1.1f, transform.localScale.z / 1.1f);
    }

    void OnMouseUpAsButton()
    {
        GameObject.Find("ClickAudio").GetComponent<AudioSource>().Play();
        switch (gameObject.name)
        {
            case "Play":
                {
                    if (PlayerPrefs.HasKey("PlayerLevel"))
                    {
                        tempLevel = PlayerPrefs.GetInt("PlayerLevel");
                    }
                    else
                    {
                        tempLevel = 1;
                        PlayerPrefs.SetInt("PlayerLevel", tempLevel);
                    }
                    SceneManager.LoadScene(tempLevel);
                    Resources.UnloadUnusedAssets();
                }
                break;
            case "Audio":
                {
                    if (PlayerPrefs.GetInt("Audio") == 1)
                    {
                        PlayerPrefs.SetInt("Audio", 0);
                        OnAudio.SetActive(false);
                        OffAudio.SetActive(true);
                        AudioListener.volume = 0f;
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Audio", 1);
                        OffAudio.SetActive(false);
                        OnAudio.SetActive(true);
                        AudioListener.volume = 1f;
                    }
                }
                break;
            case "Replay":
                {
                    GameControl.ReplayFlag = true;
                }
                break;
            case "NextLevel":
                {
                    if (SceneManager.GetActiveScene().buildIndex < GameControl.LEVELS)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        Resources.UnloadUnusedAssets();
                    }
                    else
                    {
                        SceneManager.LoadScene("Menu");
                        Resources.UnloadUnusedAssets();
                    }
                }
                break;
            case "Menu":
                {
                    SceneManager.LoadScene("Menu");
                    Resources.UnloadUnusedAssets();
                }
                break;
        }
    }
}