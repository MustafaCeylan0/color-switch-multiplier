using System.Collections;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    private float midpointX;
    private float midpointY;
    void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        float xSum = 0;
        float ySum = 0;
        foreach (var tr in transforms)
        {
            xSum += tr.position.x;
            ySum += tr.position.y;
        }

        xSum -= transform.position.x;
        ySum -= transform.position.y;
        midpointX = xSum / 4;
        midpointY = ySum / 4;
        StartCoroutine(rotateWindMill());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator rotateWindMill()
    {
        while (true)
        {
            gameObject.transform.RotateAround(new Vector3(midpointX,midpointY,0),Vector3.forward, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
