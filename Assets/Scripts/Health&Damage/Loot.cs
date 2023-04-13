using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Loot : MonoBehaviour, IDestroyable
{
    public PickableObject[] objectsToLoot;
    public PickableObject keyPrefab;
    public bool isKeyKeeper = false;

    private void Start()
    {
        var health = GetComponent<Health>();
        if (health == null)
        {
            health = gameObject.AddComponent<Health>();
        }
        
        health.currentHealth = (int) Random.Range(1, 15);

        health.teamId = -2;

        health.useLives = false;

        health.invincibilityTime = 0;

        health.isAlwaysInvincible = false;
        
        var rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Static;
    }
    

    void DropLoot()
    {
        var objectToLoot = objectsToLoot[Random.Range(0, objectsToLoot.Length)];

        Instantiate(objectToLoot, transform.position, objectToLoot.transform.rotation, null);
    }

    public void DoBeforeDestroy()
    {
        DropLoot();
    }

    public void MakeKeyKeeper()
    {
        objectsToLoot = new[] {keyPrefab};
        isKeyKeeper = true;
    }
}
