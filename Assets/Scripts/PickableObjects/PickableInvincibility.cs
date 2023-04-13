using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableInvincibility : PickableObject
{
    public int invincibilityTime;
    
    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        Health playerHealth = collision.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.BecomeInvincible(invincibilityTime);
            Destroy(gameObject);
        }
    }
}
