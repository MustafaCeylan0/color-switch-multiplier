using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private float musicVolumeLevel;


    public float changeMusicVolume()
    {
        if ((int)musicVolumeLevel == 1)
        {
            musicVolumeLevel = 0;
            GetComponent<AudioSource>().volume = 0;

            return 0;
        }
        else
        {
            musicVolumeLevel = 1;
            GetComponent<AudioSource>().volume = 1;

            return 1;
        }
        
    }



}
