using System.Collections;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{

    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private float gameMusicLoopCycleTime;
    private AudioSource audioSourceMain;
    void Awake()
    {
        setupSingleton();
        audioSourceMain = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        StartCoroutine(loopGameMusic());
        
    }

    private void setupSingleton()
    {
        int numberOfSoundManagers = FindObjectsOfType<SoundManagement>().Length;
        if (numberOfSoundManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    private IEnumerator loopGameMusic()
    {
        while (true)
        {
            audioSourceMain.PlayOneShot(gameMusic);
            yield return new WaitForSeconds(gameMusicLoopCycleTime);
            audioSourceMain.Stop();
        }
    }
    public float changeMusicVolume()
    {
        if ((int)GetComponent<AudioSource>().volume == 1)
        {
            GetComponent<AudioSource>().volume = 0;
            return 0;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
            return 1;
        }
        
    }

}