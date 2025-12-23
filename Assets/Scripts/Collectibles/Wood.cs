using UnityEngine;

public class Wood : Collectible
{
    private Canvas woodCanvas;  
    private Camera mainCamera;
    public Vector3 offset;


    public override void Start()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f);
        transform.position = hit.point + Vector3.down * 0.01f;
        
        woodCanvas = GetComponentInChildren<Canvas>();
        mainCamera = Camera.main;
    }

    public override void Update()
    {
        base.Update();

        if(IsPlayerNearby())
        {
            woodCanvas.enabled = true;
        }
        else
        {
            woodCanvas.enabled = false;
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        // Face camera
        Vector3 dir = woodCanvas.transform.position - mainCamera.transform.position;        
        woodCanvas.transform.rotation = Quaternion.LookRotation(dir);

        // Local offset above the wood
        woodCanvas.transform.localPosition = offset;
    }

    private bool IsPlayerNearby()
    {
        Vector3 dist = Player.Instance.transform.position - transform.position;

        if(dist.magnitude <= 2)
            return true;
        else
            return false;
    }


}
