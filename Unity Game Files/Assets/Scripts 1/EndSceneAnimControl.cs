using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAnimControl : MonoBehaviour
{
    public Animator playerDieAnimator;

    public void Start()
    {
            
        
    }

   void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.GetComponent<Animator>())
        {
            playerDieAnimator.SetTrigger("Die");
            StartCoroutine(LoadLoseScene());
        }
    }

    private IEnumerator LoadLoseScene()
    {
        yield return new  WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
