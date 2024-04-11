using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject[] levelLockers;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TextMeshPro starsTMP;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject noAdPanel;


    [Header("Settings Panel")] [SerializeField]
    private GameObject musicButton;

    [SerializeField] private GameObject SFXButton;
    [SerializeField] private Sprite musicActiveImage;
    [SerializeField] private Sprite musicDeActiveImage;
    [SerializeField] private Sprite SFXActiveImage;
    [SerializeField] private Sprite SFXDeActiveImage;
    private bool settingsActive = false;
    
    
    

    private void Awake()
    {
        setSpritesCorrect();
        //setLevelLockers();
    }

    private void setLevelLockers()
    {
        Debug.Log("current level is " + FindObjectOfType<GameSession>().currentLevelIndex);
        for (int i = 0; i < FindObjectOfType<GameSession>().currentLevelIndex; i++)
        {
            levelLockers[i].SetActive(false);
        }
    }

    private void Start()
    {
        setStarUi();
    }

    public void setStarUi()
    {
        starsTMP.text = PlayerPrefs.GetInt("Total Stars").ToString();
    }

    public void playButtonClick()
    {
        //take the current level int value from game session object and start the "Level" + index scene
        int levelToLoadIndex = FindObjectOfType<GameSession>().currentLevelIndex;
        FindObjectOfType<SceneManagement>().loadLevelWithIndex(levelToLoadIndex);
    }

    /*public void endlessButtonClick()
    {
        
    }*/


    public void levelsButtonClick()
    {
        levelSelectionPanel.SetActive(true);
    }

    public void levelsCloseButtonClick()
    {
        levelSelectionPanel.SetActive(false);
    }

    public void settingsButtonClick()
    {
        //settingsPanel.SetActive(true);
        if (!settingsActive)
        {
            settingsPanel.transform.DOMoveX(settingButton.transform.position.x, .3f);
            settingsActive = true;
        }
        else
        {
            settingsPanel.transform.DOMoveX(settingButton.transform.position.x + 2, .3f);
            settingsActive = false;
        }
    }

    public void settingsCloseButtonClick()
    {
        settingsPanel.SetActive(false);
    }

    public void loadLevelButtonClick(int levelIndex)
    {
        FindObjectOfType<SceneManagement>().GetComponent<SceneManagement>().loadLevelWithIndex(levelIndex);
    }

    public void musicButtonClick()
    {
        //change to the other sprite
        FindObjectOfType<MusicManager>().changeMusicVolume(-1);
        if (FindObjectOfType<MusicManager>().getMusicVolume())
        {
            if (musicButton.GetComponent<SpriteRenderer>())
            {
                musicButton.GetComponent<SpriteRenderer>().sprite = musicActiveImage;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = musicActiveImage;
            }

            PlayerPrefs.SetInt("musicStatus", 1);
        }
        else
        {
            if (musicButton.GetComponent<SpriteRenderer>())
            {
                musicButton.GetComponent<SpriteRenderer>().sprite = musicDeActiveImage;
            }
            else
            {
                musicButton.GetComponent<Image>().sprite = musicDeActiveImage;
            }

            PlayerPrefs.SetInt("musicStatus", 0);
        }
    }

    public void SFXButtonClick()
    {
        //change to the other sprite
        FindObjectOfType<SFXManager>().changeSFXVolume(-1);
        if (FindObjectOfType<SFXManager>().getSFXVolume())
        {
            if (SFXButton.GetComponent<SpriteRenderer>())
            {
                SFXButton.GetComponent<SpriteRenderer>().sprite = SFXActiveImage;
            }
            else
            {
                SFXButton.GetComponent<Image>().sprite = SFXActiveImage;
            }

            PlayerPrefs.SetInt("SFXStatus", 1);
        }
        else
        {
            if (SFXButton.GetComponent<SpriteRenderer>())
            {
                SFXButton.GetComponent<SpriteRenderer>().sprite = SFXDeActiveImage;
            }
            else
            {
                SFXButton.GetComponent<Image>().sprite = SFXDeActiveImage;
            }

            PlayerPrefs.SetInt("SFXStatus", 0);
        }
    }

    public void quitButtonClick()
    {
        Application.Quit();
    }


    public void changeMusicStatus(bool isActive)
    {
        if (isActive)
        {
            musicButton.GetComponent<Image>().sprite = musicActiveImage;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicDeActiveImage;
        }
    }

    public void changeSFXStatus(bool isActive)
    {
        if (isActive)
        {
            SFXButton.GetComponent<Image>().sprite = SFXActiveImage;
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXDeActiveImage;
        }
    }

    public void setSpritesCorrect()
    {
        if (PlayerPrefs.GetInt("musicStatus") == 1)
        {
            musicButton.GetComponent<Image>().sprite = musicActiveImage;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicDeActiveImage;
        }

        if (PlayerPrefs.GetInt("SFXStatus") == 1)
        {
            SFXButton.GetComponent<Image>().sprite = SFXActiveImage;
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXDeActiveImage;
        }
    }

    public void loadSceneWithName(String sceneName)
    {
        FindObjectOfType<SceneManagement>().loadSceneWithName(sceneName);
    }

    public void shopButtonClick()
    {
        shopPanel.SetActive(true);
        shopPanel.transform.DOScale(1, .4f);
        if (settingsActive)
        {
            settingsButtonClick();
        }
    }

    public void shopButtonCloseClick()
    {
        Invoke(nameof(shopDEActivate), .4f);

        shopPanel.transform.DOScale(0, .4f);
    }


    private void shopDEActivate()
    {
        shopPanel.SetActive(false);
    }

    public void rateUs(String url)
    {//https://play.google.com/store/apps/details?id=com.HyperMasterGames.ColorControl
        Application.OpenURL(url);
    }

    public void rewardForStarsButton()
    {
        if (FindObjectOfType<AdScript>().rewardedAd.IsLoaded())
        {
            FindObjectOfType<AdScript>().ShowRewardedVideo(true);
        }
        else
        {
            Time.timeScale = 0;
            noAdPanel.transform.DOScale(1, .3f).SetUpdate(true);
        }
    }

    public void noAdRightNowCloseButton()
    {
        Time.timeScale = 1;
        noAdPanel.transform.DOScale(0, .3f).SetUpdate(true);
    }
}