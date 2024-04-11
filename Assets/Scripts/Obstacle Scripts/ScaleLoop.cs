using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleLoop : MonoBehaviour
{
    [Header("Dont forget to locate children of this object to the center")]
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    [SerializeField] private float shrinkingDuration;
    [SerializeField] private float growingDuration;

    [SerializeField] private float shrinkedWaiting;
    [SerializeField] private float growedWaiting;

    
    
    void Start()
    {
        StartCoroutine(scaleLoop());
    }

    IEnumerator scaleLoop()
    {
        while (true)
        {
            //shrink
            transform.DOScale(minSize, shrinkingDuration);
            //wait as shrinked
            yield return new WaitForSeconds(shrinkedWaiting);
            //grow
            transform.DOScale(maxSize, growingDuration);
            //wait as growed
            yield return new WaitForSeconds(growedWaiting);
        }
    }
}
