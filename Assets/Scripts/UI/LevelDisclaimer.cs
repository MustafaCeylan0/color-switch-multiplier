using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisclaimer : MonoBehaviour
{
    [SerializeField]private TextMeshPro numb1;
    [SerializeField]private TextMeshPro numb2;
    [SerializeField]private TextMeshPro levelTMP;
    [SerializeField] float changeTime = .3f;
    [SerializeField] float zigzagPeriod = .5f;

    private Color[] colors = new[]
    {
        (Color) new Color32(221, 0, 0, 255), (Color) new Color32(157, 0, 229, 255),
        (Color) new Color32(0, 113, 207, 255), (Color) new Color32(185, 183, 0, 255)
    };

    private Color colorRed = new Color32(221, 0, 0, 255);
    private Color colorPurple = new Color32(157, 0, 229, 255);
    private Color colorGreen = new Color32(0, 113, 207, 255);
    private Color colorYellow = new Color32(185, 183, 0, 255);

    private void Start()
    {
        StartCoroutine(changeColor(levelTMP,0));

        StartCoroutine(changeColor(numb1,1));
        StartCoroutine(changeColor(numb2,2));
        StartCoroutine(doZigzag());
    }

    IEnumerator changeColor(TextMeshPro textMeshPro, int index)
    {
       
        while (true)
        {
            textMeshPro.color = colors[index];
            index++;
            if (index >= 4)
            {
                index = 0;
                
            }

            yield return new WaitForSeconds(changeTime);
        }
    }

    IEnumerator doZigzag()
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        while (true)
        {
            rigidbody2D.velocity = new Vector2(1, 2);
            yield return new WaitForSeconds(zigzagPeriod);
            rigidbody2D.velocity = new Vector2(-1, 2);
            yield return new WaitForSeconds(zigzagPeriod);

            

        }

    }
}