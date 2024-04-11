using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MoveLoop : MonoBehaviour

{
    enum Type
    {
        onlyX,
        onlyY,
        both
    }

    [SerializeField] private Type moveType;
    [SerializeField] private Transform startingTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float durationOfMovingToEnd;
    [SerializeField] private float durationOfMovingToStart;
    [SerializeField] private float waitingAtTheEndPos;
    [SerializeField] private float waitingAtTheStartPos;
    [SerializeField] private float startingDelay;
    private bool isMoveLoopOn = false;

    private void Start()
    {
        //fireTheMoveLoop();
    }

    public void fireTheMoveLoop()
    {
        if (!isMoveLoopOn)
        {
            switch (moveType)
            {
                case Type.both:
                    StartCoroutine(startMoveLoopBoth());
                    break;

                case Type.onlyX:
                    StartCoroutine(startMoveLoopOnlyX());
                    break;

                case Type.onlyY:
                    StartCoroutine(startMoveLoopOnlyY());
                    break;
            }
        }

        isMoveLoopOn = true;
    }

    IEnumerator startMoveLoopBoth()
    {
        yield return new WaitForSeconds(startingDelay);
        while (true)
        {
            //object is at the start pos
            //wait for the waitingAtTheStartPos
            yield return new WaitForSeconds(waitingAtTheStartPos);
            //move to the end pos
            transform.DOMove(endTransform.position, durationOfMovingToEnd);
            yield return new WaitForSeconds(durationOfMovingToEnd);
            //object is at the end pos
            //wait for the waitingAtTheEndPos
            yield return new WaitForSeconds(waitingAtTheEndPos);
            //move to the start pos
            transform.DOMove(startingTransform.position, durationOfMovingToStart);
            yield return new WaitForSeconds(durationOfMovingToStart);


            //repeat all again
        }
    }
    
    IEnumerator startMoveLoopOnlyX()
    {
        yield return new WaitForSeconds(startingDelay);
        while (true)
        {
            //object is at the start pos
            //wait for the waitingAtTheStartPos
            yield return new WaitForSeconds(waitingAtTheStartPos);
            //move to the end pos
            transform.DOMoveX(endTransform.position.x, durationOfMovingToEnd);
            yield return new WaitForSeconds(durationOfMovingToEnd);
            //object is at the end pos
            //wait for the waitingAtTheEndPos
            yield return new WaitForSeconds(waitingAtTheEndPos);
            //move to the start pos
            transform.DOMoveX(startingTransform.position.x, durationOfMovingToStart);
            yield return new WaitForSeconds(durationOfMovingToStart);


            //repeat all again
        }
    }
    
    IEnumerator startMoveLoopOnlyY()
    {
        yield return new WaitForSeconds(startingDelay);
        while (true)
        {
            //object is at the start pos
            //wait for the waitingAtTheStartPos
            yield return new WaitForSeconds(waitingAtTheStartPos);
            //move to the end pos
            transform.DOMoveY(endTransform.position.y, durationOfMovingToEnd);
            yield return new WaitForSeconds(durationOfMovingToEnd);
            //object is at the end pos
            //wait for the waitingAtTheEndPos
            yield return new WaitForSeconds(waitingAtTheEndPos);
            //move to the start pos
            transform.DOMoveY(startingTransform.position.y, durationOfMovingToStart);
            yield return new WaitForSeconds(durationOfMovingToStart);


            //repeat all again
        }
    }
}