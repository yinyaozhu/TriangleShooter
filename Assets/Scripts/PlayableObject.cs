using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//inherited
public abstract class PlayableObject : MonoBehaviour, IDamageable
{
    public Health health;
    public Weapon weapon;

    //public Health health = new Health();

    public abstract void Move(Vector2 direction, Vector2 target);

    //methods for polymorphism
    public virtual void Move(Vector2 direction) { }

    public virtual void Move(float speed) { }

    public abstract void Shoot();

    public abstract void Attack(float interval);

    public abstract void Die();

    public abstract void GetDamage(float damage);

}
