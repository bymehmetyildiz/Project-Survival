using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Resource : MonoBehaviour
{
    public ResourceType resourceType;
    public virtual void Interact(Player player)
    {

    }

}

public enum ResourceType
{
    Collectible,
    Interactible,
}
