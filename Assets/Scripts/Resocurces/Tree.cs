using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;


public class Tree : Resource
{
    private int hitCount;
    private int necessaryHitCount = 3;
    private Rigidbody rb;
    public TreeState state;
    [SerializeField] private GameObject wood;
  

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(rb != null )
            rb.isKinematic = true;
    }

   
    void Update()
    {
       
    }

    public override void Interact(Player player)
    {
        if (state == TreeState.Standing)
        {
            if (hitCount < necessaryHitCount)
            {
                hitCount++;
                transform.DOPunchScale(
                   Vector3.one * -0.2f, // shrink
                   0.25f,               // duration
                   5,                   // vibrato
                   0.9f                 // elasticity
               );
            }
            else
            {
                Vector3 dir = player.transform.position - transform.position;
                rb.isKinematic = false;
                rb.AddForce(-dir.normalized * 5f, ForceMode.Impulse);
                StartCoroutine(IsTreeFallen());
            }
        }

        else if (state == TreeState.Fallen)
        {
            if (hitCount < necessaryHitCount)
            {
                hitCount++;
                transform.DOPunchScale(
                    Vector3.one * -0.2f, // shrink
                    0.25f,               // duration
                    5,                   // vibrato
                    0.9f                 // elasticity
                );                
            }
            else
            {
                Instantiate(wood, transform.position + transform.up, Quaternion.identity);
                Destroy(gameObject);
            }
        }
            

    }

    private IEnumerator IsTreeFallen()
    {
        while (true)
        {
            float angle = Vector3.Angle(transform.up, Vector3.up);

            if (angle >= 75f)
                break;

            yield return null; // wait 1 frame
        }

        rb.isKinematic = true;
        state = TreeState.Fallen;
        hitCount = 0;
    }



}

public enum TreeState
{
    Standing,
    Fallen
}