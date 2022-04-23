using System.Collections.Generic;
using UnityEngine;
using TMPro;
//CommentPush
/// Thanks for downloading my projectile gun script! :D
/// Feel free to use it in any project you like!
/// 
/// The code is fully commented but if you still have any questions
/// don't hesitate to write a yt comment
/// or use the #coding-problems channel of my discord server
/// 
/// Dave
public class ProjectileGunTutorial : MonoBehaviour
{

    public PlayerController playerController;

    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float attackSpeed, spread;
    public bool allowButtonHold;
    public float attackSpeedMulti = 1;

    int bulletsShot;

    //bools
    bool shooting, readyToShoot;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;


    //bug fixing :D
    public bool allowInvoke = true;

    private void Awake()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        

    }

    public void gunUpgrade()
    {
        int numAttackSpeed = 1;
        foreach (var item in playerController.items)
        {
            
            if (item == "attackSpeedBasic")
            {
                numAttackSpeed += 1;
            }
            
        }
        attackSpeedMulti += numAttackSpeed * .05f;
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if (readyToShoot && shooting)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = ray.GetPoint(75) - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = ObjectPool.SharedInstance.GetPooledObject();
        if (currentBullet != null)
        {
            currentBullet.transform.position = attackPoint.transform.position;
            currentBullet.transform.rotation = gameObject.transform.rotation;
            currentBullet.SetActive(true);
            currentBullet.transform.forward = directionWithSpread.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        }

        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your attackSpeed
        if (allowInvoke)
        {
            Invoke("ResetShot", attackSpeed / attackSpeedMulti);
            allowInvoke = false;
        }

    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

}
