using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    public int totalPlayerAmount = 1;
    private Rigidbody2D playerRB;
    [SerializeField] private bool disableDeath  = false;
    [Header("Player Values")] [SerializeField]
    private float pushingPower = 5;


    [SerializeField] [Tooltip("The proportion of the x value in the pushing vector.")]
    private float xValue = 1;

    [SerializeField] [Tooltip("The proportion of the y value in the pushing vector.")]
    private float yValue = 2;


    private SFXManager sfxManager;
    public bool isControlsActive = true;

    private void Awake()
    {
        Invoke(nameof(setTotalPlayersAtStart), .3f);
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    private void setTotalPlayersAtStart()
    {
        totalPlayerAmount = FindObjectsOfType<Player>().Length;
    }


    private void Update()
    {
#if UNITY_EDITOR


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
                if (!sfxManager)
                {
                    sfxManager = FindObjectOfType<SFXManager>();
                }
                if (FindObjectOfType<LevelManagement>().levelStatus.Equals("play"))
                {
                    // play the same jump sound no matter left or right jump
                    sfxManager.jump();
                }
        }

#else
       if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!sfxManager)
                {
                    sfxManager = FindObjectOfType<SFXManager>();
                }

                if (FindObjectOfType<LevelManagement>().levelStatus.Equals("play"))
                {
                    // play the same jump sound no matter left or right jump
//                    Debug.Log("player parent detected touch");
                    sfxManager.jump();
                }
            }
        }
#endif






        if (!disableDeath)
        {
             if (totalPlayerAmount <= 0)
                    {
                        if (FindObjectOfType<LevelManagement>().levelStatus == "play")
                        {
                            FindObjectOfType<LevelManagement>().levelFailed();
                            isControlsActive = false;
                        }
                    }
        }
       
    }
}