using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemy;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<Animator>().SetBool("EnemyAttack1",true);
            InvokeRepeating("AttackActive", 1f,1f);
        }
    }

    void AttackActive()
    {
        if (gameObject.activeSelf == true)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        enemy.GetComponent<Animator>().SetBool("EnemyAttack1", false);
       

    }


}
