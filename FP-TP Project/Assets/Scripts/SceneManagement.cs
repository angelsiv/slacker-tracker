using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    
    //PROPERTIES
    private AsyncOperation async;

    /// <summary>
    /// Default selected panel will be panel 0.
    /// </summary>
    public void Start()
    {
        TogglePanel(0);
    }

    /// <summary>
    /// Allows for loading a scene with its string name.
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        if (async == null)
        {
            Time.timeScale = 1.0f;
            async = SceneManager.LoadSceneAsync(scene);
            async.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// Returns the currentScene in order to restart level.
    /// </summary>
    /// <param name="scene"></param>
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        LoadScene(sceneName);
    }


    /// <summary>
    /// Exits the Unity editor or the game.
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //PROPERTIES
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultSelected;

    /// <summary>
    /// Scroll through panels until right one selected and becomes visible.
    /// Makes sure that the defaultSelected is the first one.
    /// </summary>
    /// <param name="panel"></param>
    public void TogglePanel(int panel)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(panel == i);

            if (panel == i)
            {
                defaultSelected[i].Select();
            }
        }
    }
}