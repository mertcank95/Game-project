using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndScript : MonoBehaviour
{
    public GameObject gameEndPanel,mobilPanel,scorePanel;
    public Image emtyStar1,emtyStar2,emtyStar3;
    public Sprite star;
    public Text diamondText;
    public Button nextButton;
    public int starDontWin,star2win,star3win;
    public AudioClip gameEndSound;
    PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
           StartCoroutine("GameEnd");
    }


    IEnumerator GameEnd()
    {
        int diamond = player.diamondCounter;
        diamondText.text = "x " + diamond;
        gameEndPanel.SetActive(true);
        mobilPanel.SetActive(false);
        scorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameEndSound, transform.position);
        player.StopPlayerMobil();
        if (star2win>diamond && starDontWin >= diamond)
        {
            emtyStar1.sprite = star;
        }
        else if(star2win <= diamond  && diamond < star3win)
        {
            emtyStar1.sprite = star;
            emtyStar2.sprite = star;
            yield return new WaitForSeconds(2.2f);
            nextButton.interactable=true;
        }
        else if (diamond >= star3win)
        {
            emtyStar1.sprite = star;
            emtyStar2.sprite = star;
            emtyStar3.sprite = star;
            yield return new WaitForSeconds(2.2f);
            nextButton.interactable = true;
        }

    }

}
