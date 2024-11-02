using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class GunEnemy : Enemy
{
    [SerializeField] private float attackRange;// attachRange didn't really work, why
    [SerializeField] private float attackTime;

    [SerializeField] private Bullet bulletPrefab;
    
    private float timer = 0f;
    private float setSpeed = 0f;

    public SpriteRenderer sprite;

    private Weapon gunWeapon = new Weapon("Gun", 10f, 20f);

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
            Attack(attackTime);// we re-write the attack method to shoot
        }
        else
        {
            speed = setSpeed;
        }
    }
    public override void Attack(float interval)
    {
        // we don't really use interval here cuz the gun enemy does not disppear
        // but we can pass to the enemy bullet and let it delete itself

        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            StartCoroutine(FlashRed());

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
            Shoot();
            //Debug.Log(weapon.GetDamage());
        }
    }

    public override void Shoot()
    {
        //we default to shoot towards player
        weapon.Shoot(bulletPrefab, this, "Player", 10, "GunEnemy");
    }

    public void SetGunEnemy(float _attackRange, float _attackTime)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;// not really using here
    }
    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }
}
