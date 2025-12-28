using DG.Tweening;
using UnityEngine;

public class Stone : Resource
{
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject poofParticle;
    [SerializeField] private GameObject brick;

    public override void Interact(Player player)
    {
        if (hitCount < necessaryHitCount)
        {
            hitCount++;
            hitParticle.Play();
        }
        else
        {
            StartCoroutine(CreateCollectible(poofParticle, brick, this.gameObject, this));
        }
    }
}
