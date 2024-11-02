using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float attackRange;// attachRange didn't really work, why
    [SerializeField] private float attackTime;

    private float timer = 0f;
    private float setSpeed = 0f;

    private Weapon meleeWeapon = new Weapon("Melee", 1f, 0f);

    protected override void Start() {
        base.Start();
        weapon = meleeWeapon;
        health = new Health(1,0,1);
        setSpeed = speed;
    }

    //target is default to player in enemy base class
    protected override void Update() { 
        base.Update();
        if (target == null) return;

        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0f;
            Attack(attackTime);
        }
        else {
            speed = setSpeed;
        }
    }
    public override void Attack(float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            if (target == null)
            {
                Debug.LogWarning("Target is null in Attack method");
                return;
            }
            var damageable = target.GetComponent<IDamageable>();
            if (damageable == null)
            {
                Debug.LogWarning("Target does not have IDamageable component");
                return;
            }
            if (weapon == null)
            {
                Debug.LogWarning("Weapon is null in Attack method");
                return;
            }
            // target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage()); // -> change to below
            damageable.GetDamage(this.weapon.GetDamage());
            //Debug.Log(weapon.GetDamage());
        }
    }

    public void SetMeleeEnemy(float _attackRange, float _attackTime)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;
    }

    //public SpriteRenderer sprite;

    //if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        StartCoroutine(FlashRed());
    //gameManager.ouch();
    //    }

    //public IEnumerator FlashRed()
    //{
    //    sprite.color = Color.red;
    //    yield return new WaitForSeconds(0.5f);
    //    sprite.color = Color.white;
    //}

}
