using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToPlayerSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] playerImages;


    private void Awake()
    {
        int playerIndex = PlayerPrefs.GetInt("Player Object Index");
        RectTransform rt = gameObject.GetComponent<RectTransform>();

        gameObject.GetComponent<Image>().sprite = playerImages[playerIndex];

        if (playerIndex == 0)
        {
            rt.sizeDelta = new Vector2(35, 35);
        }
        else if (playerIndex == 1)
        {
            rt.sizeDelta = new Vector2(60, 80);
        }
        else if (playerIndex == 3)
        {
            rt.sizeDelta = new Vector2(75, 100);
        }
    }
}