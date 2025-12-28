using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;
using static Unity.Collections.AllocatorManager;

public class Resource : MonoBehaviour
{
    protected int hitCount;
    protected int necessaryHitCount = 3;
    public ResourceStatus resourceType;
    public virtual void Interact(Player player)
    {

    }

    protected IEnumerator CreateCollectible(GameObject poof, GameObject collectible, GameObject destroy, Resource resource)
    {
        Instantiate(poof, resource.transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(collectible, resource.transform.position, Quaternion.identity);
        Destroy(destroy);
    }

}

public enum ResourceStatus
{
    Collectible,
    Interactible,
}

