using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : Pickup, IDamageable
{
    public override void Onpicked()
    {
        base.Onpicked();
        var player = GameManager.GetInstance().GetPlayer();

        player.superPower.ChargeSuperPower("Nuke");
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
