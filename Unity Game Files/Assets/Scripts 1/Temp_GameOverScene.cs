using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Temp_GameOverScene : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadGivenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
