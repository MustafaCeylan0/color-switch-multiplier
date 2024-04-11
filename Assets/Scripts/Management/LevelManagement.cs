using System;
using DG.Tweening;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public string levelStatus = "play";// play pause finished failed
    public GameObject playerGO;
    public Canvas levelCanvas;
    public GameObject gameOverPanel;

    public int latestPlayerID;

    private void Start()   
    {
        gameOverPanel.transform.localScale = Vector3.zero;
    }

    public void levelFailed()
    {
        Debug.Log("Level Failed");
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.DOScale(Vector3.one, 1f);
        levelStatus = "failed";
        FindObjectOfType<SFXManager>().levelFail();

        //calling fail occured method to check if it is time to show an interstitial ad
        FindObjectOfType<GameSession>().incrementInterstitialCounter(1);
    }

}
