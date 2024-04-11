using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Block : MonoBehaviour
{
    public string blockColor;
    [SerializeField] private TextMeshPro armorText;
    private int armor;
    [SerializeField] private GameObject destroyParticle;

    private Color[] colors;


    private void Awake()
    {
        colors = FindObjectOfType<GameValues>().gameColors;
        armor = int.Parse(armorText.text);
        switch (blockColor.ToLower())
        {
            case "red":

                gameObject.GetComponent<SpriteRenderer>().color = colors[0];

                break;
            case "purple":

                gameObject.GetComponent<SpriteRenderer>().color = colors[1];

                break;
            case "green":

                gameObject.GetComponent<SpriteRenderer>().color = colors[2];

                break;
            case "yellow":

                gameObject.GetComponent<SpriteRenderer>().color = colors[3];

                break;
        }
    }

    public void takeDamage()
    {
        if (armor - 1 > 0)
        {
            armor--;

            armorText.text = "" + armor;
        }
        else
        {
            die();
        }
    }

    private void die()
    {
        transform.DOScale(0, .3f);
        Invoke(nameof(instantiateDeathParticle), .3f);
    }

    private void instantiateDeathParticle()
    {
        Instantiate(destroyParticle, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}