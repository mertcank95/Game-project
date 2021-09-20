using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondControl : MonoBehaviour
{
    public float rotateSpeed = 1f, diamodSpeed=1f;
    GameObject diamondImage;
    bool isFlying;
    public AudioClip diamondSound;
    public enum Coin
    {
        Flycoin,
        DestroyCoin
    }
    public Coin coin;
    void Start()
    {
        
        isFlying = false;
        diamondImage = GameObject.Find("DiamondImage");
        

    }


    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed, 0));
        if (isFlying)
        {
            transform.position = Vector2.Lerp(transform.position, diamondImage.transform.position, diamodSpeed * Time.deltaTime);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(coin == Coin.Flycoin)
            {
                isFlying = true;
                
            }

            if(coin == Coin.DestroyCoin)
            {
                Destroy(gameObject);
            }
           AudioSource.PlayClipAtPoint(diamondSound, transform.position);

        }

    }


}
