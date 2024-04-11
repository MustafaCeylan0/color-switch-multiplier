using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWall : MonoBehaviour
{
    void Start()
    {
        transform.position =
            -new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x,
                transform.position.y);
    }
}