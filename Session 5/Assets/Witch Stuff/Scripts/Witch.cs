using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Witch : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;

    public Animator anim;

    public int maxHealth = 3;
    private int currHealth;
    public Animator deathFade;

    public GameObject fireball;
    // time before player can attack again
    public float attackTime = 0.4f;
    private float attackTimeCounter;
    private bool isAttacking;

    private bool invincible;

    void Start()
    {
        currHealth = maxHealth;
        anim.SetBool("DownIdle", true);
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(hori * speed, vert * speed);

        // edited these to work with wasd too since I had to use mouse for attacking
        // walking animations
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ResetAnimation();
            anim.SetBool("Down", true);
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            ResetAnimation();
            anim.SetBool("DownIdle", true);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ResetAnimation();
            anim.SetBool("Left", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            ResetAnimation();
            anim.SetBool("LeftIdle", true);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ResetAnimation();
            anim.SetBool("Right", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            ResetAnimation();
            anim.SetBool("RightIdle", true);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            ResetAnimation();
            anim.SetBool("Up", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            ResetAnimation();
            anim.SetBool("UpIdle", true);
        }

        
        // projectile attacking
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttacking = true;
                anim.SetBool("Attack", true);
                attackTimeCounter = attackTime;
                
                Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = target - myPosition;
            
                
                // normalizes magnitude of vector (sets it to 1)
                direction.Normalize();

                // idk what this does its quaternion stuff that I don't understand
                Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90f);
                GameObject projectile = (GameObject)Instantiate(fireball, myPosition, rotation);
            }
        }
        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            anim.SetBool("Attack", false);
            isAttacking = false;
        }

    }

    public IEnumerator DamagePlayer(int dmg)
    {
        currHealth -= dmg;

        if (currHealth <= 0)
        {
            currHealth = 0;
            StartCoroutine(RestartLevel());
        }
        // blink and go invincible
        invincible = true;
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invincible = false;
    }
    IEnumerator RestartLevel()
    {
        rb.velocity = Vector2.zero;
        ResetAnimation();
        anim.SetBool("Death", true);
        enabled = false;
        deathFade.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public int GetCurrentHP()
    {
        return currHealth;
    }

    void ResetAnimation(){
        anim.SetBool("Up", false);
        anim.SetBool("UpIdle", false);
        anim.SetBool("Down", false);
        anim.SetBool("DownIdle", false);
        anim.SetBool("Left", false);
        anim.SetBool("LeftIdle", false);
        anim.SetBool("Right", false);
        anim.SetBool("RightIdle", false);
    }

    public Vector2 DirectionFacing(){
        if(anim.GetBool("Up") || anim.GetBool("UpIdle"))
            return new Vector2(transform.position.x, transform.position.y + 100);
        else if(anim.GetBool("Down") || anim.GetBool("DownIdle"))
            return new Vector2(transform.position.x, transform.position.y-100);
        else if(anim.GetBool("Right") || anim.GetBool("RightIdle"))
            return new Vector2(transform.position.x + 100, transform.position.y);
        else if(anim.GetBool("Left") || anim.GetBool("LeftIdle"))
            return new Vector2(transform.position.x - 100, transform.position.y);
        return new Vector2(transform.position.x, transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !invincible)
        {
            StartCoroutine(DamagePlayer(1));
        }
    }
}
