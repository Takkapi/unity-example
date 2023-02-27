using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] GameManager gm;
    [SerializeField] HealthBar healthBar;

    [Header("Player Stuff")]
    [SerializeField] float speed;
    public int health = 100;

    float horizontal;
    float vertical;

    [Header("Easter Egg")]
    [SerializeField] bool MemeMode = false;
    [SerializeField] KeyCode key;
    [SerializeField] bool isOn;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] GameObject ee;
    [SerializeField] KeyCode joyEasterEggKey;

    void Start()
    {
        healthBar.SetMaxHealth(health);
        body = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        healthBar.SetHealth(health);
        if(health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("DeathSound");
            health = 0;
            gm.gameOver = true;
        }

        if(gm.gameOver == true)
        {
            Destroy(gameObject);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //EASTER EGG
        
        if(MemeMode)
        {
            sr.enabled = false;
            ee.SetActive(true);
            body.freezeRotation = false;
        } else
        {
            if (Input.GetKeyDown(key) || Input.GetKeyDown(joyEasterEggKey))
            {
                if (!isOn)
                {
                    isOn = true;
                    sr.enabled = false;
                    ee.SetActive(true);
                    body.freezeRotation = false;
                }
            }
            if (Input.GetKeyUp(key) || Input.GetKeyUp(joyEasterEggKey))
            {
                if (isOn)
                {
                    isOn = false;
                    sr.enabled = true;
                    ee.SetActive(false);
                    body.freezeRotation = true;
                    transform.rotation = Quaternion.identity;
                }
            }
        }
        
    }

    float Clamp(float var, float min, float max) {
        if(var >= max)
            return var = max;
        else if(var <= min)
            return var = min;
        else
            return var;
    }

    private void FixedUpdate()
    {
        //body.velocity = new Vector2(horizontal * speed, vertical * speed);
        if(gm.preStart == false)
        {
            transform.Translate(new Vector2(horizontal * speed * Time.fixedDeltaTime, vertical * speed * Time.fixedDeltaTime));
        }

        transform.position = new Vector2(Clamp(transform.position.x, -10.14f, 10.14f), Clamp(transform.position.y, -5.45f, 5.45f));

        //transform.position.x = Clamp(transform.position.x, -10.4f, 10.4f);
        // transform.Translate(new Vector2(Clamp(transform.position.x, -10.4f, 10.4f), 0));
        // transform.Translate(new Vector2(0, Clamp(transform.position.y, -5.5f, 5.5f)));
        //transform.position.y = Clamp(transform.position.y, -5.5f, 5.5f);

        // if(transform.position.x >= 10.4f || transform.position.x <= -10.4f)
        // {
        //     transform.Translate(new Vector2(-horizontal * speed * Time.fixedDeltaTime, vertical * speed * Time.fixedDeltaTime));
        // }
        // if(transform.position.y >= 5.5f || transform.position.y <= -5.5f)
        // {
        //     transform.Translate(new Vector2(horizontal * speed * Time.fixedDeltaTime, -vertical * speed * Time.fixedDeltaTime));
        // }

        // if(transform.position.x >= 10.4f && transform.position.y >= 5.5f ) {
        //     transform.Translate(new Vector2(-horizontal * speed * Time.fixedDeltaTime, -vertical * speed * Time.fixedDeltaTime));
        // }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.CompareTag("Basic Enemy") || collision.CompareTag("FastEnemy") || collision.CompareTag("SmartEnemy"))
        {
            health = health - 10;
            FindObjectOfType<AudioManager>().Play("PlayerHit");
        }
    }
}
