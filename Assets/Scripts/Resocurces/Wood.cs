using UnityEngine;

public class Wood : Resource
{

    private void Start()
    {
       Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
       transform.position = hit.point + Vector3.down * 0.01f;
    }


}
