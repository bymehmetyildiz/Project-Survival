using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform shootPoint;
    public Projectile bullet;
    public GameObject rocket;
    public int bulletAmount;
    public int magazineSize;
    public int currentAmmo;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Shoot()
    {
        Projectile newBullet = Instantiate(bullet, shootPoint.position, Quaternion.LookRotation(shootPoint.forward * -1));
        newBullet.rb.linearVelocity = shootPoint.forward * newBullet.speed;
        currentAmmo--;

        if (weaponType == WeaponType.HEAVY)
        {
            rocket.SetActive(false);
            newBullet.gameObject.transform.localScale *= 2f;
        }
    }

    public void Reload()
    {
        if(bulletAmount >= magazineSize)
        {
            bulletAmount -= magazineSize;
            currentAmmo = magazineSize;
            
        }
        else if(bulletAmount < magazineSize && bulletAmount > 0)
        {
            currentAmmo = bulletAmount;
            bulletAmount = 0;
            
        }
        else if(bulletAmount <= 0)
        {
            Debug.Log("No ammo left");
            bulletAmount = 0;
            return;
        }

        
    }

}

public enum WeaponType
{
    PISTOL,
    RIFLE,
    SHOTGUN,
    HEAVY
}
