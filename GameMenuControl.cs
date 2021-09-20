using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuControl : MonoBehaviour
{
    public GameObject gamePausePanel,mobilControlPanel, loadingUI;
    public Button musicBtn;
    public Sprite musicOn, musicOff;
    public AudioSource mainCameraAudioSource;
    AsyncOperation test;
    public Slider slider;



    public void Paused()
    {
        Time.timeScale = 0;
        gamePausePanel.SetActive(true);
        mobilControlPanel.SetActive(false);

    }

    public void Resume()
    {
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
        mobilControlPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
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


    public void BgMusicControl()
    {
        if(musicBtn.image.sprite == musicOn )//açýk
        {
            mainCameraAudioSource.enabled = false;
            musicBtn.image.sprite = musicOff;
            
        }
        else if(musicBtn.image.sprite == musicOff)//kapalý
        {
            mainCameraAudioSource.enabled = true;
            musicBtn.image.sprite = musicOn;
            
        }
    }

}
