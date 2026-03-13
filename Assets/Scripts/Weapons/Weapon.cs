using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

public enum WeaponType
{
    PISTOL,
    RIFLE,
    SHOTGUN,
    HEAVY
}
