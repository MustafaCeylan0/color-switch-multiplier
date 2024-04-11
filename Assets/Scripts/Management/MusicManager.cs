using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private float gameMusicLoopCycleTime;
    private AudioSource musicAudioSource;
    private bool musicActive;

    void Awake()
    {
        setupSingleton();
        musicAudioSource = GetComponent<AudioSource>();
        if (musicAudioSource.volume > 0.9)
        {
            musicActive = true;
        }
        else
        {
            musicActive = false;
        }
    }

    private void Start()
    {
        StartCoroutine(loopGameMusic());
    }

    private void setupSingleton()
    {
        int numberOfMusicManagers = FindObjectsOfType<MusicManager>().Length;
        if (numberOfMusicManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator loopGameMusic()
    {
        while (true)
        {
            musicAudioSource.PlayOneShot(gameMusic);
            yield return new WaitForSeconds(gameMusicLoopCycleTime);
            musicAudioSource.Stop();
        }
    }

    public void changeMusicVolume(int status)
    {
        if (status == -1)
        {
            if ((int) GetComponent<AudioSource>().volume == 1)
            {
                GetComponent<AudioSource>().volume = 0;
                musicActive = false;
                PlayerPrefs.SetInt("musicStatus",0);
            }
            else
            {
                GetComponent<AudioSource>().volume = 1;
                musicActive = true;
                PlayerPrefs.SetInt("musicStatus",1);

            }
        }
        else if (status == 1)
        {
            GetComponent<AudioSource>().volume = 1;
            musicActive = true;
            PlayerPrefs.SetInt("musicStatus",1);

        }
        else if (status == 0)
        {
            GetComponent<AudioSource>().volume = 0;
            musicActive = false;
            PlayerPrefs.SetInt("musicStatus",0);

        }
    }

    public bool getMusicVolume()
    {
        return musicActive;
    }
}