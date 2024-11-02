using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player: PlayableObject
{
    [SerializeField] private float speed; // player speed

    [SerializeField] private float weaponDamage = 1f;
    [SerializeField] private float bulletSpeed = 150f;
    [SerializeField] private Bullet bulletPrefab;

    public Action OnDeath;

    private Camera mainCamera;
    private Rigidbody2D playerRB;

    public SuperPower superPower;
    public SuperStatus superStatus;

    public bool autoShooting;

    private void Awake() { 
        mainCamera = Camera.main;
        playerRB = GetComponent<Rigidbody2D>();
        health = new Health(100f, 0.5f, 100f);

        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);

        superPower = new SuperPower();
        superStatus = new SuperStatus();
        //enemyTraceback = new Stack<GameObject>();

        autoShooting = false;
    }

    private void Update()
    {
        health.RegenHealth();
    }

    public override void Move(Vector2 direction, Vector2 target) {
        playerRB.velocity = direction * speed * 100 * Time.deltaTime;

        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(transform.position);
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = (Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 85f);// for a small delay
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void Attack(float interval)
    {
        //throw new System.NotImplementedException();
    }

    public override void Shoot() {
        //Debug.Log($"Shoots bullet at {direction} with a speed of {speed}");
        weapon.Shoot(bulletPrefab, this, "Enemy");
    }

    //when user use Nuke
    public void SuperPower() {
        string temp = superPower.UseSuperPower();
        if (temp == "Nuke")
        {
            KillEverybody();
        } // if temp is null, do nothing
    }

    private void KillEverybody() {
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy)))
        {
            //Destroy(item.gameObject);
            item.GetComponent<Enemy>().Die();
        }
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup)))
        {
            Destroy(item.gameObject);
        }
    }

    public override void Die() {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public override void GetDamage(float damage)
    {
        //base.GetDamage(damage);
        health.DeductHealth(damage);

        if (health.GetHealth() <= 0) { 
            Die();
        }
    }


}
