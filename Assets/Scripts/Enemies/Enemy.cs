using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which controls enemy behaviour
/// </summary>
public class Enemy : MonoBehaviour, IDestroyable
{
    [Header("Settings")]
    [Tooltip("The speed at which the enemy moves.")]
    public float moveSpeed = 5.0f;
    [Tooltip("The score value for defeating this enemy")]
    public int scoreValue = 5;

    [Header("Following Settings")]
    [Tooltip("The transform of the object that this enemy should follow.")]
    public Transform followTarget = null;
    [Tooltip("The distance at which the enemy begins following the follow target.")]
    public float followRange = 10.0f;

    private Rigidbody2D rb;
    public Animator animator;


    /// <summary>
    /// Description:
    /// Standard Unity function called after update every frame
    /// Inputs: 
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void FixedUpdate()
    {
        HandleBehaviour();       
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first call to Update
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    private void Start()
    {
        if (followTarget == null)
        {
            if (GameManager.instance != null && GameManager.instance.player != null)
            {
                followTarget = GameManager.instance.player.transform;
            }
        }

        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Description:
    /// Handles moving and shooting in accordance with the enemy's set behaviour
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void HandleBehaviour()
    {

        if (followTarget != null && (followTarget.position - transform.position).magnitude < followRange)
        {
            MoveEnemy();
        }

    }


    public void DoBeforeDestroy()
    {
        AddToScore();
        IncrementEnemiesDefeated();
    }

    /// <summary>
    /// Description:
    /// Adds to the game manager's score the score associated with this enemy if one exists
    /// Input:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    private void AddToScore()
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsOver)
        {
            GameManager.AddScore(scoreValue);
        }
    }

    /// <summary>
    /// Description:
    /// Increments the game manager's number of defeated enemies
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    private void IncrementEnemiesDefeated()
    {
        if (GameManager.instance != null && !GameManager.instance.gameIsOver)
        {
            GameManager.instance.IncrementEnemiesDefeated();
        }       
    }

    /// <summary>
    /// Description:
    /// Moves the enemy and rotates it according to it's movement mode
    /// Inputs: none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void MoveEnemy()
    {
        FollowPlayerRotation();
        FollowPlayerMovement();
    }





    private void FollowPlayerMovement()
    {
        Vector3 moveDirection = (followTarget.position - transform.position).normalized;
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        
        rb.MovePosition(rb.position + (Vector2)movement);
        
        animator.SetBool("isWalk", movement != Vector3.zero);
    }

    /// <summary>
    /// Description
    /// The desired rotation of follow movement mode
    /// Inputs: 
    /// none
    /// Returns: 
    /// Quaternion
    /// </summary>
    /// <returns>Quaternion: The rotation to be used in follow movement mode.</returns>
    private void FollowPlayerRotation()
    {
        float angle = Vector3.SignedAngle(Vector3.up, (followTarget.position - transform.position).normalized, Vector3.forward);
        Quaternion rotationToTarget = Quaternion.Euler(0, 0, angle);
        
        rb.MoveRotation(rotationToTarget);
    }



}
