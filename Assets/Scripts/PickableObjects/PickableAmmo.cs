using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableAmmo : PickableObject
{
    public int ammoAmount;
    
    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        MultiGunShootingController playerGunController = collision.GetComponent<Controller>().currentGun.GetComponent<MultiGunShootingController>();

        if (playerGunController != null)
        {
            playerGunController.RestockAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}
