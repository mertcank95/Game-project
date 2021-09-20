using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsControl : MonoBehaviour
{
    Animator anim;
    public GameObject diamond;
    bool touch=true;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && touch)
        {
            anim.SetTrigger("ChestsOpen");
            touch = false;
            StartCoroutine("DiamondCreate");
        }
    }

    IEnumerator DiamondCreate()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(diamond, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
   

}
