using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform shootPoint;
    public Projectile bullet;
    public GameObject rocket;

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

        if (weaponType == WeaponType.HEAVY)
        {
            rocket.SetActive(false);
            newBullet.gameObject.transform.localScale *= 2f;
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
