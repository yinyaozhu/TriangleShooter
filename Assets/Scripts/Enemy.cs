using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: PlayableObject
{
    private string name_not_same_in_playableObject;

    // Health is now being constructed from playable object instead
    private EnemyType enemyType;
    protected Transform target;
    [SerializeField] protected float speed;

    protected virtual void Start() {
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else {
            Destroy(this);
            //return;
        }
    }

    protected virtual void Update() {
        if (target != null)
        {
            Move(target.position);
        }
        else {
            Move(speed);
        }
    }
    public void SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
       //nonthing yet
    }

    public override void Move(Vector2 direction)
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Move(float speed)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Shoot()
    {
        //we default to shoot towards player

    }

    public override void Attack(float interval)
    {

    }

    public override void Die()
    {
        // in notify death, we generate a pickup in where enemy died
        GameManager.GetInstance().NotifyDeath(this);
        Destroy(gameObject);
    }

    //public void Example() { 
    //    SetEnemyType(EnemyType.Melee);
    //}

    public override void GetDamage(float damage) { 
        health.DeductHealth(damage);
        if (health.GetHealth() <= 0) { Die(); }  
    }

}
