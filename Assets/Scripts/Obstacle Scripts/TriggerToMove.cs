using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerToMove : MonoBehaviour
{
[SerializeField] private float destinationX;
[SerializeField] private GameObject movingObject;
[SerializeField] private float duration;
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        movingObject.transform.DOMoveX(destinationX, duration);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
}
