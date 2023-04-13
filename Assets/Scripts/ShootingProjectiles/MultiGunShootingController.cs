using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MultiGunShootingController : ShootingController
{
    public Transform[] guns;

    private int lastGunNumber;

    public bool isLimitedAmmoAmount = true;

    public int maximumAmmo = 1000;
    public int currentAmmo = 1000;

    
    

    public override void Fire()
    {
        if(isLimitedAmmoAmount && currentAmmo <= 0)
            return;
        /*foreach (var gun1 in guns)
        {
            if (Time.timeScale > 0)
            {
                var point = new Vector2(inputManager.horizontalLookAxis, inputManager.verticalLookAxis);
                // Rotate the player to look at the mouse.
                Vector2 lookDirection = Camera.main.ScreenToWorldPoint(point) - gun1.position;

                gun1.up = lookDirection;


            }
        }*/
        
        var gun = guns[lastGunNumber % guns.Length];
        if (Time.timeSinceLevelLoad - lastFired > fireRate)
        {
            // Launches a projectile
            SpawnProjectileAtPoint(gun);

            if (fireEffect != null)
            {
                Instantiate(fireEffect, gun.position, gun.rotation, projectileHolder);
            }

            // Restart the cooldown
            lastFired = Time.timeSinceLevelLoad;
            lastGunNumber++;

            if (isLimitedAmmoAmount)
                currentAmmo--;
            
            GameManager.UpdateUIElements();
        }
    }

    public void RestockAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maximumAmmo)
            currentAmmo = maximumAmmo;
    }

    public void SpawnProjectileAtPoint(Transform point)
    {
        if (projectilePrefab != null)
        {
            // Create the projectile
            GameObject projectileGameObject = Instantiate(projectilePrefab, point.position, point.rotation, null);

            // Account for spread
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += Random.Range(-projectileSpread, projectileSpread);
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);

            // Keep the heirarchy organized
            if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }
        }
    }
    
}
