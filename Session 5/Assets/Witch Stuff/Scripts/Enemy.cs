using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 2;
    private int currHealth;

    GameObject player;
    Vector3 target;
    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;
        
        agent.SetDestination(target);

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
        agent.SetDestination(target);
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
