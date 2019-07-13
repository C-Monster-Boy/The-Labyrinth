using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimManager : MonoBehaviour
{
    public Animator playerEndAnim;
    public int animSelectionLimit;
    public string intSelectorName;
    public string endTriggerName;
    public bool callIENum;

    private int animDecider = 0;

    // Start is called before the first frame update
    void Start()
    {
        animDecider = Random.Range(1, animSelectionLimit);
        playerEndAnim.SetInteger(intSelectorName, animDecider);
        if(callIENum)
        {
            StartCoroutine(EndAnim());
        }
    }

    IEnumerator EndAnim()
    {
        yield return new WaitForSeconds(10f);
        playerEndAnim.SetTrigger(endTriggerName);
    }
}
