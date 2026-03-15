using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public GameObject impactEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject impact = Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}
