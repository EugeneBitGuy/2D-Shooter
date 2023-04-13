using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : UIelement
{
    public Slider displaySlider = null;

    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Description:
    /// Changes the high score display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public void DisplayPlayerAmmo()
    {
        if (displaySlider != null)
        {
            var playerController = GameManager.instance.player.GetComponent<Controller>();

            if (playerController == null || playerController.currentGun == null)
            {
                group.alpha = 0f;
                return;
            }

            var playerGun = playerController.currentGun.GetComponent<MultiGunShootingController>();

            if (playerGun == null)
            {
                group.alpha = 0f;
                return;
            }
            
            if(!playerGun.isLimitedAmmoAmount)
            {
                group.alpha = 0f;
                return;
            }

            var ammoValue = (float) playerGun.currentAmmo / playerGun.maximumAmmo;
            
            group.alpha = 1f;

            displaySlider.value = ammoValue;
        }
    }


    private void SetActiveChildren(bool isActive)
    {
        var children = GetComponentsInChildren<RectTransform>(true);
        foreach (var child in children)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    /// <summary>
    /// Description:
    /// Overrides the virtual function UpdateUI() of the UIelement class and uses the DisplayHighScore function to update
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayPlayerAmmo();
    }
}
