using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject[] UIParents;
    public GameObject maskingPanel;
    public GameObject curr;

    private UISoundInitializer uiSource;
    private Stack backStack;
    private KeyBindManager kbm;

    public void Start()
    {
        backStack = new Stack();
        kbm = GameObject.FindObjectOfType<KeyBindManager>();
        uiSource = GameObject.FindObjectOfType<UISoundInitializer>();

        Color col = maskingPanel.GetComponent<Image>().color;
        foreach (GameObject g in UIParents)
        {
            Animator anim = g.GetComponent<Animator>();
            anim.SetTrigger("BecomeNext");
        }
        UIParents[0].GetComponent<Animator>().SetTrigger("BecomeCurrFromNext"); // Make sure MainMenuObject is index 0
        curr = UIParents[0];
    }

    private void Update()
    {
        if(backStack.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (backStack.Count == 1)
                {
                    MenuCamController mcc = GameObject.FindObjectOfType<MenuCamController>();
                    mcc.ChangePriority(0);
                }

                uiSource.PlaySoundOnBackButton();
                Animator currAnim = curr.GetComponent<Animator>();
                GameObject prev = backStack.Pop() as GameObject;
                Animator prevAnim = prev.GetComponent<Animator>();

                currAnim.SetTrigger("BecomeNext");
                prevAnim.SetTrigger("BecomeCurrFromPrev");
                curr = prev;

               
            }
        }
    }

    public void MoveToNext(GameObject next)
    {
        backStack.Push(curr);
        Animator currAnim = curr.GetComponent<Animator>();
        Animator nextAnim = next.GetComponent<Animator>();

        currAnim.SetTrigger("BecomePrev");
        nextAnim.SetTrigger("BecomeCurrFromNext");
        curr = next;
    }

    public void MoveToPrev(GameObject prev)
    {
        backStack.Pop();
        Animator currAnim = curr.GetComponent<Animator>();
        Animator prevAnim = prev.GetComponent<Animator>();

        currAnim.SetTrigger("BecomeNext");
        prevAnim.SetTrigger("BecomeCurrFromPrev");
        curr = prev;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CheckSaveKeyBinds(GameObject prev)
    {
        if(!KeyBindManager.saved)
        {
            kbm.SaveCheckModeActive();
        }
        else
        {
            MoveToPrev(prev);
        }
    }

    public void KeyBindingsUpdate()
    {   
            kbm.SetKeyBindText();    
    }
}
