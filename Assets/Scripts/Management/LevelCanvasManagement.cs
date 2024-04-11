using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvasManagement : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject levelFinishedPanel;
    [SerializeField] private TextMeshProUGUI levelFinishedTMP;
    
    

    //Pause Panel
    public void pauseButtonClick()
    {
        //Set the game time to 0 and activate the pause panel
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void resumeButtonClick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }


    //Failed level panel
    /*public void restartLevelButtonClick()
    {

    }*/


    //Completed level panel

    public void levelCompleted()
    {
        levelFinishedTMP.text = SceneManager.GetActiveScene().name + " is completed.";
        levelFinishedPanel.SetActive(true);
    }
    /*public void mainMenuButtonClick()
    {
        
    }*/

    /*public void nextLevelButtonClick()
    {
        
    }*/
}