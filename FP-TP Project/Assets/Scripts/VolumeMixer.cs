using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeMixer : MonoBehaviour {

    //PROPERTIES
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string nameOfMix;

    /// <summary>
    /// Gets the Slider component. Retrieves previously saved option.
    /// </summary>
    void Start()
    {
        Slider mSlider = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameOfMix);
    }

    /// <summary>
    /// Allows player to set volume.
    /// Saves chosen preferences.
    /// </summary>
    /// <param name="vol"></param>
    public void SetVolMixer(float vol)
    {
        Slider mSlider = GetComponent<Slider>();
        mixer.SetFloat(nameOfMix, vol);
        mSlider.value = vol;

        //Save it to player preferences
        PlayerPrefs.SetFloat(nameOfMix, vol);
        PlayerPrefs.Save();
    }



}
