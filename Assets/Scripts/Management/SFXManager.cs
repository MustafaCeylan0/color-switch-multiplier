using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private bool sfxActive;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip paintSound;
    [SerializeField] private AudioClip switchSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip levelFailedSound;
    [SerializeField] private AudioClip multiplySound;
    [SerializeField] private AudioClip additionSound;
    [SerializeField] private AudioClip levelSuccessfulSound;
    [SerializeField] private AudioClip levelSuccessFirstSound;


    private AudioSource sfxAudioSource;


    private void Awake()
    {
        //setupSingleton();
        sfxAudioSource = GetComponent<AudioSource>();
        if (sfxAudioSource.volume > 0.9)
        {
            sfxActive = true;
        }
        else
        {
            sfxActive = false;
        }
    }

    private void setupSingleton()
    {
        int numberOfSFXManagers = FindObjectsOfType<SFXManager>().Length;
        if (numberOfSFXManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void paintPlayer()
    {
        sfxAudioSource.PlayOneShot(paintSound);
    }

    public void levelSuccesfulFirst()
    {
        sfxAudioSource.PlayOneShot(levelSuccessFirstSound);
    }


    public void buttonClick()
    {
        sfxAudioSource.PlayOneShot(buttonClickSound);
    }

    public void jump()
    {
        if (FindObjectOfType<LevelManagement>().levelStatus == "play")
            sfxAudioSource.PlayOneShot(jumpSound);
    }

    public void volumeSwitch()
    {
        sfxAudioSource.PlayOneShot(switchSound);
    }

    public void death()
    {
        sfxAudioSource.PlayOneShot(deathSound);
    }

    public void levelFail()
    {
        sfxAudioSource.PlayOneShot(levelFailedSound);
    }

    public void multiply()
    {
        sfxAudioSource.PlayOneShot(multiplySound);
    }

    public void addition()
    {
        sfxAudioSource.PlayOneShot(additionSound);
    }

    public void levelSuccessful()
    {
        sfxAudioSource.PlayOneShot(levelSuccessfulSound);
    }

    public void changeSFXVolume(int status) // insert: -1 if u want to change, 1 for activating 0 for deactivating 
    {
        if (status == -1)
        {
            if ((int) GetComponent<AudioSource>().volume == 1)
            {
                GetComponent<AudioSource>().volume = 0;
                sfxActive = false;
                PlayerPrefs.SetInt("SFXStatus", 0);
            }
            else
            {
                GetComponent<AudioSource>().volume = 1;
                sfxActive = true;
                PlayerPrefs.SetInt("SFXStatus", 1);
            }
        }
        else if (status == 1)
        {
            GetComponent<AudioSource>().volume = 1;
            sfxActive = true;
            PlayerPrefs.SetInt("SFXStatus", 1);
        }
        else if (status == 0)
        {
            GetComponent<AudioSource>().volume = 0;
            sfxActive = false;
            PlayerPrefs.SetInt("SFXStatus", 0);
        }
    }

    public bool getSFXVolume()
    {
        return sfxActive;
    }
}