using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class InLevelUI : MonoBehaviour
{
    //[SerializeField] private GameObject pausePanel;
    [Header("Level Successful")] [SerializeField]
    private GameObject levelFinishedPanel;

    [SerializeField] private TextMeshProUGUI levelFinishedTMP;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button homeButton;

    [Header("Settings Panel")] [SerializeField]
    private GameObject pauseButton;

    [SerializeField] private Sprite pauseImage;
    [SerializeField] private Sprite unPauseImage;
    private bool isPaused = false;
    [SerializeField] private GameObject moveAblePausePanel;

    [SerializeField] private GameObject musicButton;

    [SerializeField] private GameObject SFXButton;
    [SerializeField] private Sprite musicActiveImage;
    [SerializeField] private Sprite musicDeActiveImage;
    [SerializeField] private Sprite SFXActiveImage;
    [SerializeField] private Sprite SFXDeActiveImage;

    [Header("Stars")] private int totalStarsGathered = 0;
    [SerializeField] private TextMeshProUGUI starsTMP;


    private void Awake()
    {
        int num = (int) SceneManager.GetActiveScene().name[6] - '0';
    }

    private void Start()
    {
        // SET SETTINGS PANEL ICONS
        setSpritesCorrect();
    }


    public void pauseButtonClick()
    {
        if (FindObjectOfType<LevelManagement>().levelStatus.Equals("play"))
        {
            FindObjectOfType<SFXManager>().buttonClick();
            if (!isPaused)
            {
                moveAblePausePanel.transform.DOMoveX(pauseButton.transform.position.x, .5f).SetUpdate(true);

                Time.timeScale = 0;
                isPaused = true;
                pauseButton.GetComponent<Image>().sprite = unPauseImage;
                //put moveable pause menu to view
                FindObjectOfType<PlayerParent>().isControlsActive = false;
                FindObjectOfType<LevelManagement>().levelStatus = "pause";
            }
        }
        else
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                pauseButton.GetComponent<Image>().sprite = pauseImage;

                //put moveable pause menu to out of view

                moveAblePausePanel.transform.DOMoveX(pauseButton.transform.position.x + 500f, .5f).SetUpdate(true);
                FindObjectOfType<PlayerParent>().isControlsActive = true;
                FindObjectOfType<LevelManagement>().levelStatus = "play";
            }
          
        }
    }

    /*public void resumeButtonClick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }*/


    //Completed level panel

    public void levelCompleted()
    {
        FindObjectOfType<SFXManager>().levelSuccessful();
        totalStarsGathered = FindObjectOfType<PlayerParent>().totalPlayerAmount;
        levelFinishedPanel.SetActive(true);
        int num = (int) SceneManager.GetActiveScene().name[6] - '0';

        if (num == PlayerPrefs.GetInt("ClassicCurrentLevel") - 1)
        {
            PlayerPrefs.SetInt("Total Stars", PlayerPrefs.GetInt("Total Stars") + totalStarsGathered);
        }

        levelFinishedPanel.transform.DOScale(1, .3f);
        StartCoroutine(setTheStars());
        nextLevelButton.interactable = false;
        homeButton.interactable = false;
        //Move the level finished panel to up
        levelFinishedTMP.text = SceneManager.GetActiveScene().name + " is completed.";
        FindObjectOfType<GameSession>().incrementInterstitialCounter(2);
    }

    private IEnumerator setTheStars()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i <= totalStarsGathered; i++)
        {
            starsTMP.text = i + "";
            yield return new WaitForSeconds(.02f);
        }

        nextLevelButton.interactable = true;
        homeButton.interactable = true;
    }


    public void musicButtonClick()
    {
        //change to the other sprite
        FindObjectOfType<MusicManager>().changeMusicVolume(-1);
        if (FindObjectOfType<MusicManager>().getMusicVolume())
        {
            musicButton.GetComponent<Image>().sprite = musicActiveImage;
            PlayerPrefs.SetInt("musicStatus", 1);
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicDeActiveImage;
            PlayerPrefs.SetInt("musicStatus", 0);
        }
    }

    public void SFXButtonClick()
    {
        //change to the other sprite
        FindObjectOfType<SFXManager>().changeSFXVolume(-1);
        if (FindObjectOfType<SFXManager>().getSFXVolume())
        {
            SFXButton.GetComponent<Image>().sprite = SFXActiveImage;
            PlayerPrefs.SetInt("SFXStatus", 1);
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXDeActiveImage;
            PlayerPrefs.SetInt("SFXStatus", 0);
        }
    }

    public void buttonClick()
    {
        if (PlayerPrefs.GetInt("SFXStatus") == 1)
        {
            FindObjectOfType<SFXManager>().buttonClick();
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


    /*public void openStarAmount()
    {
        collectedStarAmountInTheLevel++;
        starAmount.text = collectedStarAmountInTheLevel + "\\" + totalStarsInTheLevel;

        starAmountCanvas.transform.DOMoveY(2100, 1);
        Invoke(nameof(closeStarAmount), 3f);
    }*/

    /*private void closeStarAmount()
    {
        starAmountCanvas.transform.DOMoveY(3000, 1);
    }*/

    /*public void updateTotalStars()
    {
        PlayerPrefs.SetInt("Total Stars", PlayerPrefs.GetInt("Total Stars") + collectedStarAmountInTheLevel);
    }*/
}