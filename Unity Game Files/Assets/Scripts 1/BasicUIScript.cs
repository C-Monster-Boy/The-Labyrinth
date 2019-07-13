using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUIScript : MonoBehaviour
{
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void EnableObject()
    {
        gameObject.SetActive(true);
    }
}
