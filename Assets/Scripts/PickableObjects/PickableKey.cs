using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableKey: PickableObject
{
    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        GameManager.instance.PickUpKey();
        Destroy(gameObject);
    }
}
