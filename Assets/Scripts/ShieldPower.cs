using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : MonoBehaviour
{   
    [Header("Gameobject // Objeto")]
    public GameObject shield;
    public bool activateShield;
    [Header("Particulas del escudo")]
    public ParticleSystem Shield;

    private Animator anim;
    [Header("Clase del objeto-jugador")]
    public Player player;
    private int isTapped =  0;
    public static ShieldPower instance2;
    public bool IsActive = false;

    

    float moveSpeed; // variable que se iguala a la variable de velocidad con el fin de resetearla mas adelante
     float jumpForce; // variable que se iguala a la variable de fuerza de salto con el fin de resetearla mas adelante

    void Start()
    {

        
        moveSpeed = player.runSpeed;
        jumpForce = player.jumpSpeed;
      
        activateShield = false;
        shield.SetActive(false);
        anim = GetComponent<Animator>();
        instance2 = this;
        
        
    }

    void Update()
    {
          
        if (Input.GetMouseButtonDown(0))
        {
            if(player.isGrounded) // si el objeto que contiene la clase de jugador está tocando el suelo...
            {
                if (!activateShield) // y el escudo no se ha activado
            {
                    IsActive = true;
                    SoundManager.Playsound("shield");
                    anim.SetBool("ShieldActivate", true);
                shield.SetActive(true);
                activateShield = true;
                player.runSpeed = 0;
                player.jumpSpeed = 0;
                isTapped = 1;
                ShieldParticles();
                

                }

            else // si el escudo ya se activo
            {
                IsActive = false;

                player.runSpeed = moveSpeed;
                player.jumpSpeed = jumpForce;
                shield.SetActive(false);
                activateShield = false;
                anim.SetBool("ShieldActivate", false);
                isTapped = 2;
                StartCoroutine(tapLimiter());
                Shield.Stop();
            }
            }
            
        }
    }

    public bool ActiveShield
    {
        get
        {
            return activateShield;
        }
        set
        {
            activateShield = value;
        }
    }

// limitador de clicks para que no sse buggee el escudo

    IEnumerator tapLimiter(){

        if(isTapped == 2){
        yield return new WaitForSeconds(1.0f);
        isTapped = 0;
        }
      

    }


    void ShieldParticles()
    {
        Shield.Play();

    }
}
