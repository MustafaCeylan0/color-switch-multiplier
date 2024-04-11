using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float midpointX;
    [SerializeField] private float midpointY;
    private Vector3 midpoint;
    [SerializeField] private float delayBetweenDegrees = 0.02f;
    [SerializeField] private float verticalCenterOffset = 0;
    [SerializeField] private float horizontalCenterOffset = 0;
    private bool isAlone;
    [SerializeField] private bool isMoving = false;

    [Tooltip("1 for rotating anti-clockwise, -1 for rotating clockwise")] [SerializeField]
    private float rotateDegree = -4;

    void Start()
    {


        setMidPoint();

        StartCoroutine(rotateAround());
    }

    private void setMidPoint()
    {
        
        Transform[] transforms = GetComponentsInChildren<Transform>();
        int numberOfUnits = GetComponentsInChildren<Transform>().Length - 1;
        if (numberOfUnits == 0 || gameObject.CompareTag("Player"))
        {

            isAlone = true;
        }
        else
        {
            isAlone = false;
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

        midpoint = new Vector3(midpointX, midpointY, 0);
    }


    IEnumerator rotateAround()
    {
        while (true)
        {
            if (isMoving)
            {
                setMidPoint();
            }
            if (isAlone)
            {
                gameObject.transform.Rotate(Vector3.forward, rotateDegree);

            }
            else
            {
                gameObject.transform.RotateAround(midpoint, Vector3.forward, rotateDegree);

            }
            yield return new WaitForSeconds(delayBetweenDegrees);

        }
    }
}