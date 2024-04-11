using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEndIndividual : MonoBehaviour
{

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform postStartTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float speed = 2;
    private float intervalx;
    private float intervaly;
    private Vector2 movingDirection;
    

    private float maxY;
    
    private float minX;
    private float minY;
    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0)
        {
            speed = 2;
        }
        intervalx = postStartTransform.position.x - startTransform.position.x;
        intervaly = postStartTransform.position.y - startTransform.position.y;
        maxY = endTransform.position.y;
        minX = startTransform.position.x - intervalx;
        minY = startTransform.position.y - intervaly;
        movingDirection = new Vector2(intervalx, intervaly).normalized;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(movingDirection*Time.deltaTime *speed,Space.World);
        if (transform.position.y >= maxY)
        {
            transform.position = new Vector3(minX, minY);
        }
        
    }


}