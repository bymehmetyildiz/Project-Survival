using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Building : MonoBehaviour
{
    public GameObject buildingModel;
    public GameObject UIProgress;
    public GameObject buildEffect;
    public bool isBuilt;

    public int requiredWood = 100;
    public int requiredStone = 100;

    public int currentWood;
    public int currentStone;

    public Image woodProgressBar;
    public Image stoneProgressBar;
    public float growRatio;

    void Start()
    {
        isBuilt = false;
        buildingModel.transform.localScale = Vector3.zero;
        buildingModel.SetActive(false);
        UIProgress.SetActive(true);
        woodProgressBar.fillAmount = (float)currentWood / requiredWood;
        stoneProgressBar.fillAmount = (float)currentStone / requiredStone;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null && player.isBusyCarrying)
        {
            StartCoroutine(UpdateResources(player));
            player.stateMachine.ChangeState(player.deliverState);

        }
    }

    private IEnumerator UpdateResources(Player player)
    {
        Collectible collectible = player.currentCollectible;
        if (collectible == null)
            yield break;

        if (collectible is Wood)
        {
            if(currentWood < requiredWood)
            {
                int target = collectible.amount;
                while (collectible.amount > 0)
                {
                    currentWood++;
                    collectible.amount--;
                    woodProgressBar.fillAmount = (float)currentWood / requiredWood;
                    yield return new WaitForSeconds(0.001f);
                }
             
                if (currentWood > requiredWood)
                    currentWood = requiredWood;

                woodProgressBar.fillAmount = (float)currentWood / requiredWood;
            }
        }
        else if (collectible is Brick)
        {
            if (currentStone < requiredStone)
            {
                int target = collectible.amount;
                while (collectible.amount > 0)
                {
                    currentStone++;
                    collectible.amount--;
                    stoneProgressBar.fillAmount = (float)currentStone / requiredStone;
                    yield return new WaitForSeconds(0.001f);
                }
                
                if (currentStone > requiredStone)
                    currentStone = requiredStone;

                stoneProgressBar.fillAmount = (float)currentStone / requiredStone;
            }
        }

        yield return new WaitForSeconds(0.5f);

        if(currentWood >= requiredWood && currentStone >= requiredStone)
        {
            Instantiate(buildEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            isBuilt = true;
            buildingModel.SetActive(true);
            buildingModel.transform.DOScale(Vector3.one * growRatio, 1f).SetEase(Ease.OutBack);
            UIProgress.SetActive(false);
        }
    }

}
