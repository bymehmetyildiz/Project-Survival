using UnityEngine;

public class Wood : Collectible
{
    
    public override void Start()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity);
        if (hitInfo.collider != null)
        {
            transform.position = hitInfo.point;
        }

        base.Start();

    }

    public override void Update()
    {
        base.Update();
    }

    public override void GetCollected()
    {
        base.GetCollected();
    }
}
