using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    public RectTransform interactBtn;

    private void Start()
    {
        interactBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        Collectible collectible = Player.Instance.NearestCollectibleResource();

        if (collectible != null && collectible.IsPlayerInRange())
        {
            interactBtn.gameObject.SetActive(true);

            Vector3 screenPos 
                = Camera.main.WorldToScreenPoint(
                    Player.Instance.NearestCollectibleResource().transform.position
                );

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                interactBtn.parent as RectTransform,
                screenPos,
                null, // Overlay canvas
                out Vector2 localPos
            );

            localPos.y += 100f;
            interactBtn.localPosition = localPos;

            if(Input.GetKeyDown(KeyCode.E))
            {
                CollectMaterial();
            }
        }
        else
        {
            interactBtn.gameObject.SetActive(false);
        }
    }

    public void CollectMaterial()
    {
        Player player = Player.Instance;

        if (player.CanCollectResource())
        {
            player.stateMachine.ChangeState(player.pickUpState);
        }
    }
}
