using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindManager : MonoBehaviour
{
    public static string FORWARD = "KeyBindButton_Forward";
    public static string BACKWARD = "KeyBindButton_Backward";
    public static string LEFT = "KeyBindButton_Left";
    public static string RIGHT = "KeyBindButton_Right";
    public static string EAGLE_EYE = "KeyBindButton_EagleEye";
    public static string PHASE_OUT = "KeyBindButton_PhaseOut";
    public static string BREADCRUMB = "KeyBindButton_Breadcrumb";

    public static bool saved;
    public GameObject savePanel;
    public Text up, down, left, right, eagleEye, phase, breadcrumb;
    public Color32 normal;
    public Color32 selected;

    private Dictionary<string, KeyCode> keys;
    private  GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        saved = true;
        keys = new Dictionary<string, KeyCode>();

        keys.Add("KeyBindButton_Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Forward","W")));
        keys.Add("KeyBindButton_Backward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Backward", "S")));
        keys.Add("KeyBindButton_Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Left", "A")));
        keys.Add("KeyBindButton_Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Right", "D")));
        keys.Add("KeyBindButton_EagleEye", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_EagleEye", "Space")));
        keys.Add("KeyBindButton_PhaseOut", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_PaseOut", "Q")));
        keys.Add("KeyBindButton_Breadcrumb", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Breadcrumb", "Mouse0")));

        up.text         = keys["KeyBindButton_Forward"].ToString();
        down.text       = keys["KeyBindButton_Backward"].ToString();
        left.text       = keys["KeyBindButton_Left"].ToString();
        right.text      = keys["KeyBindButton_Right"].ToString();
        eagleEye.text   = keys["KeyBindButton_EagleEye"].ToString();
        phase.text      = keys["KeyBindButton_PhaseOut"].ToString();
        breadcrumb.text = keys["KeyBindButton_Breadcrumb"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                StartCoroutine(DelayActive(currentKey));
                currentKey = null;
            }
            else if(e.isMouse) // && e.type == EventType.MouseUp
            { 
                switch(e.button)
                {
                    case 0:
                        {
                            keys[currentKey.name] = KeyCode.Mouse0;
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.Mouse0.ToString();
                            break;
                        }
                    case 1:
                        {
                            keys[currentKey.name] = KeyCode.Mouse1;
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.Mouse1.ToString();
                            break;
                        }
                    case 2:
                        {
                            keys[currentKey.name] = KeyCode.Mouse2;
                            currentKey.transform.GetChild(0).GetComponent<Text>().text = KeyCode.Mouse2.ToString();
                            break;
                        }
                }
                StartCoroutine(DelayActive(currentKey));
                currentKey = null;

            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
            if (currentKey != null)
            {
                currentKey.GetComponent<Image>().color = normal;

            }

            currentKey = clicked;
            currentKey.GetComponent<Button>().interactable = false;
            currentKey.GetComponent<Image>().color = selected;
             saved = false;
    }

    private IEnumerator DelayActive(GameObject currentKey)
    {
        yield return new WaitForSeconds(0.2f);
        currentKey.GetComponent<Image>().color = normal;
        currentKey.GetComponent<Button>().interactable = true;

    }

    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
        saved = true;
    }

    public void DefaultKeys()
    {
        keys["KeyBindButton_Forward"] = KeyCode.W;
        keys["KeyBindButton_Backward"] = KeyCode.S;
        keys["KeyBindButton_Left"] = KeyCode.A;
        keys["KeyBindButton_Right"] = KeyCode.D;
        keys["KeyBindButton_EagleEye"] = KeyCode.Space;
        keys["KeyBindButton_PhaseOut"] = KeyCode.Q;
        keys["KeyBindButton_Breadcrumb"] = KeyCode.Mouse0;

        up.text = keys["KeyBindButton_Forward"].ToString();
        down.text = keys["KeyBindButton_Backward"].ToString();
        left.text = keys["KeyBindButton_Left"].ToString();
        right.text = keys["KeyBindButton_Right"].ToString();
        eagleEye.text = keys["KeyBindButton_EagleEye"].ToString();
        phase.text = keys["KeyBindButton_PhaseOut"].ToString();
        breadcrumb.text = keys["KeyBindButton_Breadcrumb"].ToString();

        saved = false;
    }

    public void SaveCheckModeActive()
    {
        savePanel.SetActive(true);
    }
    public void SaveCheckModeInactive()
    {
        savePanel.SetActive(false);
    }

    public void SetKeyBindText()
    {
       
        keys["KeyBindButton_Forward"]       =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Forward","W"));
        keys["KeyBindButton_Backward"]      =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Backward","S"));
        keys["KeyBindButton_Left"]          =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Left","A"));
        keys["KeyBindButton_Right"]         =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Right", "D"));
        keys["KeyBindButton_EagleEye"]      =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_EagleEye", "Space"));
        keys["KeyBindButton_PhaseOut"]      =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_PaseOut", "Q"));
        keys["KeyBindButton_Breadcrumb"]    =       (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KeyBindButton_Breadcrumb", "Mouse0"));

        up.text = keys["KeyBindButton_Forward"].ToString();
        down.text = keys["KeyBindButton_Backward"].ToString();
        left.text = keys["KeyBindButton_Left"].ToString();
        right.text = keys["KeyBindButton_Right"].ToString();
        eagleEye.text = keys["KeyBindButton_EagleEye"].ToString();
        phase.text = keys["KeyBindButton_PhaseOut"].ToString();
        breadcrumb.text = keys["KeyBindButton_Breadcrumb"].ToString();
    }
           
}
