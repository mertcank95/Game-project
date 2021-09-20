using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayControl : MonoBehaviour
{
    public GameObject levelPanel, menuPanel;
    public void GotoPanel()
    {
        if (menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
            levelPanel.SetActive(true);
        }
        else if (levelPanel.activeSelf)
        {
            menuPanel.SetActive(true);
            levelPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   

   


}
