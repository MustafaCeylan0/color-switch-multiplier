using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject SFXButton;
    [SerializeField] private Sprite musicActiveImage;
    [SerializeField] private Sprite musicDeActiveImage;
    [SerializeField] private Sprite SFXActiveImage;
    [SerializeField] private Sprite SFXDeActiveImage;

    void Start()
    {
    }

    private void Awake()
    {
        setupSingleton();

        gameObject.SetActive(false);

    }

    private void setupSingleton()
    {
        int numberOfSettingsPanel = FindObjectsOfType<SettingsPanel>().Length;
        if (numberOfSettingsPanel > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public void musicButtonClick()
    {
        //change to the other sprite
        if ((int)FindObjectOfType<SoundManager>().changeMusicVolume() == 1)
        {
            musicButton.GetComponent<Image>().sprite = musicActiveImage;
            
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicDeActiveImage;
        }
    }

    /*public void SFXButtonClick()
    {
        //change to the other sprite
        if ((int)FindObjectOfType<SFXManager>().changeSFXVolume() == 1)
        {
            SFXButton.GetComponent<Image>().sprite = SFXActiveImage;
        }
        else
        {
            SFXButton.GetComponent<Image>().sprite = SFXDeActiveImage;
        }
    }*/
        public void settingsCloseButtonClick()
        {
           gameObject.SetActive(false);
        }
}