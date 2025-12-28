using UnityEngine;

public class Wood : Collectible
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

    public override void Update()
    {
        base.Update();
    }
}
