using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectsAttack : MonoBehaviour
{
    float timer;
    public float nextTime=2f;
    public GameObject fire;
    public Transform attackPos;
    void Start()
    {
        
    }

   
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > nextTime)
            EnemyAttack();
    }

    void EnemyAttack()
    {
        Instantiate(fire, attackPos.position, fire.transform.rotation);
        timer = 0;
    }


}
