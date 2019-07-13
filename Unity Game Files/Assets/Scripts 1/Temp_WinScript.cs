using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_WinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerEntry>())
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            GameObject.FindObjectOfType<WinSceneManager>().WinSceneActivate();
            Bot0[] g = GameObject.FindObjectsOfType<Bot0>();
            foreach (Bot0 b in g)
            {
                Destroy(b.gameObject);
            }
        }
    }

    public void LoadSc(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
