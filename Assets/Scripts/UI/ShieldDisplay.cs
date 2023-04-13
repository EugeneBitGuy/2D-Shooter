using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : UIelement
{
    private float shieldTime;
    public Image displayShieldTime;
    private float shieldTimeLeft;


    public void SpawnShield(float shieldTime)
    {
        this.shieldTime = shieldTime;
        shieldTimeLeft = shieldTime;
    }

    private void Update()
    {
        displayShieldTime.fillAmount = shieldTimeLeft / shieldTime;
        shieldTimeLeft -= Time.deltaTime;
    }
}
