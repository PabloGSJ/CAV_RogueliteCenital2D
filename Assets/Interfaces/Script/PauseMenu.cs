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
