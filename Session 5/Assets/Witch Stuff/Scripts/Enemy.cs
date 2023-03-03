using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 2;
    private int currHealth;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamageEnemy(int dmg)
    {
        currHealth -= dmg;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "fireball")
        {
            DamageEnemy(1);
        }
    }
}
