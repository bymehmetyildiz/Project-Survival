using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Resource : MonoBehaviour
{
    public ResourceStatus resourceType;
    public virtual void Interact(Player player)
    {

    }


}

public enum ResourceStatus
{
    Collectible,
    Interactible,
}

