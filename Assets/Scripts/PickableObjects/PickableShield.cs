using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableShield : PickableObject
{
    public GameObject shield;
    public float shieldTime;
    
    protected override void OnPick(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;

        var shieldObject = Instantiate(shield, collision.transform);
        shieldObject.GetComponent<ShieldDisplay>().SpawnShield(shieldTime);
        Destroy(shieldObject, shieldTime);
        
        Destroy(gameObject);
    }
}
