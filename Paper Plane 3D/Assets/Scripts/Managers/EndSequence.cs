using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class EndSequence : MonoBehaviour
{
    [SerializeField] private List<Transform> multiplierBlocks = new List<Transform>();

    [SerializeField] private float yOffset;
    private void Awake()
    {
        SetupBlocks();
    }

  
    private void SetupBlocks()
    {
        float yValue = 0;
        int blockIndex = 1;
        foreach (var block in multiplierBlocks)
        {
            block.DOLocalMove(new Vector3(0, yValue, 0), 0);
            block.GetComponentInChildren<Multiplier>().SetMultiplier(blockIndex);
            block.GetComponentInChildren<TextMeshPro>().text = "x" + blockIndex;
            blockIndex++;
            yValue += yOffset;
        }
    }
    
 
}
