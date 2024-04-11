using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorDeactivator : MonoBehaviour
{
private enum State {
active,
passive,
}

private GameObject levelObject;

private void Awake()
{
    levelObject = transform.GetChild(0).gameObject;
}


private State currentState = State.passive;
[SerializeField] private bool isFirst = false;

// Start is called before the first frame update

void Start()
    {           
        
        Debug.Log("name of the level object is " + levelObject.name);

        if (isFirst)
        {
            currentState = State.active;
        }
     
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("call from activator-deactivator, collided with" + other.gameObject.name);
        if (other.gameObject.CompareTag("activator")  )
        {
            Debug.Log("setting object " + levelObject.name + " to active");
            levelObject.SetActive(true);
            currentState = State.active;
            
        }
        if (other.gameObject.CompareTag("deleter")  )
        {
            Debug.Log("setting object " + levelObject.name + " to deactive");

            levelObject.SetActive(false);
            currentState = State.passive;
        }
    }

// Update is called once per frame


}
