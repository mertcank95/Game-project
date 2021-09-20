using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelControl : MonoBehaviour
{
    public GameObject loadingUI;
    public Slider slider;
    AsyncOperation test;
    public GameObject levels;

    private void Start()
    {
        //levels = GameObject.Find("LevelsPanel");
        int playerLvl = PlayerPrefs.GetInt("kacinciLevel");
        if (playerLvl <= 9)
        {
            for (int i = 0; i < playerLvl; i++)
            {
                levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
        else if (playerLvl > 9)
        {
            for (int i = 0; i < 9; i++)
            {
                levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
        }
    }
    public void LoadLevel()
    {
        if (PlayerPrefs.GetInt("kacinciLevel") == 0)
        {
            StartCoroutine(LoadNextLevel(1));
        }
        else
        {
            StartCoroutine(LoadNextLevel(PlayerPrefs.GetInt("kacinciLevel")));
        }
    }
    
    public void GoToLevel(int levelIndex)
    {
        StartCoroutine(LoadNextLevel(levelIndex));
    }

    IEnumerator LoadNextLevel(int lvlIndex)
    {
        loadingUI.SetActive(true);
        test = SceneManager.LoadSceneAsync(lvlIndex);
       
        while (test.isDone == false)
        {
            slider.value = test.progress;
            yield return null;
        }
        
    }
}
