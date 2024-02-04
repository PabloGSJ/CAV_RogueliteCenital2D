using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuScript : MonoBehaviour
{
    public void TryAgain()
    {
        //SceneManager.LoadScene()
        GameObject sceneController = GameObject.Find("RoomController");
        if(sceneController == null)
        {
            sceneController = GameObject.Find("Room");
            sceneController.GetComponent<SceneChanger>().RestartGame();
        }
        else
        {
            sceneController.GetComponentInChildren<SceneChanger>().RestartGame();
        }
        // Debug.Log("I work!");
    }
}
