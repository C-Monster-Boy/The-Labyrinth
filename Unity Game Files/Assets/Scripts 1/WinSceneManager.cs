using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WinSceneManager : MonoBehaviour
{
    public Animator playerWinAnim;
    public CinemachineVirtualCamera winCam;
    public GameObject[] disableOnEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinSceneActivate()
    {   
        foreach(GameObject g in disableOnEnd)
        {
            g.SetActive(false);
        }

        winCam.gameObject.SetActive(true);
        playerWinAnim.SetTrigger("Win");
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<Animator>())
        {
            Destroy(coll.gameObject);
            StartCoroutine(WinSceneLoad());
        }
    }

    private IEnumerator WinSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
    }
}
