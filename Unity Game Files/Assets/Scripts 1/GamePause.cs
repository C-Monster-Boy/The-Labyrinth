using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool isGamePaused;
    public GameObject panel_BG;
    public GameObject panel;

    private AudioSource[] audioSources;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        DisableCursor();
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else if(!isGamePaused)
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        DisableCursor();
        Time.timeScale = 1f;
        // Start Music
        isGamePaused = false;
        panel_BG.SetActive(false);
        panel.SetActive(false);
        EnableBreadcrumb();
    }

    public void Pause()
    {
        EnableCursor();
        Time.timeScale = 0f;
        //Shutoff Music
        isGamePaused = true;
        panel_BG.SetActive(true);
        panel.SetActive(true);
        DisableBreadcrumb();
    }

    public void ToMainMenu()
    {
        EnableCursor();
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void  DisableBreadcrumb()
    {
        player.GetComponent<BreadCrumb>().enabled = false;
    }
    private void EnableBreadcrumb()
    {
        player.GetComponent<BreadCrumb>().enabled = true;
    }

}
