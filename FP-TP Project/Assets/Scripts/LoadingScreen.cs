using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    //PROPERTIES
    [SerializeField] private string sceneToLoad; //this makes it not be manually interchangeable...

    private AsyncOperation loading;
    private bool isReady = false;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text percentageText;
    [SerializeField] private Text loadingText;
    

    /// <summary>
    /// Gets the scene to load and assigns it. Single scene load mode so every other scenes are trashed.
    /// Doesn't allow scene to load (yet).
    /// </summary>
    public void Start()
    {
        System.GC.Collect();
        Time.timeScale = 1.0f;
        isReady = false;

        loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        loading.allowSceneActivation = false;
    }

    /// <summary>
    /// Takes the loading progression value and assigns it to the progressBar value to show the progression of the loading.
    /// When the loading progress is over 89% (as per Unity AsyncOperation problems) the loading flagged as ready, and prompts player to press space to continue.
    /// When player inputs "Submit" (Space or Enter), the game level is loaded.
    /// </summary>
    public void Update()
    {
        progressBar.value = loading.progress + 0.1f;
        percentageText.text = (loading.progress + 0.1f) * 100 + "%";

        if (loading.progress >= 0.89f)
        {
            isReady = true;
            loadingText.text = "Press Space To Continue";
        }

        if (isReady && Input.GetButtonDown("Submit"))
        {
            loading.allowSceneActivation = true;
        }
    }
}
