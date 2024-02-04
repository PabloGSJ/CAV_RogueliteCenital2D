using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void BackToMenu()
    {
        //SceneManager.LoadScene()
        GameObject sceneController = GameObject.Find("RoomController");
        if(sceneController == null)
        {
            sceneController = GameObject.Find("Room");
            sceneController.GetComponent<SceneChanger>().BackToMenu();
        }
        else
        {
            sceneController.GetComponentInChildren<SceneChanger>().BackToMenu();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GodMode()
    {
        //TODO
    }

}
