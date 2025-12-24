using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
