using System.Collections;
using UnityEngine;

public class RotateAndWait : MonoBehaviour
{
    private float midpointX;
    private float midpointY;
    private Vector3 midpoint;
    [SerializeField] private float delayBetweenMoves = 3f;
    [SerializeField] private float delayBetweenDegrees = 0.005f;
    [SerializeField] private float degreesPerMove = 90;
    [SerializeField] private float verticalCenterOffset = 0;
    [SerializeField] private float horizontalCenterOffset = 0;
    [SerializeField] private float rotationDegree = 1;
    private bool isAlone;
    [SerializeField] private bool isMoving = false;

    void Start()
    {
        calculateMidpoint();

        StartCoroutine(rotateAndWait());
    }

    
    
    private void calculateMidpoint()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        if (transforms.Length > 1)
        {
            isAlone = false;
            int numberOfUnits = GetComponentsInChildren<Transform>().Length - 1;
            float xSum = 0;
            float ySum = 0;
            foreach (var tr in transforms)
            {
                xSum += tr.position.x;
                ySum += tr.position.y;
            }


            xSum -= transform.position.x;
            ySum -= transform.position.y;
            midpointX = (xSum / numberOfUnits) + horizontalCenterOffset;
            midpointY = (ySum / numberOfUnits) + verticalCenterOffset;
        }
        else if (transforms.Length == 1)
        {
            isAlone = true;
        }

        midpoint = new Vector3(midpointX, midpointY);
    }

    IEnumerator rotateAndWait()
    {
        while (true)
        {
            for (int i = 0; i < degreesPerMove; i++)
            {
                if (!isAlone)
                {
                    if (isMoving)
                    {
                        calculateMidpoint();
                    }
                    gameObject.transform.RotateAround(midpoint, Vector3.forward, rotationDegree);
                }
                else
                {
                    gameObject.transform.Rotate(Vector3.forward, rotationDegree);
                }
                yield return new WaitForSeconds(delayBetweenDegrees);
            }

            yield return new WaitForSeconds(delayBetweenMoves);
        }
    }
}