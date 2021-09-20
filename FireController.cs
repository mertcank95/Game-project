using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    Rigidbody2D rigid;
    public Vector2 speed;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        rigid.velocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Diamond") || collision.gameObject.CompareTag("EnemyTrap")))
            Destroy(gameObject);    
    }


}
