using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class QuitSceneAnimationActivation : MonoBehaviour
{
    private CinemachineVirtualCamera quitCam;
    private Animator anim;
    private bool offScreen = true;

    // Start is called before the first frame update
    void Start()
    {
        quitCam = transform.parent.Find("Quit VCam").gameObject.GetComponent<CinemachineVirtualCamera>();        
        anim = transform.parent.Find("ybot@Shrugging").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(quitCam.Priority == 100 && offScreen)
        {
            anim.SetTrigger("OnScreen");
            offScreen = false;
        }
        else if(quitCam.Priority == 5 && !offScreen) 
        {
            anim.SetTrigger("OffScreen");
            offScreen = true;
        }
        


    }
}
