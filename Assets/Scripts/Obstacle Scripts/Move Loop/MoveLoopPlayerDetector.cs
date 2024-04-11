using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLoopPlayerDetector : MonoBehaviour
{
    [SerializeField] bool onlyParent = false;
    [SerializeField] GameObject movingObjectsParent;
    private MoveLoop[] moveLoopScripts;

    private void Start()
    {
        ///TODO try starting the coroutine at the begining instead of with detector 
        if (!onlyParent)
        {
            int i = 0;
            Transform[] childrenTransforms = movingObjectsParent.GetComponentsInChildren<Transform>();
            moveLoopScripts = new MoveLoop[childrenTransforms.Length - 1];
            foreach (Transform curr in childrenTransforms)
            {
                if (curr.gameObject.GetComponent<MoveLoop>())
                {
                    moveLoopScripts[i] = curr.gameObject.GetComponent<MoveLoop>();
                    i++;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("detector !! ");

            if (onlyParent)
            {
                //will only move the moving objects parent

                movingObjectsParent.GetComponent<MoveLoop>().fireTheMoveLoop();
            }
            else
            {
                //fire all the move loops
                foreach (var curr in moveLoopScripts)
                {
                    curr.fireTheMoveLoop();
                }

                //deactivate the trigger
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}