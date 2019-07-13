using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class BirdsEye : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCam;
    public CinemachineVirtualCamera firstPersonCam;
    public CinemachineVirtualCamera midCam;
    public CinemachineVirtualCamera topCam;

    public float sensitivity;
    public bool birdsEyeCooldown;

    private float mouseX;
    private float mouseY;
    private float finalInputX;
    private float finalInputY;
    private float rotY;
    private float rotX;
    private GameObject portalLocatorBeam;

    public float birdsEyeTime = 300f;


    private Image birdsEyeBar;
    private bool birdsEye = false;
    private PlayerMovement pm;
    private Animator anim;
    private BreadCrumb brc;
    private Phase p;


    // Start is called before the first frame update
    void Start()
    {
        portalLocatorBeam = GameObject.Find("Portal(Clone)").transform.GetChild(6).gameObject;
        thirdPersonCam = GameObject.FindObjectOfType<CinemachineFreeLook>();
        thirdPersonCam.Priority = 50;
        firstPersonCam.Priority = 5;
        midCam.Priority = 5;
        topCam.Priority = 5;
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        p = GetComponent<Phase>();
        brc = GetComponent<BreadCrumb>();
        birdsEyeBar = GameObject.Find("BirdsEyeBar").transform.GetChild(1).GetComponent<Image>();
        birdsEyeBar.fillAmount = 0f; // 0 is full
        birdsEyeCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.EAGLE_EYE, "Space"))) && !birdsEye && !birdsEyeCooldown)
        {
            pm.enabled = false;
            anim.SetTrigger("Crouch");
            StartCoroutine(BirdsEyeActivate());
            birdsEye = true;
            brc.enabled = false;
            birdsEyeBar.fillAmount = 1f;
            portalLocatorBeam.SetActive(true);
        }

        if(birdsEye && !Phase.phaseCooldown)
        {
            DisableOtherPowers();
        }

        if(birdsEyeBar.fillAmount>0f && birdsEyeCooldown)
        {
            birdsEyeBar.fillAmount -= 0.0003f;
        }
        else if(birdsEyeCooldown)
        {
            birdsEyeCooldown = false;
        }
 

        if (topCam.Priority == 50)
        {
            //float inputX = Input.GetAxis("RightStickHorizontal");
            //float inputY = Input.GetAxis("RightStickVertical");

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            //finalInputX = inputX + mouseX;
            //finalInputY = inputY + mouseY;
            finalInputX =  mouseX;
            finalInputY =  mouseY;

            rotY += finalInputX * sensitivity * Time.deltaTime;
            rotX -= finalInputY * sensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -30f, 80f);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            topCam.transform.rotation = localRotation;

            if(Input.GetKeyDown(KeyCode.Space) && birdsEye)
            {
                StopAllCoroutines();
                StartCoroutine(BirdsEyeDeactivate());
            }

           
        }

        
    }

    //Initial Third -> First -> Mid -> Top

    public IEnumerator BirdsEyeActivate()
    {
        yield return new WaitForSeconds(1f);
        thirdPersonCam.Priority = 5;
        firstPersonCam.Priority = 5;
        midCam.Priority = 5;
        topCam.Priority = 5;
        firstPersonCam.Priority = 100;
        yield return new  WaitForSeconds(1.5f);
        thirdPersonCam.Priority = 5;
        firstPersonCam.Priority = 5;
        midCam.Priority = 5;
        topCam.Priority = 5;
        midCam.Priority = 50;
        yield return new WaitForSeconds(0.2f);
        thirdPersonCam.Priority = 5;
        firstPersonCam.Priority = 5;
        midCam.Priority = 5;
        topCam.Priority = 5;
        topCam.Priority = 50;

        yield return new WaitForSeconds(birdsEyeTime);

        StartCoroutine(BirdsEyeDeactivate());
       
    }

    public IEnumerator BirdsEyeDeactivate()
    {
        thirdPersonCam.Priority = 5;
        firstPersonCam.Priority = 100;
        midCam.Priority = 0;
        topCam.Priority = 5;
        yield return new WaitForSeconds(2f);
        portalLocatorBeam.SetActive(false);
        thirdPersonCam.Priority = 100;
        firstPersonCam.Priority = 5;
        midCam.Priority = 0;
        topCam.Priority = 5;
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("Stand");
        thirdPersonCam.Priority = 50;
        yield return new WaitForSeconds(1.2f);
        pm.enabled = true;
        birdsEye = false;
        birdsEyeCooldown = true;
        EnableOtherPowers();
    }

    private void EnableOtherPowers()
    {
        brc.enabled = true;
        p.enabled = true;
    }

    private void DisableOtherPowers()
    {
        brc.enabled = false;
        p.enabled = false;
    }
}
