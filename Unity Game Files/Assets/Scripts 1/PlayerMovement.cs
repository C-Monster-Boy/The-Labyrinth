using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerMovementState
    {
        Stop,
        Walk,
        Run
    };

    public PlayerMovementState currMovementState;
    public Camera camFoll;
    public float walkWait;
    public float runWait;
    public AudioClip[] footStepClips;


    CinemachineFreeLook cine;
    Animator anim;
    //Rigidbody rb;
    float mouseX;
    float mouseY;
    float finalInputX;
    public float walkSpeed =0.5f;
    public float runSpeed = 5f;
    public float strafeSpeed = 0.5f;
    public float walkBackSpeed = -0.5f;
    public float gravityMultiplier = 2.5f;
    public float lowJumpGravityMultiplier = 1.5f;
    float rotY;
    float currSpeed = 0f;
    bool inVertical = false;
    bool readyToPlayFootsteps;
    int currFoorStepIndex;

    private AudioSource footStepsAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        currMovementState = PlayerMovementState.Stop;
        currFoorStepIndex = 0;
        readyToPlayFootsteps = true;
        footStepsAudioSource = transform.GetChild(9).GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        camFoll = Camera.main;
        cine = GameObject.FindObjectOfType<CinemachineFreeLook>();
        cine.Follow = gameObject.transform.Find("LookAtObj");
        cine.LookAt = gameObject.transform.Find("LookAtObj");
        cine.m_YAxis.m_MaxSpeed = PlayerPrefs.GetFloat("Y_SENS",2f);
        cine.m_XAxis.m_MaxSpeed = PlayerPrefs.GetFloat("X_SENS",300f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float inputX = Input.GetAxis("RightStickHorizontal");

        mouseX = Input.GetAxis("Mouse X");
        finalInputX = inputX + mouseX;

        rotY += finalInputX * sensitivity * Time.deltaTime;
       

        Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
        transform.rotation = localRotation;
        */

        switch(currMovementState)
        {
            case PlayerMovementState.Stop:
                {
                    //footStepsAudioSource.Stop();
                    break;
                }
            case PlayerMovementState.Run:
                {
                    if (readyToPlayFootsteps && !footStepsAudioSource.isPlaying)
                    {
                        RunSoundPlay();
                    }

                    break;
                }
            case PlayerMovementState.Walk:
                {
                    if(readyToPlayFootsteps && !footStepsAudioSource.isPlaying)
                    {
                        WalkSoundPlay();
                    }
                    
                    break;
                }
        }
        VerticalInput();
         
        HorizontalInput();
    }

    void VerticalInput()
    {
        if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.FORWARD, "W"))))
        {
            currMovementState = PlayerMovementState.Walk;

            inVertical = true;
            Quaternion localRotation = Quaternion.Euler(0.0f, camFoll.transform.eulerAngles.y, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, 0.1f);

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currMovementState = PlayerMovementState.Run;

                anim.SetFloat("Forward", 1.0f);
                currSpeed = runSpeed;
                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))))
                { 
                    anim.SetFloat("Sideways", -1f);
                    //rb.velocity = new Vector3(-strafeSpeed , 0f, runSpeed);
                    //transform.Translate(new Vector3(-strafeSpeed * Time.deltaTime, 0f, runSpeed * Time.deltaTime));
                }
                else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
                {
                    anim.SetFloat("Sideways", 1f);
                    //rb.velocity = new Vector3(strafeSpeed, 0f, runSpeed);
                    //transform.Translate(new Vector3(strafeSpeed * Time.deltaTime, 0f, runSpeed * Time.deltaTime));
                }
                else if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))) || Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
                {
                    //rb.velocity = Vector3.zero;
                    //rb.angularVelocity = Vector3.zero;
                    anim.SetFloat("Sideways", 0.0f);
                }
            }
            else
            {
                anim.SetFloat("Forward", 0.7f);
                currSpeed = walkSpeed;

                if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))))
                {
                    anim.SetFloat("Forward", 1.0f);
                    anim.SetFloat("Sideways", -1f);
                    //rb.velocity = new Vector3(-strafeSpeed , 0f, walkSpeed);
                    //transform.Translate(new Vector3(-strafeSpeed * Time.deltaTime, 0f, walkSpeed * Time.deltaTime));
                }
                else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
                {
                    anim.SetFloat("Forward", 1.0f);
                    anim.SetFloat("Sideways", 1f);
                    //rb.velocity = new Vector3(strafeSpeed, 0f, walkSpeed);
                    //transform.Translate(new Vector3(strafeSpeed * Time.deltaTime, 0f, walkSpeed * Time.deltaTime));
                }
                else if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))) || Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
                {
                    //rb.velocity = Vector3.zero;
                    //rb.angularVelocity = Vector3.zero;
                    anim.SetFloat("Forward", 0.7f);
                    anim.SetFloat("Sideways", 0.0f);
                }
            }
        }
        else if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.FORWARD, "W"))) || Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.BACKWARD, "S"))))
        {
            currMovementState = PlayerMovementState.Stop;

            anim.SetFloat("Forward", 0.0f);
            inVertical = false;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
        }
        else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.BACKWARD, "S"))))
        {
            currMovementState = PlayerMovementState.Walk;

            inVertical = true;

            Quaternion localRotation = Quaternion.Euler(0.0f, camFoll.transform.eulerAngles.y, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, 0.2f);

            anim.SetFloat("Forward", -0.2f);
            currSpeed = walkBackSpeed;

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))))
            {
                anim.SetFloat("Sideways", -1f);
                //rb.velocity = new Vector3(-strafeSpeed, 0f, walkBackSpeed);
                //transform.Translate(new Vector3(-strafeSpeed * Time.deltaTime, 0f,  walkBackSpeed * Time.deltaTime));
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
            {
                anim.SetFloat("Sideways", 1f);
                //rb.velocity = new Vector3(-strafeSpeed, 0f, walkBackSpeed);
                //transform.Translate(new Vector3(-strafeSpeed * Time.deltaTime, 0f, walkBackSpeed * Time.deltaTime));
            }
            else
            {
                anim.SetFloat("Forward", -0.2f);
                //rb.velocity = new Vector3(0f, 0f, walkBackSpeed);
                //transform.Translate(new Vector3(0f, 0f, walkBackSpeed * Time.deltaTime));

            }
        }

        if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))) || Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
        {
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            anim.SetFloat("Sideways", 0.0f);
        }
    }

    void HorizontalInput()
    {
        if(!inVertical)
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))))
            {
                currMovementState = PlayerMovementState.Walk;

                Quaternion localRotation = Quaternion.Euler(0.0f, camFoll.transform.eulerAngles.y, 0.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, 0.1f);

                anim.SetFloat("Sideways", -1f);
                anim.SetFloat("Forward", 1f);
                //rb.velocity = new Vector3(-strafeSpeed * Time.deltaTime, 0f, 0f);
                //transform.Translate(new Vector3(-strafeSpeed * Time.deltaTime, 0f, 0f));
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
            {
                currMovementState = PlayerMovementState.Walk;

                Quaternion localRotation = Quaternion.Euler(0.0f, camFoll.transform.eulerAngles.y, 0.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, 0.2f);
                anim.SetFloat("Sideways", 1f);
                anim.SetFloat("Forward", 1f);
                //rb.velocity = new Vector3(strafeSpeed * Time.deltaTime, 0f, 0f);
                //transform.Translate(new Vector3(strafeSpeed * Time.deltaTime, 0f, 0f ));
            }
            else if (Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.LEFT, "A"))) || Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.RIGHT, "D"))))
            {
                currMovementState = PlayerMovementState.Stop;

                //rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                anim.SetFloat("Sideways", 0.0f);
                anim.SetFloat("Forward", 0.0f);
            }
        }
        
    }

    void WalkSoundPlay()
    {
        readyToPlayFootsteps = false;
        footStepsAudioSource.clip = footStepClips[currFoorStepIndex];
        if(currFoorStepIndex == 1)
        {
            currFoorStepIndex = 0;
        }
        else
        {
            currFoorStepIndex = 1;
        }
        StartCoroutine(PlayFootSteps(walkWait));
       

    }

    void RunSoundPlay()
    {
        readyToPlayFootsteps = false;
        footStepsAudioSource.clip = footStepClips[currFoorStepIndex];
        if (currFoorStepIndex == 1)
        {
            currFoorStepIndex = 0;
        }
        else
        {
            currFoorStepIndex = 1;
        }
        StartCoroutine(PlayFootSteps(runWait));


    }

    IEnumerator PlayFootSteps(float waitTime)
    {
        footStepsAudioSource.Play();
        yield return new WaitForSeconds(waitTime);
        readyToPlayFootsteps = true;
    }
}
