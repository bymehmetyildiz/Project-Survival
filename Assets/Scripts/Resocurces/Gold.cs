using UnityEngine;

public class Gold : Resource
{
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject poofParticle;
    [SerializeField] private GameObject goldIngots;

    public override void Interact(Player player)
    {
        if (hitCount < necessaryHitCount)
        {
            hitCount++;
            hitParticle.Play();
        }
        else
        {
            StartCoroutine(CreateCollectible(poofParticle, goldIngots, this.gameObject, this));
        }
    }
}
