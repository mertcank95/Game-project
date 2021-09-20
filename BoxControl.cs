using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    short boxHealt;
    public GameObject diamond, heart,boomEffect;
    public AudioClip breakingSound;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FirePlayer"))
        {
            short r = (short)Random.Range(0, 4);
            Instantiate(boomEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(breakingSound, transform.position);
            if(r==1)
                Instantiate(heart, transform.position, Quaternion.identity);
            else
                Instantiate(diamond, transform.position, Quaternion.identity);

        }
    }


}
