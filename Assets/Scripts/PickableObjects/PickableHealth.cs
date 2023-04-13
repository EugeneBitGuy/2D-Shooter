using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHealth : PickableObject
{
    public int healthAmount = 10;

    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        Health playerHealth = collision.gameObject.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.ReceiveHealing(healthAmount);
            Destroy(gameObject);
        }
    }
}
