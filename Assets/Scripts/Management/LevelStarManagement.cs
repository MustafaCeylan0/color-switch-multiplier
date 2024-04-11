using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarManagement : MonoBehaviour
{
    
    void Start()
    {
        
        
        // Check if the level is current level, if so make the stars collectable by not disabling them
        int num = (int) SceneManager.GetActiveScene().name[6] - '0';
        if (num != PlayerPrefs.GetInt("ClassicCurrentLevel"))
        {
            gameObject.SetActive(false);
        }
    }


}
