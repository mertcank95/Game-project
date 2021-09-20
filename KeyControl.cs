using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyControl : MonoBehaviour
{
    public static bool keyVar;
    public  Image imageKey;
    public  Sprite spriteKey;
    public AudioClip keySound;
    void Start()
    {
        keyVar = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            keyVar = true;
            Destroy(gameObject);
            imageKey.sprite = spriteKey;
            AudioSource.PlayClipAtPoint(keySound, transform.position);
        }
    }
    void Update()
    {
        
    }
}
