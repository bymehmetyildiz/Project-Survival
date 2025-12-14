using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public int countX = 100;
    public int countZ = 100;
    public float spacing = 12.1464f;

#if UNITY_EDITOR
    [ContextMenu("Generate Blocks")]
    public void Generate()
    {
        Clear();

        for (int x = 0; x < countX; x++)
        {
            for (int z = 0; z < countZ; z++)
            {
                Vector3 pos = new Vector3(
                    x * spacing,
                    -2.624965f,
                    z * spacing
                );

                GameObject block = (GameObject)PrefabUtility.InstantiatePrefab(blockPrefab);
                block.transform.SetParent(transform);
                block.transform.position = pos;
            }
        }
    }

    [ContextMenu("Clear Blocks")]
    public void Clear()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
#endif
}
