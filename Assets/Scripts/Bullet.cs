using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float speed;
   
    public string targetTag;
    public string fireTag;

    public void SetBullet(float _damage, string _compareTag, float _speed = 15, string _fireTag = "Player")
    {
        this.damage = _damage;
        this.targetTag = _compareTag;
        this.speed = _speed;
        this.fireTag = _fireTag;
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move() {
        //google why vector2.up later
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public virtual void Damage(IDamageable damageable) {
        if (damageable != null) {
            damageable.GetDamage(damage);
            //add score here
            GameManager.GetInstance().scoreManager.InCrementScore();
        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name);

        if (!collision.gameObject.CompareTag(targetTag)) {
            return;
        }

        //Debug.Log(targetTag);
        IDamageable damageable = collision.GetComponent<IDamageable>();
        Damage(damageable);
    }
}
