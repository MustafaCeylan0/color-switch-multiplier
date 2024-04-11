using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject[] finishParticles;
    private bool isActive = true;
    [SerializeField] private TextMeshPro finishLineText1; //digit 1
    [SerializeField] private TextMeshPro finishLineText2; //digit 2

    [SerializeField]
    [Tooltip("the amount of time that the finishing level will be delayed after player passing the finish line")]
    private float levelFinishDelay = 2f;

    [SerializeField] private int levelFinishRequirement;
    public int neededPlayerAmount;


    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            Player[] players = FindObjectsOfType<Player>();
            StartCoroutine(fireLevelFinished());
            isActive = false;

            foreach (var player in players)
            {
                StartCoroutine(player.levelFinished());

            }
            StartCoroutine(FindObjectOfType<Player>().GetComponent<Player>().levelFinished());
        }
    }*/


    private IEnumerator fireLevelFinished()
    {
        foreach (var player in FindObjectsOfType<Player>())
        {
            if (player.CompareTag("Player") && player.GetComponent<Player>().isControlsActive)
            {
                player.GetComponent<Player>().isControlsActive = false;
                StartCoroutine(player.GetComponent<Player>().levelFinished());
            }
        }

        float cameraY = Camera.main.transform.position.y;

        FindObjectOfType<SFXManager>().levelSuccesfulFirst();
        foreach (var finishParticle in finishParticles)
        {
            Instantiate(finishParticle, new Vector3(Random.Range(-4, 4), Random.Range(cameraY - 3, cameraY + 3), 0),
                Quaternion.identity);
        }

        yield return new WaitForSeconds(levelFinishDelay / 3f);
        yield return new WaitForSeconds(levelFinishDelay / 3f);

        //updateStars();
        yield return new WaitForSeconds(levelFinishDelay / 3f);
        //Level 1
        //0123456
        //Find the scene index and set it to complete in the game session
        string[] sceneNameArray = SceneManager.GetActiveScene().name.Split(' ');
        int num = Int32.Parse(sceneNameArray[1]);


        FindObjectOfType<GameSession>().classicLevelPassed(num);
        FindObjectOfType<InLevelUI>().GetComponent<InLevelUI>().levelCompleted();
    }

    // call the level stars management script for the stars to be added to the inventory

    /*private void updateStars()
    {
        int num = (int) SceneManager.GetActiveScene().name[6] - '0';
        if (num == PlayerPrefs.GetInt("ClassicCurrentLevel"))
        {
            FindObjectOfType<InLevelUI>().updateTotalStars();
        }
    }*/

    private void Start()
    {
        setFinishLineText(false);
        neededPlayerAmount = levelFinishRequirement;
    }

    void setFinishLineText(bool isFinished)
    {
        if (!isFinished)
        {
            if (levelFinishRequirement >= 10)
            {
                finishLineText1.text = "" + (levelFinishRequirement / 10);
                finishLineText2.text = "" + (levelFinishRequirement % 10);
            }
            else
            {
                finishLineText1.text = "0";
                finishLineText2.text = "" + levelFinishRequirement;
            }
        }
        else
        {
            finishLineText1.text = "0";
            finishLineText2.text = "0";
            FindObjectOfType<LevelManagement>().levelStatus = "finished";
        }
    }

    public void updateFinishLineText()
    {
        levelFinishRequirement--;
        if (levelFinishRequirement <= 0)
        {
            setFinishLineText(true);
            StartCoroutine(fireLevelFinished());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            setFinishLineText(false);
        }
    }
}