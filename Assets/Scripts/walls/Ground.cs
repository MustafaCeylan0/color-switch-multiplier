using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private float startPosY;
    public bool isDeadly = false;
    void Start()
    {
        transform.position =
            -new Vector2(transform.position.x,Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y);
        startPosY = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.y > startPosY + 10)
        {
            isDeadly = true;
        }
        if (other.CompareTag("Player") && isDeadly)
        {
            other.GetComponent<Player>().die();
        }
    }
}
