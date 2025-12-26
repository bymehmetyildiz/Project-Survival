using UnityEngine;

public class Collectible : MonoBehaviour
{
    public virtual void Start()
    {
        
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
