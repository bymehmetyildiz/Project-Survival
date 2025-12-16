using UnityEngine;

public class CameraOcclusionFade : MonoBehaviour
{
    [SerializeField] private Material fadeMaterial;
    [SerializeField] private LayerMask occlusionLayerMask;

    private Player player;
    private Renderer currentFadedRenderer;
    private Material originalMaterial;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        float dist = dir.magnitude;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, dist, occlusionLayerMask))
        {
            Renderer r = hit.collider.GetComponent<Renderer>();

            if (r != null && r != currentFadedRenderer)
            {
                RestoreCurrent();

                originalMaterial = r.material;
                currentFadedRenderer = r;
                currentFadedRenderer.material = fadeMaterial;
            }
        }
        else
        {
            RestoreCurrent();
        }
    }

    private void RestoreCurrent()
    {
        if (currentFadedRenderer == null)
            return;

        currentFadedRenderer.material = originalMaterial;
        currentFadedRenderer = null;
        originalMaterial = null;
    }
}
