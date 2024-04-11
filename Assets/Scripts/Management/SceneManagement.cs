using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private float delayAfterLevelEnding = 1.2f;


    [Header("Transition Parts")] [SerializeField]
    private GameObject left;

    [SerializeField] private GameObject right;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 upPos;
    private Vector3 downPos;
    private int distance;

    public void loadStartMenu()
    {
        animateTransition(true, 1);
        StartCoroutine(loadStartMenuCoroutine());
    }

    private IEnumerator loadStartMenuCoroutine()
    {
        yield return new WaitForSeconds(delayAfterLevelEnding);
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Scene",LoadSceneMode.Single);
        setVolumeSprites();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            distance = 10;
        }
        else
        {
            distance = 1000;
        }
    }

    public void restartCurrentLevel()
    {
        animateTransition(true, .3f);
        StartCoroutine(restartCurrentLevelCoroutine(.4f));
    }

    private IEnumerator restartCurrentLevelCoroutine(float delay)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
        setVolumeSprites();
    }


    public void loadLevelWithIndex(int levelIndex)
    {
        animateTransition(true, 1);
        StartCoroutine(loadLevelWithIndexCoroutine(levelIndex));
    }

    private IEnumerator loadLevelWithIndexCoroutine(int levelIndex)
    {
        yield return new WaitForSeconds(delayAfterLevelEnding);
        //Check if Level is available
        Time.timeScale = 1;
        if (FindObjectOfType<GameSession>().currentLevelIndex >= levelIndex)
        {
            String sceneName = "Level " + levelIndex;
            SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
            setVolumeSprites();
        }
        else
        {
            Debug.Log("level " + levelIndex + " is not reached yet.");
        }
    }


    private void Start()
    {
        if (left)
        {
            leftPos = left.transform.position;
            rightPos = right.transform.position;
        }


        if (up)
        {
            upPos = up.transform.position;
            downPos = down.transform.position;
        }


        animateTransition(false, 1);
        // StartCoroutine(startCoroutine());
    }


    private void animateTransition(bool close, float duration)
    {
        if (!close)
        {
            if (left)
            {
                left.transform.DOMoveX(leftPos.x - distance, duration*2).SetUpdate(true);
                right.transform.DOMoveX(rightPos.x + distance, duration*2).SetUpdate(true);
            }


            if (up)
            {
                up.transform.DOMoveY(upPos.y + 1600, duration).SetUpdate(true);
                down.transform.DOMoveY(downPos.y - 1600, duration).SetUpdate(true);
            }
        }
        else
        {
            if (left)
            {
                left.transform.DOMoveX(leftPos.x, duration).SetUpdate(true);
                right.transform.DOMoveX(rightPos.x, duration).SetUpdate(true);
            }


            if (up)
            {
                up.transform.DOMoveY(upPos.y, duration).SetUpdate(true);
                down.transform.DOMoveY(downPos.y, duration).SetUpdate(true);
            }
        }
    }

    public void loadNextLevel()
    {
        StartCoroutine(loadNextLevelCoroutine());
    }

    private IEnumerator loadNextLevelCoroutine()
    {
        animateTransition(true, 1);


        yield return new WaitForSeconds(delayAfterLevelEnding);
        Time.timeScale = 1;

        string[] sceneNameArray = SceneManager.GetActiveScene().name.Split(' ');
        int num = Int32.Parse(sceneNameArray[1]);
        
        
        loadLevelWithIndex(num + 1);
        setVolumeSprites();
    }

    private void setVolumeSprites()
    {
        StartCoroutine(FindObjectOfType<GameSession>().setVolumeSprites()
        );
    }

    public void loadSceneWithName(String sceneName)
    {
        StartCoroutine(loadSceneWithNameCoroutine(sceneName));
    }
    
    
    private IEnumerator loadSceneWithNameCoroutine(String sceneName)
    {
        Time.timeScale = 1;
        animateTransition(true, 1);


        yield return new WaitForSeconds(delayAfterLevelEnding);
        SceneManager.LoadScene(sceneName);
    }
}