using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class CarBehaviour : MonoBehaviour
{
    public Sprite FullLightSprite;
    public Sprite LeftLightOffSprite;
    public Sprite RightLightOffSprite;
    public Sprite NoneLightSprite;
    public Sprite BloodStain;
    public AudioClip startEngine;
    public Light leftLight;


    public Light rightLight;
    public Light topLight;

    private Collider2D collider;
    private Rigidbody2D rb;
    private AudioSource audio;

    private SpriteRenderer carSprite;
    private Animator anim;
    
    

    private bool leftLightBroke = false;
    private bool rightLightBroke = false;
    private bool gameOverNextFrame = false;
    private bool carOff = true;
    private bool carPausedForLore = false;
    private bool magicActivated = false;



    // Start is called before the first frame update
    void Start()
    {
        carSprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        leftLightBroke = false;
        rightLightBroke = false;
        gameOverNextFrame = false;
        carOff = true;
        carPausedForLore = false;
        magicActivated = false;
        audio.Stop();
    }
    public bool HasActivatedMagic()
    {
        return magicActivated;
    }

    public void LightsOff()
    {
        leftLight.enabled = false;
        rightLight.enabled = false;
        topLight.enabled = false;
    }
    public void LightsOn()
    {
        leftLight.enabled = !leftLightBroke;
        rightLight.enabled = !rightLightBroke;

        if (!(leftLightBroke && rightLightBroke))
            topLight.enabled = true;
        else
            topLight.enabled = false;
    }
    public bool CheckLights()
    {
        return leftLight.enabled || rightLight.enabled;
    }
    public bool IsCarOff()
    {
        return carOff;
    }

    public void StartCar()
    {
        carOff = false;
        anim.SetTrigger("StartEngine");
    }

    void Update()
    {
        if (carOff)
        {
            LightsOff();
            if (Input.GetMouseButtonDown(0))
            {
                this.audio.PlayOneShot(startEngine,1);
                carOff = false;
                anim.SetTrigger("StartEngine");
                audio.Play();
            }
            return;
        }
        else
        {
            
        }


        if (gameOverNextFrame)
        {
            GameController.GetInstance().SetGameOver();
        }

        if (carSprite.sprite == NoneLightSprite)
        {
            LightsOff();
        } else
        {
            LightsOn();
        }

        if(leftLightBroke)
            carSprite.sprite = LeftLightOffSprite;
        if(rightLightBroke)
            carSprite.sprite = RightLightOffSprite;
        if(leftLightBroke && rightLightBroke)
        {
            carSprite.sprite = NoneLightSprite;
            gameOverNextFrame = true ;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Track"))
        {
            var contactPoint = collision.GetContact(0).point;
            Vector3 center = collider.bounds.center;
            Debug.Log("Fator de colisao:" + (center.y - contactPoint.y));
            ReduceLight(contactPoint.y - center.y);
            GameController.GetInstance().HitSome();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Fator de colisao:" + (transform.position.y - collision.transform.position.y));
            collision.transform.GetComponentInChildren<Animator>().enabled = false;
            collision.transform.GetComponentInChildren<SpriteRenderer>().sprite = BloodStain;
            rb.AddForce(Vector2.left * 2f, ForceMode2D.Impulse);
            ReduceLight(transform.position.y - collision.transform.position.y);
            GameController.GetInstance().HitSome();
        } else if (collision.CompareTag("SpecialEnemy"))
        {
            GameController.GetInstance().TurnOnScary();
            //Ativa o grito
        } else if (collision.CompareTag("Lore"))
        {
            if (!carPausedForLore)
            {
                anim.enabled = true;
                carOff = true;
                carPausedForLore = true;
                anim.SetTrigger("CarOff");
                audio.Stop();
            }
        }else if (collision.CompareTag("ActivateMagic"))
        {
            this.magicActivated = true;

        }else if(collision.gameObject.name == "Level2")
        {
            GameController.GetInstance().Level2();
        }
        else if (collision.gameObject.name == "Level3")
        {
            GameController.GetInstance().Level3();
        }
        else if (collision.gameObject.name == "Level4")
        {
            GameController.GetInstance().Credits();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SpecialEnemy"))
            GameController.GetInstance().TurnOffScary();

    }

    // Update is called once per frame
    void ReduceLight(float colisionPoint)
    {
        if(colisionPoint > 0.1)
        {
            if (leftLight.enabled)
            {
                leftLight.intensity = leftLight.intensity / 2;
                leftLightBroke = leftLight.intensity < 10;
            } else
            {
                rightLight.intensity = rightLight.intensity / 2;
                rightLightBroke = rightLight.intensity < 10;
            }
        }

        if (colisionPoint < -0.1)
        {
            if (rightLight.enabled)
            {
                rightLight.intensity = rightLight.intensity / 2;
                rightLightBroke = rightLight.intensity < 10;
            }
            else
            {
                leftLight.intensity = leftLight.intensity / 2;
                leftLightBroke = leftLight.intensity < 10;
            }
        }

        if (colisionPoint >= -0.1 && colisionPoint <= 0.1)
        {
            leftLight.intensity = leftLight.intensity / 2;
            leftLightBroke = leftLight.intensity < 10;
            rightLight.intensity = rightLight.intensity / 2;
            rightLightBroke = rightLight.intensity < 10;
        }

    }

}
