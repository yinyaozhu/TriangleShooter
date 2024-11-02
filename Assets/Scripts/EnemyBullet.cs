using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBullet: Bullet
{
    private Transform target;
    private Vector2 direction;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            //Debug.Log("found player;");

            target = playerObject.transform;
            direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            //Debug.Log($"Target Position: {target.position}, Bullet Position: {transform.position}, Calculated Direction: {direction}");
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Destroy(this);

        }
    }


    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (target != null) {
            //we don't follow the player for gun enemy, but can design for another enemy
            if (fireTag == "GunEnemy") {
                transform.Translate(direction * speed * Time.deltaTime);
            }
        }
        
    }
    //https://www.youtube.com/watch?v=ouzkNDIXg3I

    public override void Damage(IDamageable damageable)
    {
        if (damageable != null)
        {
            damageable.GetDamage(damage);// which is player        
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        IDamageable damageable = collision.GetComponent<IDamageable>();
        Damage(damageable);
    }


}
