using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SettingsAnimationActivation : MonoBehaviour
{

    private enum AnimStates
    {
        Bend, Look, Jitter
    };

    private CinemachineVirtualCamera settingsCam;
    private Animator anim;
    private float lastChangeTime;
    private AnimStates currState;
    // Start is called before the first frame update
    void Start()
    {
        settingsCam = transform.parent.Find("SettingsVCam").gameObject.GetComponent<CinemachineVirtualCamera>();
        anim = transform.parent.Find("ybot@SittingBend").gameObject.GetComponent<Animator>();
        lastChangeTime = Time.time;
        currState = AnimStates.Look;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastChangeTime > 10f)
        {
            if(currState == AnimStates.Bend)
            {
                anim.SetTrigger("Jitter");
                currState = AnimStates.Jitter;
            }
            else if (currState == AnimStates.Look)
            {
                anim.SetTrigger("Bend");
                currState = AnimStates.Bend;
            }
            else if (currState == AnimStates.Jitter)
            {
                anim.SetTrigger("Look");
                currState = AnimStates.Look;
            }
            lastChangeTime = Time.time;
        }
    }
}
