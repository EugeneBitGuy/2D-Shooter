using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowingContorller : MonoBehaviour
{
    public int maximumGrenadeAmount = 10;
    public int currentGrenadeAmount = 5;

    public GameObject grenadePrefab;
    public bool isPlayerControlled;

    private InputManager inputManager;
    public GameObject preExplodeAreaPrefab;

    private void Start()
    {
        SetupInput();
    }
    private void Update()
    {
        ProcessInput();
    }
    
    void ProcessInput()
    {
        if (isPlayerControlled)
        {

            
            ShowPreAttack(inputManager.grenadeThrowingHeld && currentGrenadeAmount>0);
            
            if (inputManager.grenadeThrowingPressed)
            {
                Throw();
            }
        }   
    }
    
    void SetupInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager == null)
            {
                inputManager = InputManager.instance;
            }
            if (inputManager == null)
            {
                Debug.LogError("Player Shooting Controller can not find an InputManager in the scene, there needs to be one in the " +
                               "scene for it to run");
            }
        }
    }

    private void ShowPreAttack(bool canShow)
    {
        
            preExplodeAreaPrefab.SetActive(canShow);
            if(canShow)
                preExplodeAreaPrefab.transform.position =
                (Vector2)Camera.main.ScreenToWorldPoint(new Vector2(inputManager.horizontalLookAxis,
                    inputManager.verticalLookAxis));
    }
    
    private void Throw()
    {
        if (currentGrenadeAmount > 0)
        {

            var target = new Vector2(inputManager.horizontalLookAxis, inputManager.verticalLookAxis);
            
            var grenadeObject = Instantiate(grenadePrefab, transform.position, transform.rotation);
            
            grenadeObject.GetComponent<Grenade>().SpawnGrenade((Vector2)Camera.main.ScreenToWorldPoint(target));

            currentGrenadeAmount--;
        }
    }
}
