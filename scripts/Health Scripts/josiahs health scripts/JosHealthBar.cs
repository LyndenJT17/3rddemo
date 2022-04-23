using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JosHealthBar : MonoBehaviour
{
    public Image healthbar;
    // Start is called before the first frame update
    public void UpdateHealth(float fraction)
    {
        healthbar.fillAmount = fraction;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
