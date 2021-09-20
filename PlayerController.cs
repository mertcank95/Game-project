using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public float jumpForce;
    public Transform leftFirePos, rightFirePos;
    public GameObject leftFire, rightFire;
    float timer;
    public float nextFireAttack;
    bool doubleJump, isJump,isGround;
    public float jumpDelay;
    public Transform feet;
    public float width, height;
    public LayerMask myLayer, enemy,miniEnemy;
    public float attackRange;
    public Transform attackLocation;
    public float nextAttackTime;
    float timerAttack;
    public bool leftClicked, rightClicked;
    public AudioClip audioSwordAttack,fireSound,playerJumpSound;
    public Image[] imageHearts;
    short healt = 3;
    public Sprite emptyHeart,heart;
    public  int diamondCounter;
    public Text diamondText;
    public GameObject gameOverPanel;
    public AudioClip gameOverSound,hurtSound,heartSound;
    public GameObject mobilPanel, scorePanel;
    public Button btnAttack;

    private InterstitialAd interstitial;

    void Start()
    {
        Time.timeScale = 1;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacinciLevel"))
        {
            PlayerPrefs.SetInt("kacinciLevel", SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(width, height, 0));
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
  
    void Update()
    {
        isGround = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(width, height), 360f, myLayer);
        if (healt > 3)
            healt = 3;

        timer += Time.deltaTime;
        timerAttack += Time.deltaTime;
        
        float h = Input.GetAxis("Horizontal");
        PlayerMove(h);  

       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }

        if (Input.GetKeyDown(KeyCode.E) && timer>nextFireAttack)
        {
            FireAttack();
        }*/

        if (leftClicked == true)
        {
            PlayerMove(-1);//sola gidicek 
        }
        if (rightClicked == true)
        {
            PlayerMove(1);//saða gidicek 
        }


    }
    
    void PlayerMove(float h)
    {
        if (h > 0)
        {
            spriteRenderer.flipX = false;
            attackLocation.localPosition = new Vector3(1f, -0.56f, 0);
        }

        else if (h < 0)
        {
            spriteRenderer.flipX = true;
            attackLocation.localPosition = new Vector3(-1f, -0.56f, 0);
        }
        rigid.velocity = new Vector2(h * moveSpeed, rigid.velocity.y);
        if(h!=0)
        anim.SetInteger("status", 1);
        else
            anim.SetInteger("status", 0);
    }


   public void PlayerAttack()
    {
        Collider2D[] damage = Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemy);
        anim.SetTrigger("Attack");
        for (int i = 0; i < damage.Length; i++)
        {
           // damage[i].gameObject.GetComponent<Enemy>().Dameged();
            damage[i].gameObject.GetComponent<EnemyDamage>().Dameged();
        }
        
        AudioSource.PlayClipAtPoint(audioSwordAttack, gameObject.transform.position);
        timerAttack = 0;
    }


    void StopPlayer()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (!isJump)
            anim.SetInteger("status", 0);
    }

  

    void PlayerJump()
    {
        if (isGround)
        {
            isJump = true;
            rigid.AddForce(new Vector2(0f, jumpForce));
            AudioSource.PlayClipAtPoint(playerJumpSound, gameObject.transform.position);
            if (isJump)
            {
                anim.SetInteger("status", 3);
                Invoke("DoubleJump", jumpDelay);
                    
            }
        }

        if (doubleJump && !isGround)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0f, jumpForce));
            anim.SetInteger("status", 3);
            doubleJump = false;
            AudioSource.PlayClipAtPoint(playerJumpSound, gameObject.transform.position);

        }

    }

    void DoubleJump()
    {
        doubleJump = true;
    }

   public void FireAttack()
    {
        if (timer > nextAttackTime)
        {
            if (spriteRenderer.flipX)
                Instantiate(leftFire, leftFirePos.position, Quaternion.identity);
            else if (!spriteRenderer.flipX)
                Instantiate(rightFire, rightFirePos.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position);
            timer = 0;
        }
       
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                PlayerDamage();
                break;
            case "Hurt":
                HealtConrtolIncrease(1, collision);
                break;
            case "Diamond":
                diamondCounter++;
                diamondText.text = "x " + diamondCounter;
                break;
            case "EnemyTrap":
                PlayerDamage();
                break;
            case "DestroyGround":
                healt = 0;
                PlayerDamage();
                break;
            case "Mushroom":
                rigid.AddForce(new Vector2(0, 1000));
                break;

            default:
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }

    void PlayerDamage()
    {
        anim.SetTrigger("Hurt");
        rigid.AddForce(new Vector2(0, 100f));
        AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        HealtControlDecline(1);
    }

    void HealtControlDecline(short amount)
    {
        if( healt<4 && healt>0)
        {
            imageHearts[healt - 1].sprite = emptyHeart;
            healt -= amount;
        }    
        if (healt == 0)
        {
            Destroy(gameObject, 0.7f);
            gameOverPanel.SetActive(true);
            AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
            mobilPanel.SetActive(false);
            scorePanel.SetActive(false);
            GameOverMob();
        }

    }
 
    void HealtConrtolIncrease(short amount, Collider2D collision)
    {
        if (healt >0 && healt<3)
        {
            healt += amount;
            for (int i = 0; i < healt; i++)
            {
                imageHearts[i].sprite = heart;
                AudioSource.PlayClipAtPoint(heartSound, transform.position);
                Destroy(collision.gameObject);
            }          
        }
        
    }
    public void MoveLeftMobil()
    {
        leftClicked = true;

    }

    public void MoveRightMobil()
    {
        rightClicked = true;
    }

    public void StopPlayerMobil()
    {
        leftClicked = false;
        rightClicked = false;
    }

    public void JumpMobil()
    {
        PlayerJump();
    }

    public void FireAttackMobil()
    {
        FireAttack();
    }
    public void MeleeAttackMobil()
    {
        PlayerAttack();
    }



    private void RequestInterstitial()
    {

        //
        //
        //orjinal id:ca-app-pub-8735782127180816/1839117433
        string adUnitId = "ca-app-pub-8735782127180816/1839117433";
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        interstitial.OnAdClosed += HandleOnAdClosed;
    }
    private void GameOverMob()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }
    private void OnEnable()
    {
        RequestInterstitial();
    }

    private void OnDisable()
    {
        interstitial.Destroy();
    }














}
