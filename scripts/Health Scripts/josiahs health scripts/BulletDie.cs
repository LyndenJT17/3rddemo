using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
    public float dieTime, damage = 50f;
    public GameObject diePEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }
    
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
        GameObject collisionGameObject = other.gameObject;

        if (collisionGameObject.name != "Player")
        {
            if (collisionGameObject.GetComponent<HealthScript>() != null)
            {
                collisionGameObject.GetComponent<HealthScript>().TakeDamage(damage);
            }
            Die();
        }
    }
    // Update is called once per frame
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }
    void Die()
    {
        if(diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
