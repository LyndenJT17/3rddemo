using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Health healthSystem;
    public void Setup(Health healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealth(), 1);
    }

    private void Update()
    {
        //transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
