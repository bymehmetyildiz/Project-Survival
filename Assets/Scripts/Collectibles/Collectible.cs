using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected Canvas woodCanvas;
    protected bool isCollected;
    
    public virtual void Start()
    {
        woodCanvas = GetComponentInChildren<Canvas>();
        woodCanvas.enabled = false;
        isCollected = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (woodCanvas == null) return;

        bool show = IsPlayerInRange() && !isCollected;
        if (woodCanvas.enabled != show)
        {
            woodCanvas.enabled = show;
        }

    }

    

    protected virtual bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(Player.Instance.transform.position, transform.position);

        if(distance <= 4f)
            return true;
        else
            return false;
   
    }

    public virtual void GetCollected()
    {
        Player player = Player.Instance;

        if (player.CanCollectResource())
        {
            isCollected = true;
            player.stateMachine.ChangeState(player.pickUpState);
        }

    }

}
