using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform shootPoint;
    public Projectile bullet;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Shoot()
    {
        Projectile newBullet = Instantiate(bullet, shootPoint.position, Quaternion.LookRotation(shootPoint.forward));
        newBullet.rb.linearVelocity = shootPoint.forward * newBullet.speed;
    }

}

public enum WeaponType
{
    PISTOL,
    RIFLE,
    SHOTGUN,
    HEAVY
}
