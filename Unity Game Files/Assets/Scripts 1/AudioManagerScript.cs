using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class AudioManagerScript :  MonoBehaviour
{
    public AudioMixerSnapshot splash;
    public AudioMixerSnapshot gameStart;
    public AudioClip[] soundClips;
    public AudioClip[] audioClip_UI;

    private AudioSource audioSource_UI;
    private AudioSource soundAudioSource;
    private GameObject currentButton;
    
    //UI SOUNDS
    //sound 0 -> hover
    //sound 1 -> click
    //sound 2 -> back
    //sound 3 -> UI change on

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        if(GameObject.FindObjectsOfType<AudioManagerScript>().Length > 1)
        {
            AudioManagerScript[] g = GameObject.FindObjectsOfType<AudioManagerScript>();
            for(int i=1; i<g.Length;i++)
            {
                Destroy(g[i].gameObject);
            }
        }
        soundAudioSource = GetComponent<AudioSource>();
        audioSource_UI = transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Maze Generation")
        {
            if(!soundAudioSource.isPlaying)
            {
                int rand = Random.Range(1, soundClips.Length - 1);
                soundAudioSource.clip = soundClips[rand];
                soundAudioSource.Play();
            }
        }
    }

    public void PlaySoundOnMouseHover_AudioManager()
    {
        audioSource_UI.clip = audioClip_UI[0];
        audioSource_UI.Play();
    }

    public void PlaySoundOnMouseClick_AudioManager()
    {
        audioSource_UI.clip = audioClip_UI[1];
        audioSource_UI.Play();
    }

    public void PlaySoundOnBackButton_AudioManager()
    {
        audioSource_UI.clip = audioClip_UI[2];
        audioSource_UI.Play();
    }

    public void PlaySoundOnKeyBindingClick_AudioManager()
    {
        audioSource_UI.clip = audioClip_UI[3];
        audioSource_UI.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
       if(scene.name == "MainMenu")
        {

            soundAudioSource = GetComponent<AudioSource>();
            if (soundAudioSource.isPlaying)
            {
                soundAudioSource.Stop();
            }
            soundAudioSource.clip = soundClips[0];
            soundAudioSource.Play();
            gameStart.TransitionTo(6f);
        }

       if(scene.name == "Maze Generation")
        {
            soundAudioSource = GetComponent<AudioSource>();
            if (soundAudioSource.isPlaying)
            {
                soundAudioSource.Stop();
            }
            int rand = Random.Range(1, soundClips.Length - 1);
            soundAudioSource.clip = soundClips[rand];
            soundAudioSource.Play();
        }

        if (scene.name == "Win" || scene.name == "GameOver")
        {
            soundAudioSource = GetComponent<AudioSource>();
           
        }
    }
}
