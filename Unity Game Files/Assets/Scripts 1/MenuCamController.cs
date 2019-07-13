using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuCamController : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCams;

    public CinemachineVirtualCamera currActiveCam;

    // Start is called before the first frame update
    void Start()
    {
        ChangePriority(0);
    }

   public void ChangePriority(int currCam)
    {
        foreach (CinemachineVirtualCamera c in virtualCams)
        {
            c.Priority = 5;
        }
        virtualCams[currCam].Priority = 100;
        currActiveCam = virtualCams[currCam];
    }

    public CinemachineVirtualCamera GetCurrActiveCamera()
    {
        return currActiveCam;
    }
}
