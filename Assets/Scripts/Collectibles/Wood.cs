using UnityEngine;

public class Wood : Collectible
{
    private int amount;

    public Wood(int amount)
    {
        this.amount = amount;
    }

    public override void Start()
    {
       Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
       transform.position = hit.point + Vector3.down * 0.01f;
    }

    public override void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (player.carryCapacity < amount && player.isBusyCarrying == false)
            {
                amount -= player.carryCapacity;
                player.isBusyCarrying = true;
            }

        }
    }

}
