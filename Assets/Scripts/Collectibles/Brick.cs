using UnityEngine;

public class Brick : Collectible
{
    public override void Start()
    {
        base.Start();

        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity);
        if (hitInfo.collider != null)
        {
            transform.position = hitInfo.point;
        }


    }
}
