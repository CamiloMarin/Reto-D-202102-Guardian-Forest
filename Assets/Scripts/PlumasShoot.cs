﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlumasShoot : MonoBehaviour
{
    public Transform PlumaSpawner;
    public GameObject PlumaPrefab;
    //public AudioSource disparoplayerSource;
    //public ParticleSystem ps;
    public Animator anim;
    public float fireRate;
    private float nextfire;
    private bool isAttacking = false;
    
    Player player;
    public Rigidbody2D rB;
  

    private void PlayerShooting()
    {
        if (Input.GetMouseButtonUp(0) && Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;
            Instantiate(PlumaPrefab, PlumaSpawner.position, PlumaSpawner.rotation);
            isAttacking = true;
            
            //disparoplayerSource.Play();
            


            //if (ps != null)
            //{
            //  Instantiate<ParticleSystem>(ps, transform.position, ps.transform.rotation).Play();
           
            //}
        }



    }

    void Start()
    {

        //disparoplayerSource = GetComponent<AudioSource>();
        player = gameObject.GetComponent<Player>();    
        rB = GetComponent<Rigidbody2D>();
       
    }
    void Update()
    {

        PlayerShooting();
        isJumpingEnable();
        
        StartCoroutine(isAttackEnable());
        


    }


    IEnumerator isAttackEnable()
    {   
        if(isAttacking)
        {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        isAttacking = false;
        anim.SetBool("isAttacking", false);
        }
    }

    void isJumpingEnable(){
        if(player.isFalling == true){
        rB.drag = 15f;
        }else
        {
            rB.drag=0;
        }
    }

}
