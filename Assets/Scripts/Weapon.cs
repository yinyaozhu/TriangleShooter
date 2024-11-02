using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    private string name;
    private float damage;
    private float bulletSpeed;

    public Weapon(string _name, float _damage, float _bulletSpeed) { 
        name = _name;
        damage = _damage;
        bulletSpeed = _bulletSpeed;
    }

    public Weapon(){}

    public void Shoot(Bullet _bullet, PlayableObject _player, string _targetTag, float _timeToDie = 5f, string _fireTag = "Player") {
        Bullet tempBullet;
        //if (_targetTag != "Player")
        //{
            tempBullet = GameObject.Instantiate(_bullet, _player.transform.position, _player.transform.rotation);
            tempBullet.SetBullet(damage, _targetTag, bulletSpeed, _fireTag);
            GameObject.Destroy(tempBullet.gameObject, _timeToDie);
        //}
        //else { 
        //}
    }

    public float GetDamage()
    {
        return damage;
    }

}
