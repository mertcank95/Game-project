using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilControlUI : MonoBehaviour
{
    PlayerController player;
    float timer, nextAttackTime=0.7f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void MoveLeftMobil()
    {
        player.MoveLeftMobil();
    }

    public void MoveRightMobil()
    {
        player.MoveRightMobil();
    }

    public void StopPlayerMobil()
    {
        player.StopPlayerMobil();

    }

    public void JumpMobil()
    {
        player.JumpMobil();
    }

    public void FireMobil()
    {
        player.FireAttackMobil();
    
    }

    public void MeleeAttackMobil()
    {

        if (timer > nextAttackTime)
        {
            player.MeleeAttackMobil();
            timer = 0;
        }
        
      
    }



}
