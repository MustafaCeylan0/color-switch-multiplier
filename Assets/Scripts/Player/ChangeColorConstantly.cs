using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ChangeColorConstantly : MonoBehaviour
{
    [SerializeField] private int startingColorIndex = 0;
    private Color[] colors;
    

    [SerializeField] private float changeTime;

    // Start is called before the first frame update
    void Start()
    {
        colors = FindObjectOfType<GameValues>().gameColors;
        StartCoroutine(changeColorConstantly());
    }

    // Update is called once per frame
    private IEnumerator changeColorConstantly()
    {
        int num = startingColorIndex;
        while (true)
        {
            setColor(num);
            yield return new WaitForSeconds(changeTime);
            num++;
        }
    }

    private void setColor(int num)
    {
        num = num % 4;
        switch (num)
        {
            case 0:
                if ( gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = colors[0];
                    //gameObject.GetComponent<SpriteRenderer>().DOColor(colors[0],changeTime);

                }
                else
                {
                    gameObject.GetComponent<TextMeshPro>().color = colors[0];
                    //gameObject.GetComponent<TextMeshPro>().DOColor(colors[0],changeTime);

                }
                break;
            case 1:
                if ( gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = colors[1];
                    //gameObject.GetComponent<SpriteRenderer>().DOColor(colors[1],changeTime);

                }
                else
                {
                    gameObject.GetComponent<TextMeshPro>().color = colors[1];
                    //gameObject.GetComponent<TextMeshPro>().DOColor(colors[1],changeTime);

                }
                break;
            case 2:

                if ( gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = colors[2];
                   // gameObject.GetComponent<SpriteRenderer>().DOColor(colors[2],changeTime);

                }
                else
                {
                    gameObject.GetComponent<TextMeshPro>().color = colors[2];
                    //gameObject.GetComponent<TextMeshPro>().DOColor(colors[2],changeTime);

                }
                break;
            case 3:
                if ( gameObject.GetComponent<SpriteRenderer>())
                {
                    gameObject.GetComponent<SpriteRenderer>().color = colors[3];
                    //gameObject.GetComponent<SpriteRenderer>().DOColor(colors[3],changeTime);

                }
                else
                {
                    gameObject.GetComponent<TextMeshPro>().color = colors[3];
                    //gameObject.GetComponent<TextMeshPro>().DOColor(colors[3],changeTime);

                }
                break;
        }
    }
}