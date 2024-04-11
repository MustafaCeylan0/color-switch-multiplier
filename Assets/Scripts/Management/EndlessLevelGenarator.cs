using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndlessLevelGenarator : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private float currentHeight = 0;
    [SerializeField] private float maxHeight = 0;

    [SerializeField] private float obstacleInterval = 10;
    [SerializeField] private int spawnedObstacles = 0; 
    private GameObject playerGO;
    [SerializeField] private TextMeshProUGUI currentMaxHeightTMP;
    [SerializeField] private TextMeshProUGUI maxHeightTMP;
    void Start()
    {
        playerGO = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGO)
        {
            currentHeight = playerGO.transform.position.y;

        }
        if (currentHeight > maxHeight)
        {
            maxHeight = currentHeight;
            currentMaxHeightTMP.text = maxHeight.ToString();
            PlayerPrefs.SetInt("CurrentMaxHeight", (int)maxHeight);
        }

        

        if (maxHeight > obstacleInterval*spawnedObstacles)
        {
            //spawn new obstacle
            Instantiate(obstacles[Random.Range(0, obstacles.Length)],
                new Vector2(0, obstacleInterval * (spawnedObstacles + 1 )), Quaternion.identity);
            spawnedObstacles = spawnedObstacles + 1;
        }
        
        
    }
}