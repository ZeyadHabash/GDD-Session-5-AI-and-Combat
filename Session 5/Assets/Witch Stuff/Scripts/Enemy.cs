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
    Vector3 originalLocation;

    public float visionDistance = 10f;
    LayerMask playerLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        originalLocation = transform.position;
        target = originalLocation;
        
        agent.SetDestination(target);

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        playerLayer = LayerMask.GetMask("Targets");

        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
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

    void SetTarget(){
        // Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, visionDistance, playerLayer);
        // if(targetCollider != null)
        //     target = targetCollider.gameObject.transform.position;

        float number_of_rays = 40;
        float angle = 360 / number_of_rays;
        float cast_angle = 0;

        for (int i = 0; i < number_of_rays; i++)
        {
            var dir = Quaternion.Euler(0, 0, cast_angle) * transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, visionDistance, playerLayer);
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                target = player.transform.position;
                return;
            }
            cast_angle += angle;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Gizmos.DrawWireSphere(transform.position, visionDistance);
    }
}
