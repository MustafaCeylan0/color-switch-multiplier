using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InLevelRewardManger : MonoBehaviour
{
    [SerializeField] private GameObject rewardButton;
    [SerializeField] private GameObject noRewardedPanel;


    /*public void rewardButtonClick()
    {
            Debug.Log("in level reward button is clicked");
            FindObjectOfType<AdScript>().ShowRewardedVideo(false);
            Invoke(nameof(disableRewardButton), .4f);
        

    }

    public void noRewardedPanelCloseButton()
    {
        noRewardedPanel.transform.DOScale(0, .3f).SetUpdate(true);
        Time.timeScale = 1;
    }*/


    public void rewardButtonClick()
    {
        FindObjectOfType<SFXManager>().buttonClick();
        if (FindObjectOfType<AdScript>().rewardedAd.IsLoaded())
        {
            FindObjectOfType<AdScript>().ShowRewardedVideo(false);
            Invoke(nameof(disableRewardButton), .4f);
        }
        else
        {
            Time.timeScale = 0;
            noRewardedPanel.transform.DOScale(1, .3f).SetUpdate(true);
        }
    }

    public void noRewardedPanelCloseButton()
    {
        noRewardedPanel.transform.DOScale(0, .3f).SetUpdate(true);
        Time.timeScale = 1;
    }


    private void disableRewardButton()
    {
        rewardButton.GetComponent<Button>().interactable = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rewardButton.transform.DOMoveX(rewardButton.transform.position.x - 500, 1f);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        disableRewardButton();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}