using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChangerManagerTMP : MonoBehaviour
{
    
    [SerializeField]private TextMeshPro[] _textMeshPros;
    [SerializeField] private GameObject[] zigzagParents;
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
        int startIndex = 0;
        foreach (var  tmp in _textMeshPros)
        {
            StartCoroutine(changeColor(tmp,startIndex));
            startIndex++;
        }

        foreach (var zigzagParent in zigzagParents)
        {
            StartCoroutine(doZigzag(zigzagParent));
        }
        
    }

    IEnumerator changeColor(TextMeshPro textMeshPro, int index)
    {
       
        while (true)
        {
            if (index > 3)
            {
                index = 0;
                
            }
            textMeshPro.color = colors[index];
            index++;
            

            yield return new WaitForSeconds(changeTime);
        }
    }

    IEnumerator doZigzag(GameObject gameObject)
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