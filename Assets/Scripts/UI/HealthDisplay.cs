using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : UIelement
{
    public Slider displaySlider = null;


    /// <summary>
    /// Description:
    /// Changes the high score display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public void DisplayPlayerHealth()
    {
        if (displaySlider != null)
        {
            var playerHealth = GameManager.instance.player.GetComponent<Health>();
            if(playerHealth != null)
                displaySlider.value =  (float) playerHealth.currentHealth / playerHealth.maximumHealth;
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
        DisplayPlayerHealth();
    }
}
