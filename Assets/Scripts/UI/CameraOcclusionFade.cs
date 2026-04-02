using UnityEngine;
using System.Collections.Generic;


public class CameraOcclusionFader : MonoBehaviour
{
    private Player player;

    [SerializeField] private LayerMask occlusionLayerMask;
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float fadedAlpha = 0.25f;
    [SerializeField] private float sphereRadius = 0.3f;

    private Dictionary<Renderer, Material[]> fadedObjects = new Dictionary<Renderer, Material[]>();
    private HashSet<Renderer> currentFrameHits = new HashSet<Renderer>();

    private void Start()
    {
        player = Player.Instance;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        currentFrameHits.Clear();

        Vector3 start = transform.position;
        Vector3 end = player.transform.position;
        Vector3 dir = end - start;
        float distance = dir.magnitude;

        RaycastHit[] hits = Physics.SphereCastAll(start, sphereRadius, dir.normalized, distance, occlusionLayerMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == player.gameObject)
                continue;

            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null) continue;

            currentFrameHits.Add(rend);

            if (!fadedObjects.ContainsKey(rend))
            {
                fadedObjects.Add(rend, rend.materials); // creates instance copies
            }
        }

        // Fade OUT blockers
        foreach (Renderer rend in currentFrameHits)
        {
            FadeRendererMaterials(fadedObjects[rend], fadedAlpha);
        }

        // Fade IN objects no longer blocking
        List<Renderer> toRemove = new List<Renderer>();

        foreach (var pair in fadedObjects)
        {
            Renderer rend = pair.Key;
            Material[] mats = pair.Value;

            if (!currentFrameHits.Contains(rend))
            {
                FadeRendererMaterials(mats, 1f);

                bool fullyVisible = true;

                foreach (Material mat in mats)
                {
                    if (!mat.HasProperty("_BaseColor")) continue;

                    float a = mat.GetColor("_BaseColor").a;
                    if (Mathf.Abs(a - 1f) > 0.01f)
                    {
                        fullyVisible = false;
                        break;
                    }
                }

                if (fullyVisible)
                    toRemove.Add(rend);
            }
        }

        foreach (Renderer rend in toRemove)
        {
            fadedObjects.Remove(rend);
        }
    }

    private void FadeRendererMaterials(Material[] mats, float targetAlpha)
    {
        foreach (Material mat in mats)
        {
            if (!mat.HasProperty("_BaseColor"))
                continue;

            Color color = mat.GetColor("_BaseColor");
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            mat.SetColor("_BaseColor", color);
        }
    }
}
