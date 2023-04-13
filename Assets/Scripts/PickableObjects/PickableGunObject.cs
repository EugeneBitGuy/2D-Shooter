using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableGunObject : PickableObject
{
    public GameObject gun;
    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        Controller playerController = collision.GetComponent<Controller>();

        if (playerController != null)
        {
            playerController.ReplaceGun(gun);
            Destroy(gameObject);
        }
    }
}
