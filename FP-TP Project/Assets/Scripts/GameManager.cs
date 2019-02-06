using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //PROPERTIES
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private GameObject soundManager;
    [SerializeField] private Canvas endLevelCanvas;
    private bool isLevelDone = false;

	void Start () {
        Input.ResetInputAxes();
        Cursor.lockState = CursorLockMode.Locked; //this will not fix level 2: only works if you pause and then unpause during level 2 and it will function normally
        Cursor.visible = false;

        //start with progression bar at 0%
        progressBar.value = 0.0f;

        pauseCanvas.enabled = false;
        endLevelCanvas.enabled = false;
        isLevelDone = false;
    }
	
    /// <summary>
    /// Verify that the current scene is the game, by getting the active scene.
    /// At any time during the game, the player can pause.
    /// Time pauses.
    /// </summary>
	void Update () {
        AllowPause();
        CompleteLevel();
    }

    public void AllowPause()
    {
        if (Input.GetButtonDown("Cancel") && !isLevelDone)
        {
            pauseCanvas.GetComponent<Animation>().Play("InvokePause");
            //pauseCanvas.GetComponent<AudioSource>().Play();
            //soundManager.GetComponent<AudioSource>().Pause();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseCanvas.enabled = true;
            Time.timeScale = 0.0f;

        }
    }

    /// <summary>
    /// Unpause, time back to normal.
    /// </summary>
    public void Unpause()
    {
        if (pauseCanvas.isActiveAndEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseCanvas.GetComponent<Animation>().Play("HidePause");
            pauseCanvas.enabled = false;
            Time.timeScale = 1.0f;
            LevelProgress();
        }
    }


    
    /// <summary>
    /// When Restart() is invoked, it unpauses the game. To be used with ReloadLevel(). To optimize.
    /// </summary>
    public void Restart()
    {
        Unpause();
    }


    //PROPERTIES
    [SerializeField] private Slider progressBar;
    [SerializeField] private float progression = 0.05f;
    /// <summary>
    /// ProgressionBar is increased.
    /// Pauses when pause is invoked.
    /// </summary>
    public void LevelProgress()
    {
        if (!pauseCanvas.enabled)
        {
            progressBar.value += progression;
        }

        if (pauseCanvas.isActiveAndEnabled)
        {
            progressBar.value = progressBar.value;
        }
    }


    /// <summary>
    /// Decreases progress when the Target is shot while (isWorking). Prevents shooting abuse.
    /// </summary>
    public void DecreaseProgress()
    {
        progressBar.value -= (progression * 1000.0f);
    }

    /// <summary>
    /// Displays the level completed dialog. To use with SceneManagement.LoadLevel()
    /// End value is the level difficulty (not set)
    /// </summary>
    public void CompleteLevel()
    {
        if (progressBar.value >= 10000)
        {
            Time.timeScale = 0.0f;
            endLevelCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isLevelDone = true;
        }
    }


    //public void GameOver()
    //{
    //    //count how many humans, if 0, then game over :(
    //}
}
