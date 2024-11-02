using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IDamageable
{
    [SerializeField] private float healthMin;
    [SerializeField] private float healthMax;

    public override void Onpicked()
    {
        base.Onpicked();

        float health = Random.Range(healthMin, healthMax);

        var player = GameManager.GetInstance().GetPlayer();

        player.health.AddHealth(health);

    }

    public void GetDamage(float damage) { 
        Onpicked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Onpicked();
        }
    }
}
