using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float health = 100f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        // lose HP and play hurt animation
        health -= damage;
        anim.SetTrigger("Hurt");
    }
    
    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetBool("Dead", true);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
