using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorControl : MonoBehaviour
{
    public Sprite sprite;
    SpriteRenderer spriteRender;
    BoxCollider2D boxCollider;
    public Image imageKey;
    public Sprite  emptySpriteKey;
    public AudioClip doorSound;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && KeyControl.keyVar)
        {
            spriteRender.sprite = sprite;
            boxCollider.enabled = false;
            KeyControl.keyVar = false;
            imageKey.sprite = emptySpriteKey;
            AudioSource.PlayClipAtPoint(doorSound, transform.position);

        }

    }
}
