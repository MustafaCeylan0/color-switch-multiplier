using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Addition : MonoBehaviour
{
    private bool isActive = true;
    [SerializeField] private int addAmount;

    public void add(GameObject playerGO, Transform spawnTransform, Vector2 formerVelocity, Transform parentTransform)
    {
        if (isActive)
        {
            FindObjectOfType<SFXManager>().addition();

            for (int i = 0; i < addAmount; i++)
            {
                GameObject currentSpam = Instantiate(playerGO,
                    spawnTransform.position,
                    quaternion.identity);
                FindObjectOfType<PlayerParent>().totalPlayerAmount++;
                currentSpam.GetComponent<Rigidbody2D>().velocity = formerVelocity;
                currentSpam.transform.parent = parentTransform;
            }


            isActive = false;
        }
    }
}