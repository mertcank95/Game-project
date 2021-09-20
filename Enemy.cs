using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    //public SpriteRenderer playerSpiriteRender;
    short enemyHealt;
    SpriteRenderer sprite;
    float currentSpeed;
    public float speed = 2;
    bool turn;
    public Transform left, right;
    public GameObject leftAttack, rightAttack;
    public AudioClip audioHurt;
    public GameObject diamond;
    public GameObject fireExpolison;

    void Start()
    {
        enemyHealt = 3;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        FindDirection();
        turn = true;
        anim.SetInteger("status", 1);

    }

    void Update()
    {  
        MoveEnemy();
        TurnEnemy();
    }
    void FindDirection()
    {
        if (speed < 0)
            sprite.flipX = true;
        else if (speed > 0)
            sprite.flipX = false;
    }

    void TurnEnemy()
    {
        if (!sprite.flipX && transform.position.x >= right.position.x)//saðdaki game objeye gelene kadar
        {
            if (turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TrunLeft", currentSpeed);
            }

        }
        else if (sprite.flipX && transform.position.x <= left.position.x)
        {

            if (turn)
            {
                turn = false;
                currentSpeed = speed;
                speed = 0;
                StartCoroutine("TrunRight", currentSpeed);
            }
        }

    }

    IEnumerator TrunLeft(float currentSpeed)
    {
        anim.SetInteger("status", 0);
        yield return new WaitForSeconds(2f);
        anim.SetInteger("status", 1);
        sprite.flipX = true;
        speed = -currentSpeed;
        turn = true;
        rightAttack.SetActive(false);
        leftAttack.SetActive(true);

    }

    IEnumerator TrunRight(float currentSpeed)
    {
        anim.SetInteger("status", 0);
        yield return new WaitForSeconds(2f);
        anim.SetInteger("status", 1);
        sprite.flipX = false;
        speed = -currentSpeed;
        turn = true;
        rightAttack.SetActive(true);
        leftAttack.SetActive(false);
    }

    void MoveEnemy()
    {
        rigid.velocity = new Vector2(speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FirePlayer"))
        {
            Destroy(collision.gameObject);
            Damegeda();
            Instantiate(fireExpolison, gameObject.transform.position,Quaternion.identity);
        }
    }
    
      public void Damegeda()
      { 
        anim.Play("EnemyHurt");
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();  
        enemyHealt--;
        if (enemyHealt <= 0)
        {
            anim.Play("EnemyDie");
            Destroy(gameObject, 1.2f);
            Instantiate(diamond, transform.position,Quaternion.identity);
            rightAttack.SetActive(false);
            leftAttack.SetActive(false);
        }
        AudioSource.PlayClipAtPoint(audioHurt, gameObject.transform.position);
      }





}
