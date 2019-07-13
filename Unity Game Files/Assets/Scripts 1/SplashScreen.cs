using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Image fadePanel;
    public float reduceVal;

    private Color32 panelColor;
    private float alphaValue;
    
    // Start is called before the first frame update
    void Start()
    {
        reduceVal = (Time.deltaTime*255f)/1.5f;
        panelColor = fadePanel.color;
        alphaValue = panelColor.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (alphaValue > 1f)
        {
            reduceVal = (Time.deltaTime * 255f) / 2f;
            alphaValue -= reduceVal;
            panelColor.a = (byte)alphaValue;
            fadePanel.color = panelColor;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
