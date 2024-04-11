using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsMeter : MonoBehaviour
{
    public TextMeshProUGUI fpsTMP;
    public float deltaTime;
 
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsTMP.text = Mathf.Ceil (fps).ToString ();
    }
}
