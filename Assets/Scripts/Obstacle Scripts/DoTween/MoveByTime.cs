using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveByTime : MonoBehaviour
{
    [SerializeField] [Tooltip("Start counting from 0.")]private float totalObjectAmount;
    [SerializeField] [Tooltip("Start counting from 0.")]private float orderOfThisObject;
    [SerializeField] private float timeOfEachLoop;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    private float firstTime;
    
    void Start()
    {
        firstTime = timeOfEachLoop * ((totalObjectAmount - orderOfThisObject) / totalObjectAmount);
        firstTimeMove();
    }

    // Update is called once per frame


    private void firstTimeMove()
    {
        transform.DOMove(endPos.position, firstTime);
        Invoke(nameof(goBackToTheStartPoint),firstTime);
    }

    private void makeFullLoop()
    {
        transform.DOMove(endPos.position, timeOfEachLoop);
        Invoke(nameof(goBackToTheStartPoint),timeOfEachLoop);

    }

    private void goBackToTheStartPoint()
    {
        transform.position = startPos.position;
        makeFullLoop();
    }
}
