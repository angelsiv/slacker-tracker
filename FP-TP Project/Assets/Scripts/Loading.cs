using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    private AsyncOperation async; //can recover status & properties such as getting a key before continuing to the next scene
    [SerializeField] private bool waitForUserInput = false;
    private bool ready = false;
    [SerializeField] private float delay = 0.0f; //delay of 0
    [SerializeField] private int sceneToLoad = -1; //we start counting at 0, but we can't start at 0, so if it's a negative value, it will load to the next scene

    /// <summary>
    /// ARRHG WHY WONT THIS WORK
    /// </summary>
    void Start()
    {
        Time.timeScale = 1.0f; //reset timescale
        Input.ResetInputAxes(); //if key is pressed from prev scene, they have been reset to be sure they are during the curr scene
        System.GC.Collect(); //clean the garbage in the memory from the previous scene are being removed to make space for the next loaded scene

        Scene currScene = SceneManager.GetActiveScene(); //check current scene
        async = SceneManager.LoadSceneAsync(currScene.buildIndex);// + 1); //increave value of currScene (+1) to move to the following scene
        async.allowSceneActivation = false; //don't move on to the next scene

        if (sceneToLoad == -1)
        {
            async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1); //load the following scene
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }

        if (waitForUserInput == false)
        {
            ready = true;
            Invoke("Activate", delay); //invoke is a counter that counts a function after the delay happens
        }
    }

    void Activate()
    {
        ready = true;
    }

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text txtPercent;

    void Update()
    {

        if (waitForUserInput && Input.anyKey)
        {
            if (async.progress >= 0.89f && SplashScreen.isFinished)
            {
                ready = true;
            }
        }

        if (progressBar) //if I have a progress bar image, if not, it will just skip it
        {
            progressBar.value = async.progress + 0.1f; //since it stops at 90%
        }

        if (txtPercent)
        {
            txtPercent.text = ((async.progress + 0.1f) * 100).ToString("") + " %";
        }

        if (async.progress >= 0.89f && SplashScreen.isFinished && ready) //for some reason, Unity does not increase to 100% but only to 90%, then it will load to the next scene
        {
            async.allowSceneActivation = true;
        }
    }
}
