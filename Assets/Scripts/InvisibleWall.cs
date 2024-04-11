using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
     private GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    {
        if (player)
        {
            transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y);
        }
        
    }
}