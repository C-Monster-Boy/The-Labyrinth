using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Cinemachine;
public class MainMenuPlayerSounds : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float playDelay;
    public float initialPlayDelay;
    public CinemachineVirtualCamera thisVCam;

    private int currClipIndex;
    private MenuCamController mcc;
    private AudioSource audioSource;
    private bool inCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currClipIndex = 0;
        inCoroutine = true;
        StartCoroutine(WaitBeforeRepeating(audioClips[currClipIndex], initialPlayDelay));
    }

    // Start is called before the first frame update
    void Start()
    {
        mcc = GameObject.FindObjectOfType<MenuCamController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !inCoroutine)
        {
            inCoroutine = true;

            if (currClipIndex + 1 < audioClips.Length)
            {
                currClipIndex++;
            }
            else
            {
                currClipIndex = 0;
            }

            StartCoroutine(WaitBeforeRepeating(audioClips[currClipIndex], playDelay));
        }

        if (thisVCam != mcc.GetCurrActiveCamera())
        {
            audioSource.Pause();
        }
        else if(thisVCam == mcc.GetCurrActiveCamera() && !audioSource.isPlaying && !inCoroutine)
        {
            audioSource.Play();
        }

    }

    private IEnumerator WaitBeforeRepeating(AudioClip audioClip, float waitTime)
    {
        audioSource.clip = audioClip;
        yield return new WaitForSeconds(waitTime);
        audioSource.Play();

        inCoroutine = false;
    }
}
