using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    
    void Update()
    {
        
    }

    public void CollectNearestResource() => player.InteractNearestResource();
}
