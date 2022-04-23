using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject ebullet;

    public float fireRate;
    public float nextFire;
    
    public void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;

    }

    // Update is called once pr frame
    public void Update()
    {
        CheckIfTimeToFire();
    }
    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(ebullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
