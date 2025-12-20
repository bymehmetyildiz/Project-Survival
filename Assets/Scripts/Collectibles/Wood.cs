using UnityEngine;

public class Wood : Collectible
{
    public int amount = 3;

    public override void Start()
    {
       Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
       transform.position = hit.point + Vector3.down * 0.01f;
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (player.carryCapacity < amount)
            {
                amount -= player.carryCapacity;
            }

        }
    }

}
