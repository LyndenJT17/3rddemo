using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float startHealth;
    public float hp;
    public JosHealthBar healthbar;
    void Start()
    {
        hp = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthbar.UpdateHealth(hp/startHealth);
        if(hp <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        
        Destroy(gameObject);
    }
}
