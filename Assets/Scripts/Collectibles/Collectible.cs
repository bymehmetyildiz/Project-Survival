using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int amount;
    public virtual void Start()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity);
        if (hitInfo.collider != null)
        {
            transform.position = hitInfo.point;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
       
    }

    public bool IsPlayerInRange()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position) < 2)
            return true;
        return false;
    }
}

public enum CollectibleType
{
    Wood,
    Stone
}
