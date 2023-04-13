using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;

    private bool isOpenedUp;

    public bool needKey = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(needKey && !GameManager.instance.HasKey)
            return;
        
        if(col.CompareTag("Player"))
            OpenDoor(col.transform);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(needKey && !GameManager.instance.HasKey)
            return;
        
        if(other.CompareTag("Player"))
            CloseDoor(other.transform);
    }
    
    void OpenDoor(Transform colliderTransform)
    {
        var compareVector = transform.right;

        var doorPosition = transform.position;
        var doorPositionOnVector = new Vector2(doorPosition.x * compareVector.x * compareVector.x,
            doorPosition.y * compareVector.y * compareVector.y);

        var playerPosition = colliderTransform.position;
        var playerPositionOnVector = new Vector2(playerPosition.x * compareVector.x * compareVector.x,
            playerPosition.y * compareVector.y * compareVector.y);

        isOpenedUp = (doorPositionOnVector - playerPositionOnVector).normalized == (Vector2)compareVector;
        
        
        
        doorAnimator.SetTrigger($"Open{(isOpenedUp ?  "Up" : "Down")}");
        
        
        
    }


    void CloseDoor(Transform colliderTransform)
    {
        doorAnimator.SetTrigger($"Close{(isOpenedUp ?  "Down" : "Up")}");
    }
    
    
}
