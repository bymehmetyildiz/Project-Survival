using UnityEngine;

public class Building : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && player.isBusyCarrying && !player.IsMoving())
        {
            player.stateMachine.ChangeState(player.putDownState);
        }
    }
}
