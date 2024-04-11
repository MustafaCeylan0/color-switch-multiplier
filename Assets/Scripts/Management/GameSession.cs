using System;
using System.Collections;
using GoogleMobileAds.Api;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int currentLevelIndex;
    [SerializeField] private GameObject[] playerObjects;
    [SerializeField] private int currentPlayerObjectIndex;
    private GameObject currentPlayerObject;
    private int numberOfFails;
    [SerializeField] private int rewardedStarAmount = 25;

    [Tooltip("After how many fails or level passing player will be shown an interstitial ad. (2 points for level passing, 1 for level failing)")] [SerializeField]
    private int interstitialAdInterval = 10;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    
    

    //GameSession is created once at the start of each running of the game;
    void Awake()
    {
        setupSingleton();
        MobileAds.Initialize(initStatus => { });
        
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");
            //Set the first sprite to be bought
            string key = "sprite " + 0 + " is bought";
            PlayerPrefs.SetInt(key, 1);
            PlayerPrefs.SetInt("Total Stars", 0);
            //Set everything to brand new
            currentLevelIndex = 0;
            numberOfFails = 0;
            PlayerPrefs.SetInt("fail counter", numberOfFails);
            PlayerPrefs.SetInt("Player Object Index", 0); // Test Only Now
            setPlayerSprite(PlayerPrefs.GetInt("Player Object Index"));
            


            PlayerPrefs.SetInt("ClassicCurrentLevel", 0);
            



            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            // Audio
            PlayerPrefs.SetInt("musicStatus", 1); //1 active
            PlayerPrefs.SetInt("SFXStatus", 1); //0 inactive

        }
        else
        {
            numberOfFails = PlayerPrefs.GetInt("fail counter");
            currentLevelIndex = PlayerPrefs.GetInt("ClassicCurrentLevel");
            //Do your stuff here
        }

        equalizeAudiosToPrefs();

        PlayerPrefs.Save();
        //load player prefs/////////////////////////////////////////////
    }


    //getting called if a level is passed
    //if the level passed is equal to the current level,increments the current level index by one
    public void classicLevelPassed(int levelIndex)
    {
        if (levelIndex >= PlayerPrefs.GetInt("ClassicCurrentLevel"))
        {
            currentLevelIndex = levelIndex + 1;

            PlayerPrefs.SetInt("ClassicCurrentLevel", currentLevelIndex);
//            Debug.Log("level " + levelIndex + " is passed and level " + levelIndex + 1 + "is opened.");
        }
        else
        {
            Debug.Log("level " + levelIndex + " is already passed.");
        }

        PlayerPrefs.Save();
    }


    private void setupSingleton()
    {
        //makes the object live for the next scenes
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator setVolumeSprites()
    {
        yield return new WaitForSeconds(0.5f);
//        Debug.Log("setting volume sprites at " + SceneManager.GetActiveScene().name + " scene");
        if (FindObjectOfType<StartMenuUI>())
        {
            FindObjectOfType<StartMenuUI>().setSpritesCorrect();
        }
        else if (FindObjectOfType<InLevelUI>())
        {
            FindObjectOfType<InLevelUI>().setSpritesCorrect();
        }
    }

    public void equalizeAudiosToPrefs()
    {
        FindObjectOfType<SFXManager>().changeSFXVolume(PlayerPrefs.GetInt("SFXStatus"));
        FindObjectOfType<MusicManager>().changeMusicVolume(PlayerPrefs.GetInt("musicStatus"));
    }

    public void setPlayerSprite(int index)
    {
        Debug.Log("Setting player index to " + index + ".");
        currentPlayerObject = playerObjects[index];
    }



    public void incrementInterstitialCounter(int amount)
    {
        numberOfFails += amount;
        PlayerPrefs.SetInt("fail counter", numberOfFails);
        if (numberOfFails >= interstitialAdInterval)
        {
            numberOfFails = 0;
            PlayerPrefs.SetInt("fail counter", numberOfFails);
            FindObjectOfType<AdScript>().ShowInterstitial();
        }
    }

    public void giveStarReward()
    {
        PlayerPrefs.SetInt("Total Stars",PlayerPrefs.GetInt("Total Stars")+rewardedStarAmount);
        FindObjectOfType<StartMenuUI>().setStarUi();
    }
}