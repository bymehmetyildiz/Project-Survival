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
        PlaceBtnOnCollectible();
    }

    private void PlaceBtnOnCollectible()
    {
        Collectible collectible = Player.Instance.NearestCollectibleResource();
        Collectible currentCollectible = Player.Instance.currentCollectible;

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                CollectMaterial();
            }
        }
        else if(currentCollectible != null)
        {
            interactBtn.gameObject.SetActive(true);

            Vector3 screenPos
               = Camera.main.WorldToScreenPoint(
                   Player.Instance.transform.position
               );

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
               interactBtn.parent as RectTransform,
               screenPos,
               null, // Overlay canvas
               out Vector2 localPos
            );

            localPos.y += 150f;
            interactBtn.localPosition = localPos;
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
        else if (player.currentCollectible != null)
        {
            player.stateMachine.ChangeState(player.putDownState);
        }

    }
}
