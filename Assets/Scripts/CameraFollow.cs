using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private GameObject finishLineGO;
    [SerializeField] [CanBeNull] private GameObject controlParent;


    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject.transform;
        finishLineGO = FindObjectOfType<FinishLine>().gameObject;
    }

    void Update()
    {
        
        //OPEN COMMENTS FOR TURNING BACK INTO NORMAL
        //if (player)
        //{
        //    if (player.position.y > transform.position.y &&
        //        transform.position.y < (finishLineGO.transform.position.y - 2))
        //    {
        //        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        //    }
        //}
        //else
        //{
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0 &&
                FindObjectOfType<LevelManagement>().levelStatus != "finished")
            {
//                Debug.Log("Couldn't find player");
            }
            else
            {

                float ySum = 0;
                foreach (var remainingPlayer in GameObject.FindGameObjectsWithTag("Player"))
                {
                    ySum += remainingPlayer.transform.position.y;
                }
                float yMid = ySum /  GameObject.FindGameObjectsWithTag("Player").Length;

                if (transform.position.y + 1 < yMid)
                {

                    float moveTime = Math.Abs(yMid - transform.position.y) * 0.2f;
                    transform.DOMoveY(yMid, moveTime);
                }

                if (controlParent)
                {
                    controlParent.transform.position =new Vector2(controlParent.transform.position.x, transform.position.y);
                }


                /*float closestPos = 100;
                GameObject closestPlayer = null;
                foreach (var remainingPlayer in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (Math.Abs(remainingPlayer.transform.position.y - transform.position.y) < closestPos)
                    {
                        closestPos = remainingPlayer.transform.position.y;
                        closestPlayer = remainingPlayer;
                    }
                }

                if (closestPlayer)
                {
                    player = closestPlayer.transform;
                }*/
            }
        //}
    }
}