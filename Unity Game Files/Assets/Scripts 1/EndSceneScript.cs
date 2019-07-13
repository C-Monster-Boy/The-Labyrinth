using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndSceneScript : MonoBehaviour
{

    public GameObject bot;
    public GameObject player;
    public float speed;
    public static bool isScriptActive;
    public CinemachineVirtualCamera deathCam;
    public CinemachineFreeLook mainCam;
    public Animator botKillAnimator;
    public GameObject[] disableOnEnd;

    // Start is called before the first frame update
    void Start()
    {
        isScriptActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isScriptActive)
        {
            Vector3 toVector = player.transform.position;
            toVector.x = 0f;
            toVector.y = 0f;
            bot.transform.Translate(toVector * -speed * Time.deltaTime);
        }
    }

    public void ActivateBotForKill()
    {
        foreach (GameObject g in disableOnEnd)
        {
            g.SetActive(false);
        }
        deathCam.gameObject.SetActive(true);
        isScriptActive = true;
        botKillAnimator.SetTrigger("Kill");
    }
}
