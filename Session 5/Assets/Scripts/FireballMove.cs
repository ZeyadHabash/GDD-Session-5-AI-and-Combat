using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMove : MonoBehaviour
{
    // time before projectiles expires
    public float expiryTime = 3;
    // speed of projectile
    public float speed = 10;
    private Rigidbody2D rb;
    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            // shoots towards mouse
            Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Input.mousePosition - sp).normalized;

            // shoots in direction of witch
            //Vector2 direction = FindObjectOfType<Witch>().DirectionFacing().normalized; 
            rb.velocity = direction * speed;
            isMoving = true;
        }
        if (expiryTime > 0)
            expiryTime -= Time.deltaTime;
        if (expiryTime <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag != "Player" && obj.gameObject.tag != "fireball")
            Destroy(this.gameObject);
    }
}
