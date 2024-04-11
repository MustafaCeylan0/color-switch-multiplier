using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Substracter : MonoBehaviour
{
    private bool isActive = true;
    [SerializeField] private int substractAmount;

    public void substract()
    {
        if (isActive)
        {
            Player[] players = FindObjectsOfType<Player>();
            for (int i = 0; i < substractAmount && i < players.Length; i++)
            {
                players[i].die();
            }

            isActive = false;
        }
    }
}