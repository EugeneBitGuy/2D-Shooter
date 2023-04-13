using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPick(collision);
        GameManager.UpdateUIElements();
    }
    
    protected virtual void OnPick(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
    }
}
