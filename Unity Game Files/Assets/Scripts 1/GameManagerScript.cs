using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static bool firstTime = true;

    //Main Menu Objects
    public GameObject asyncLoadMeter;
    public Image progress;
    public Text progressText;

    private bool mainMenuLoaded;

    //Objects needed for transition
    public static int mazeComplexity;

    //Maze Level Objects

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenuLoaded = true;

        if(GameObject.FindObjectOfType<GameManagerScript>() && !firstTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            firstTime = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (progress.fillAmount < 0.9f && mainMenuLoaded)
            {
                progress.fillAmount += (0.1f / 0.9f);
                progressText.text = (int)(progress.fillAmount * 100) + "%";
            }
            else if (mainMenuLoaded)
            {
                asyncLoadMeter.SetActive(false);
                mainMenuLoaded = false;
            }
        }
       
        
    }

    public void GetMazeComplexity(int complexity)
    {
        mazeComplexity = complexity;
        asyncLoadMeter.SetActive(true);
        progress.fillAmount = 0;
        progressText.text = 0 + "%";
        StartCoroutine(LoadSceneBehind("Maze Generation"));
    }

    public IEnumerator LoadSceneBehind(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while(!op.isDone)
        {
            try
            {
                progress.fillAmount = (op.progress/0.9f);
                progressText.text = (op.progress/0.9f ) * 100 + "%";
            }
            catch(Exception e)
            {
                Debug.Log("Exception in coroutine");
            }
           

            yield return null;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            asyncLoadMeter = GameObject.FindObjectOfType<GameManagerInitializer_00>().asyncLoadMeter_GMI;
            progress = GameObject.FindObjectOfType<GameManagerInitializer_00>().progress_GMI;
            progressText = GameObject.FindObjectOfType<GameManagerInitializer_00>().progressText_GMI;
            asyncLoadMeter.SetActive(true);
            mainMenuLoaded = true;
            progress.fillAmount = 0f;
            progressText.text = (0f / 0.9f) * 100 + "%";
        }
        else if( scene.name == "Win" || scene.name == "GameOver")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
