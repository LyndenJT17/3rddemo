using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        Health healthSystem = new Health(100);
        healthBar.Setup(healthSystem);

    }

}
