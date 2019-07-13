using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Phase : MonoBehaviour
{

    public static float phase = 100f;
    public static bool phaseCooldown = false;

    public GameObject phaseBar;
    

    private Image phaseFill;
    public float decr = 0.3f;
    private float incr = 0.08f;
    private bool activate = false;
    private Animator anim;
    private GameObject particle;
    private PlayerEntry pe;
    private bool readyToDcr = false;

    private Collider playerCollider;
    private Collider botCollider;
    private BreadCrumb brc;
    private BirdsEye be;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pe = GetComponent<PlayerEntry>();
        phaseBar = GameObject.Find("Phase Bar");
        phaseFill = phaseBar.transform.GetChild(1).GetComponent<Image>();
        particle = transform.Find("PowerTrails").gameObject;
        particle.SetActive(false);
        brc = GetComponent<BreadCrumb>();
        be = GetComponent<BirdsEye>();


        botCollider = GameObject.FindGameObjectWithTag("Enemy").transform.Find("Sphere-Bot").GetComponent<Collider>();
        playerCollider = transform.Find("PlayerCollider").GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {

        //print(Physics.GetIgnoreLayerCollision(10, 11));
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.PHASE_OUT, "Q"))) && !activate && !phaseCooldown) 
        {
            DisableOtherPowers();
            activate = true;
           // anim.SetTrigger("Crouch");
            particle.SetActive(true);
            anim.SetTrigger("PowerUp");
            StartCoroutine(BackToMain());
            //Physics.IgnoreCollision(playerCollider, botCollider);
            Physics.IgnoreLayerCollision(10, 11);
        }

        if(activate)
        {
            PhaseShift();
        }

        PhaseMeterRegen();
    }

    void SetActiveBool(int val)
    {
        if(val == 0)
        {
            activate = false;
        }
        else
        {
            activate = true;
        }
    }

    void PhaseShift()
    {
        if(!phaseCooldown)
        {
            if(phaseFill.fillAmount > 0 && readyToDcr) 
            {
                float temp = phaseFill.fillAmount;
                temp -= decr / 100;
                Mathf.Clamp(temp, 0f, 1f);
                phaseFill.fillAmount = temp;
            }
            else if(readyToDcr)
            {
                phaseCooldown = true;
                activate = false;
                EnableOtherPowers();
                readyToDcr = false; 
                particle.SetActive(false);
                //Physics.IgnoreCollision(playerCollider, botCollider, false);
                Physics.IgnoreLayerCollision(10, 11, false);
            }
           
        }
    }

    void PhaseMeterRegen()
    {
        if (phaseCooldown)
        {
            if(phaseFill.fillAmount >= 1)
            {
                phaseCooldown = false;
            }
            else
            {
                pe.SetDissolveInBool(true);
                float temp = phaseFill.fillAmount;
                temp += incr / 100;
                Mathf.Clamp(temp, 0f, 1f);
                phaseFill.fillAmount = temp;
            }
        }
    }

    public IEnumerator BackToMain()
    {   
        yield return new WaitForSeconds(2.5f);
        pe.SetDissolveInBool(false);
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("BackToMain");
        readyToDcr = true;
    }

    private void EnableOtherPowers()
    {
        brc.enabled = true;
        be.enabled = true;
    }

    private void DisableOtherPowers()
    {
        brc.enabled = false;
        be.enabled = false;
    }
}
