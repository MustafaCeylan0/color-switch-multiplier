using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorConstantlyUI : MonoBehaviour
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
                if (gameObject.GetComponent<Image>())
                {
                    gameObject.GetComponent<Image>().color = colors[0];
                    //gameObject.GetComponent<Image>().DOColor(colors[0],changeTime);
                }
                else
                {
                    gameObject.GetComponent<TextMeshProUGUI>().color = colors[0];
                    //gameObject.GetComponent<TextMeshProUGUI>().DOColor(colors[0],changeTime);
                }

                break;
            case 1:
                if (gameObject.GetComponent<Image>())
                {
                    gameObject.GetComponent<Image>().color = colors[1];

                    //gameObject.GetComponent<Image>().DOColor(colors[1],changeTime);
                }
                else
                {
                    gameObject.GetComponent<TextMeshProUGUI>().color = colors[1];
                    //gameObject.GetComponent<TextMeshProUGUI>().DOColor(colors[1],changeTime);
                }

                break;
            case 2:

                if (gameObject.GetComponent<Image>())
                {
                    gameObject.GetComponent<Image>().color = colors[2];
                    //gameObject.GetComponent<Image>().DOColor(colors[2],changeTime);
                }
                else
                {
                    gameObject.GetComponent<TextMeshProUGUI>().color = colors[2];
                    //gameObject.GetComponent<TextMeshProUGUI>().DOColor(colors[2],changeTime);
                }

                break;
            case 3:
                if (gameObject.GetComponent<Image>())
                {
                    gameObject.GetComponent<Image>().color = colors[3];
                    //gameObject.GetComponent<Image>().DOColor(colors[3],changeTime);
                }
                else
                {
                    gameObject.GetComponent<TextMeshProUGUI>().color = colors[3];
                    //gameObject.GetComponent<TextMeshProUGUI>().DOColor(colors[3],changeTime);
                }

                break;
        }
    }
}