using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    Animator anim;
    public short enemyHealt = 1;
    public GameObject diamond;
    public AudioClip audioHurt;
    public bool animBool=false;
    public GameObject bloodEffect, miniEnemyDestroyEffect,attackEffect;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Dameged()
    {
        if (animBool)
        {
            anim.Play("EnemyHurt");
            Instantiate(bloodEffect, transform.position,Quaternion.identity);
        }
            
        if(attackEffect!=null)
            Instantiate(attackEffect, transform.position, Quaternion.identity);
        enemyHealt--;

        if (enemyHealt <= 0)
        {
            if (animBool)
            {
                anim.Play("EnemyDie");
                Destroy(gameObject, 1f);
            }else
            {
                Destroy(gameObject);
                Instantiate(miniEnemyDestroyEffect, transform.position, Quaternion.identity);
            }


            Instantiate(diamond, transform.position, Quaternion.identity);
        }
        AudioSource.PlayClipAtPoint(audioHurt, gameObject.transform.position);
    }


}
