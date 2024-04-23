using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthBuff")]

public class HealthBuff : PowerUpEffect
{
    public int amount;

    public override void Apply(GameObject target)
    {
        if (target.GetComponent<HealthBarManager>().currentHealth == 100)
        {
            target.GetComponent<HealthBarManager>().currentHealth += 0;
        }
        else
        {
            target.GetComponent<HealthBarManager>().currentHealth += amount;
        }
        
    }
}
