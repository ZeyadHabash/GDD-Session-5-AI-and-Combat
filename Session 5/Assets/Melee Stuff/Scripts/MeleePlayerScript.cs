using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayerScript : MonoBehaviour
{
    public float speed;
    bool isAttacking;
    float moveInput;
    Animator anim;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if(!isAttacking)
                Attack();
        }
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    public void Attack(){
        isAttacking = true;
        anim.SetTrigger("Attack");

        /*
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyScript>().TakeDamage(damage);
        }*/
    }

    public void AttackEnd(){
        isAttacking = false;
    }
    
    
}
