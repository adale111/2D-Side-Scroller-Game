using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate; //fire rate in seconds
    private float nextFire = 0.0f;
    public int damage;

    public GameObject plasmaGun;
    public GameObject plasmaGunBulletPrefab;
    private float plasmaGunNextFire = 0.0f;
    public float plasmaGunFirerate;
    public Transform plasmaGunSpawn;
    public int plasmaGunDamage;

    public GameObject shield;
    private float shieldRechargeTime;
    private float rechargeStartTime;
    public bool shieldCharged;
    private bool shieldEquipt;
    public bool shieldHit;

    public GameObject rocketLauncher;
    public GameObject rocketLauncherRocketPrefab;
    private float rocketLauncherNextFire = 0.0f;
    public float rocketLauncherFirerate;
    public Transform rocketLauncherSpawn;
    public int rocketLauncherDamage;

    


    void Start()
    {
        fireRate = 0.5f;
        damage = 5;

        plasmaGunFirerate = 0.9f;
        plasmaGunDamage = 12;
        plasmaGun.SetActive(false);

        shield.SetActive(false);
        
         shieldRechargeTime = 10f;
        shieldCharged = false;
        shieldEquipt = false;
        shieldHit = false;

        rocketLauncher.SetActive(false);
        rocketLauncherDamage = 30;
        rocketLauncherFirerate = 4.0f;
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Fire();
        }

        if (plasmaGun.activeSelf)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > plasmaGunNextFire)
            {
                plasmaGunNextFire = Time.time + plasmaGunFirerate;
                FirePlasmaGun();
            }
        }

        if (rocketLauncher.activeSelf)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > rocketLauncherNextFire)
            {
                rocketLauncherNextFire = Time.time + rocketLauncherFirerate;
                FireRocketLauncher();
            }
        }


        
        LevelUpPlayer levelUpPlayer = GetComponent<LevelUpPlayer>();

        if (levelUpPlayer.hasShield == true && shieldEquipt == false)
        {
            EquiptShield();

        }
          
        
        if (shieldEquipt == true && shieldCharged == true)
        {

                shield.SetActive(true);
        }
           
        else if (shieldEquipt == true && shieldCharged == false & shieldHit == true)
            {
                shield.SetActive(false);
                StartCoroutine(RechargeShield());
                shieldHit = false;
            }
        

        if (shieldEquipt == true && shieldCharged == true)
        {
            shield.SetActive(true);
        }
        

    }

    void Fire()
    {
        
        Vector3 bulletSpawnPosition = bulletSpawn.position;
        bulletSpawnPosition.x += 0.5f; // shift the bullet spawn position slightly to the right

      

        //Spawns bullet with position adjusted as above
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawnPosition, bulletSpawn.rotation);

      
        // shoots bullet to the right
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 6;




        Destroy(bullet, 6.0f);
    }

    void FirePlasmaGun()
    {
        Vector3 plasmaGunBulletSpawnPosition = plasmaGunSpawn.position;
        plasmaGunBulletSpawnPosition.x += 0.5f;
        

        var plasmaGunBullet = (GameObject)Instantiate(plasmaGunBulletPrefab, plasmaGunBulletSpawnPosition, plasmaGunSpawn.rotation);

        plasmaGunBullet.GetComponent<Rigidbody2D>().velocity = plasmaGunBullet.transform.right * 4;

        Destroy(plasmaGunBullet, 12.0f);
    }
    
    void EquiptShield()
    {
        shield.SetActive(true);
        shieldEquipt = true;
        shieldCharged = true;
            
    }

    IEnumerator RechargeShield()
    {
        yield return new WaitForSeconds(shieldRechargeTime);
        shieldCharged = true;
    }


    void FireRocketLauncher()
    {
        Vector3 rocketLauncherRocketSpawnPosition = rocketLauncherSpawn.position;
        rocketLauncherRocketSpawnPosition.x += 0.5f;

        var rocketLauncherRocket = (GameObject)Instantiate(rocketLauncherRocketPrefab, rocketLauncherRocketSpawnPosition, rocketLauncherSpawn.rotation);

        rocketLauncherRocket.GetComponent<Rigidbody2D>().velocity = rocketLauncherRocket.transform.right * 3;
    }
}

