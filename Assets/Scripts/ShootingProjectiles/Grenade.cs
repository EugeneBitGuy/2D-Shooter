using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 target;
    public GameObject explosion;
    
    private float lastSqrMag;
    private Vector2 desiredVelocity;

    private bool isDetonating;

    public float projectileSpeed = 10f;

    
    public void SpawnGrenade(Vector2 target)
    {
        this.target = target;

        var directionalVector = (target - (Vector2)transform.position).normalized * projectileSpeed;

        lastSqrMag = Mathf.Infinity;

        desiredVelocity = directionalVector;



    }

    private void Update()
    {
        MoveGrenade();
    }

    private void MoveGrenade()
    {
        var sqrMag = (target - (Vector2)transform.position).sqrMagnitude;
        

        if (sqrMag > lastSqrMag)
        {
            desiredVelocity = Vector2.zero;

            StartCoroutine(Detonating());
            isDetonating = true;
        }

        lastSqrMag = sqrMag;
        
        transform.Rotate(Vector2.right, 1*projectileSpeed, Space.Self);

    }

    private void FixedUpdate()
    {
        rb.velocity = desiredVelocity;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Pickable") && !col.CompareTag("Shield") && !col.CompareTag("DoorTrigger"))
            Explode();
                
    }

    private IEnumerator Detonating()
    {
        if(!isDetonating)
        {
            yield return new WaitForSeconds(3);

            Explode();
        }
    }

    private void Explode()
    {

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
