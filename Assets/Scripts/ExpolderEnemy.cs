using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExpolderEnemy : Enemy
{
    [SerializeField] private float attackRange;// attachRange didn't really work, why
    [SerializeField] private float attackTime;

    private float timer = 0f;
    private float setSpeed = 0f;

    public SpriteRenderer sprite;

    private Weapon gunWeapon = new Weapon("Gun", 25f, 0f);

    protected override void Start()
    {
        base.Start();
        weapon = gunWeapon;
        health = new Health(3, 0, 3);//start with health 3
        setSpeed = speed;

        sprite = GetComponent<SpriteRenderer>();
    }

    //target is default to player in enemy base class
    protected override void Update()
    {
        base.Update();
        if (target == null) return;

        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0f;
            Attack(attackTime);// we re-write the attack method as explode
        }
        else
        {
            speed = setSpeed;
        }
    }
    public override void Attack(float interval)
    {
        //StartCoroutine(FlashRed());
        sprite.color = Color.red;

        // we don't really need the interval, we just let it explode
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
        damageable.GetDamage(this.weapon.GetDamage());
        Die();
        Debug.Log(weapon.GetDamage());
    }

    public void SetExpolderEnemy(float _attackRange, float _attackTime)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;// not really using here
    }

}
