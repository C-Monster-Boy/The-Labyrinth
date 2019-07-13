using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManagerInitializer_00 : MonoBehaviour
{

    public  GameManagerScript g;
    public Button b = null;

    //Objects to initialize g
    public GameObject asyncLoadMeter_GMI;
    public Image progress_GMI;
    public Text progressText_GMI;


    private bool calledOnce = false;



    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindObjectOfType<GameManagerScript>();
        g.asyncLoadMeter = asyncLoadMeter_GMI;
        g.progress = progress_GMI;
        g.progressText = progressText_GMI;
        //g.asyncLoadMeter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current)
        {
            if (EventSystem.current.currentSelectedGameObject)
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>())
                {
                    b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

        if (b != null && b.gameObject.name.StartsWith("LevelButton") && !calledOnce)
        {
            g.GetMazeComplexity(Int32.Parse(b.name.Substring(b.name.Length - 2)));
            calledOnce = true;

        }
    }
}
