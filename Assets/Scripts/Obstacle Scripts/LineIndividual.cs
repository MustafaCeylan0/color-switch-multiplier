using System;
using DG.Tweening;
using UnityEngine;

public class LineIndividual : MonoBehaviour
{
    [SerializeField] private GameObject parentGO; 
    private float parentXPos;
    [SerializeField] private float numberOfLines;

    private float scaleX;

    private float xEndPos;

    private float xStartPos;
    // Start is called before the first frame update
    void Start()
    {
        parentXPos = parentGO.transform.position.x;
        double rotation = transform.rotation.z;
        scaleX = transform.localScale.x ;
        xEndPos = parentXPos + (numberOfLines / 2) * scaleX;
        xStartPos = parentXPos - (numberOfLines / 2) * scaleX;
        //parentXPos = gameObject.transform.parent.position.x;
        // Debug.Log("parent line obstacle pos: " + parentXPos);
        //move();

    }

    /// Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime,Space.World);
        if (gameObject.transform.position.x >= parentXPos + (numberOfLines / 2) * scaleX)
        {
            gameObject.transform.position = new Vector3(parentXPos - (numberOfLines / 2) * scaleX, transform.position.y);
        }
        
    }

    /*private void move()
    {
        transform.DOMoveX(xEndPos,1f);
        Invoke(nameof(backStoStartPos),1f);
    }

    private void backStoStartPos()
    {
        transform.position = new Vector3(xStartPos, transform.position.y, 0);
        move();
    }*/
}
