using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    bool isAttacking;
    float moveInput;
    [SerializeField] float damage;
    Animator anim;
    Rigidbody2D rb;
    Collider2D hitbox;
    ContactFilter2D enemyFilter;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = transform.GetChild(0).GetComponent<Collider2D>();
        speed = 5f;
        damage = 30f;
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemies"));
        enemyFilter.useLayerMask = true;
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
            if(!isAttacking)
                anim.SetTrigger("Attack");
        }
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    public void Attack(){
        hitbox.enabled = true;
        Collider2D[] enemiesToDamage = new Collider2D[10];
        Physics2D.OverlapCollider(hitbox, enemyFilter, enemiesToDamage);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            
            if (enemy != null)
            {
                Debug.Log("enemy hit");
                enemy.GetComponent<EnemyScript>().TakeDamage(damage);
            }
        }
        hitbox.enabled = false;
    }


    public void AttackStart(){
        isAttacking = true;
    }

    public void AttackEnd(){
        isAttacking = false;
    }
}
