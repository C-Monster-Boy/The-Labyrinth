using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Temp_LoseGameButton : MonoBehaviour
{
    GameManagerScript g;
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindObjectOfType<GameManagerScript>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Lose();
        }
    }

    public void Lose()
    {
        Cursor.lockState =  CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }


}
