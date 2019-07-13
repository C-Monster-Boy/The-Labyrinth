using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UISoundInitializer : MonoBehaviour
{
    private AudioManagerScript ams;

    // Start is called before the first frame update
    void Start()
    {
        ams = GameObject.FindObjectOfType<AudioManagerScript>();
    }

    public void PlaySoundOnMouseHover()
    {
        ams.PlaySoundOnMouseHover_AudioManager();
    }

    public void PlaySoundOnMouseClick()
    {
        ams.PlaySoundOnMouseClick_AudioManager();
    }

    public void PlaySoundOnBackButton()
    {
        ams.PlaySoundOnBackButton_AudioManager();
    }

    public void PlaySoundOnKeyBindingClick()
    {
        ams.PlaySoundOnKeyBindingClick_AudioManager();
    }


}
