using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGunPickup : Pickup, IDamageable
{
    public override void Onpicked()
    {
        base.Onpicked();
        var player = GameManager.GetInstance().GetPlayer();

        player.superStatus.ChargeSuperStatus();
    }


    public void GetDamage(float damage)
    {
        Onpicked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Onpicked();
        }
    }
}
