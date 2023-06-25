using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponRoot;

    public Transform weaponMuzzle;

    public GameObject weaponMuzzlePrefab;

    public ProjectileBase projectilePrefab;

    public AudioSource weaponSound;
    public AudioClip shootSound;

   
    public float shootDelay = 0.1f;//¿ªÇ¹ÆµÂÊ

    public float energySpeed = 1f;
    public int maxEnergy = 20;
    public float oneShoot = 1f;
    private float currentEnergy = 0;
    private float energyRecover = 0;

    public int choosedWeaponShoot;

    private float lastShoot = Mathf.NegativeInfinity;

    public Vector3 muzzleWorldVelocity { get;private set; }

    public bool isWeaponActive { get; private set; }
    public GameObject owner { get; set; }
    public GameObject sourcePrefab { get; set; }



    private void Awake()
    {
        weaponSound = GetComponent<AudioSource>();
        weaponSound.volume = 0.1f;
    }
    public void ShowWeapon(bool show)
    {
        weaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    public bool HandleShootInput(bool input)
    {
        if (input)
        {
            if(EnergyLimit())
            {
                return TestShoot();
            }            
        }
        return false;
    }

    private void SGShoot()
    {
        var bulletNum = Random.Range(3, 6);
        //Debug.Log(bulletNum);
        switch (bulletNum)
        {
            case 3:
                {
                    //float a[] = new float[9];
                    float a1 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a2 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a3 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a4 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a5 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a6 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a7 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a8 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a9 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    Quaternion q1 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a1, a2, a3));
                    Quaternion q2 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a4, a5, a6));
                    Quaternion q3 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a7, a8, a9));
                    ProjectileBase newProjectile1 = Instantiate(projectilePrefab, weaponMuzzle.position, q1);
                    ProjectileBase newProjectile2 = Instantiate(projectilePrefab, weaponMuzzle.position, q2);
                    ProjectileBase newProjectile3 = Instantiate(projectilePrefab, weaponMuzzle.position, q3);
                    newProjectile1.Shoot(this);
                    newProjectile2.Shoot(this);
                    newProjectile3.Shoot(this);
                    break;
                }
            case 4:
                {
                    float a1 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a2 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a3 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a4 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a5 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a6 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a7 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a8 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a9 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a10 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a11 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a12 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    Quaternion q1 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a1, a2, a3));
                    Quaternion q2 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a4, a5, a6));
                    Quaternion q3 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a7, a8, a9));
                    Quaternion q4 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a10, a11, a12));
                    ProjectileBase newProjectile1 = Instantiate(projectilePrefab, weaponMuzzle.position, q1);
                    ProjectileBase newProjectile2 = Instantiate(projectilePrefab, weaponMuzzle.position, q2);
                    ProjectileBase newProjectile3 = Instantiate(projectilePrefab, weaponMuzzle.position, q3);
                    ProjectileBase newProjectile4 = Instantiate(projectilePrefab, weaponMuzzle.position, q3);
                    newProjectile1.Shoot(this);
                    newProjectile2.Shoot(this);
                    newProjectile3.Shoot(this);
                    newProjectile4.Shoot(this);
                    break;
                }
            case 5:
                {
                    float a1 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a2 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a3 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a4 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a5 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a6 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a7 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a8 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a9 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a10 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a11 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a12 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a13 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a14 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float a15 = UnityEngine.Random.Range(-2.5f, 2.5f);
                    Quaternion q1 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a1, a2, a3));
                    Quaternion q2 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a4, a5, a6));
                    Quaternion q3 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a7, a8, a9));
                    Quaternion q4 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a10, a11, a12));
                    Quaternion q5 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a13, a14, a15));
                    ProjectileBase newProjectile1 = Instantiate(projectilePrefab, weaponMuzzle.position, q1);
                    ProjectileBase newProjectile2 = Instantiate(projectilePrefab, weaponMuzzle.position, q2);
                    ProjectileBase newProjectile3 = Instantiate(projectilePrefab, weaponMuzzle.position, q3);
                    ProjectileBase newProjectile4 = Instantiate(projectilePrefab, weaponMuzzle.position, q3);
                    ProjectileBase newProjectile5 = Instantiate(projectilePrefab, weaponMuzzle.position, q4);
                    newProjectile1.Shoot(this);
                    newProjectile2.Shoot(this);
                    newProjectile3.Shoot(this);
                    newProjectile4.Shoot(this);
                    newProjectile5.Shoot(this);
                    break;
                }
        }
        
    }

    private void ChooseShoot(int i)
    {
        switch (i)
        {
            case 0:
                {
                    if (projectilePrefab != null)
                    {
                        oneShoot = 1;
                        //Vector3 shotDiraction = weaponMuzzle.forward;
                        float a1 = UnityEngine.Random.Range(-1f, 1f);
                        float a2 = UnityEngine.Random.Range(-1f, 1f);
                        float a3 = UnityEngine.Random.Range(-1f, 1f);
                        Quaternion q1 = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(a1, a2, a3));
                        ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, q1);
                        newProjectile.Shoot(this);

                    }
                    break;
                }
            case 1:
                {
                    if (projectilePrefab != null)
                    {
                        oneShoot = 3f;
                        SGShoot();
                        //Quaternion q = Quaternion.Euler(weaponMuzzle.rotation.eulerAngles + new Vector3(1f, 1f, 1f));
                        //Vector3 shotDiraction = weaponMuzzle.forward;
                        /*ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation);
                        ProjectileBase newProjectile1 = Instantiate(projectilePrefab, weaponMuzzle.position, q);
                        newProjectile.Shoot(this);
                        newProjectile1.Shoot(this);*/

                    }
                    break;
                }
            case 2:
                {
                    if (projectilePrefab != null)
                    {
                        oneShoot = 0.5f;
                        //Vector3 shotDiraction = weaponMuzzle.forward;
                        ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation);
                        newProjectile.Shoot(this);

                    }
                    break;
                }

        }
    }

    private bool TestShoot()
    {
        if (lastShoot+shootDelay<Time.time)
        {
            
            GunShoot();
            weaponSound.PlayOneShot(shootSound);
            currentEnergy += oneShoot;
            //Debug.Log("currentEnergy= " + currentEnergy);
            //Debug.Log("shoot");
            return true;
        }
        return false;
    }

    private void GunShoot()
    {
        ChooseShoot(choosedWeaponShoot);

        /*if(projectilePrefab != null)
        {
            Vector3 shotDiraction = weaponMuzzle.forward;
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation);
            newProjectile.Shoot(this); 
        }*/

        if(weaponMuzzlePrefab!=null)
        {
            GameObject muzzleInstance = Instantiate(weaponMuzzlePrefab, weaponMuzzle.position,weaponMuzzle.rotation,weaponMuzzle.transform);
            Destroy(muzzleInstance, 2);
        }
        lastShoot= Time.time;
    }

    private bool EnergyLimit()
    {
        if(currentEnergy>=maxEnergy)
        {
            return false;
        }
        return true;
    }

    private void Update()
    {
        energyRecover += Time.deltaTime;
        if (energyRecover >= 1 && currentEnergy >= 0)
        {
            energyRecover = 0;
            currentEnergy -= energySpeed;
        }
        
    }
}
