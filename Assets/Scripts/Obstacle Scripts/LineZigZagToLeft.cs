using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineZigZagToLeft : MonoBehaviour
{

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform postStartTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float speed = 2;
    private float interval;//the x lenght of a single line
    

    private float maxX;
    private float minX;
    void Start()
    {
        if (speed == 0)
        {
            speed = 2;
        }
        interval = postStartTransform.position.x - startTransform.position.x;
        maxX = endTransform.position.x;
        minX = startTransform.position.x - interval;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.left*Time.deltaTime *speed,Space.World);
        if (transform.position.x <= maxX)
        {
            transform.position = new Vector3(minX, transform.position.y);
        }
        
    }


}