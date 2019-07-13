using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntry : MonoBehaviour
{
    public float dissolveLevel = 1f;
    public bool dissolveInBool;
    SkinnedMeshRenderer skinMesh;
    SkinnedMeshRenderer jointMesh;

    // Start is called before the first frame update
    void Start()
    {
        dissolveInBool = true;
        skinMesh = transform.Find("Alpha_Surface").gameObject.GetComponent<SkinnedMeshRenderer>();
        jointMesh = transform.Find("Alpha_Joints").gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dissolveInBool)
        {
            DissolveIn();
        }
        else
        {
            DissolveOut();
        }
       
    }

    private void DissolveIn()
    {
        if (Time.realtimeSinceStartup >= 1f && dissolveLevel >= 0)
        {
            dissolveLevel -= 0.01f;
            skinMesh.material.SetFloat("_DissolveLevel", dissolveLevel);
            jointMesh.material.SetFloat("_DissolveLevel", dissolveLevel);
        }
    }

    private void DissolveOut()
    {
        if ( dissolveLevel <= 1)
        {
            dissolveLevel += 0.01f;
            skinMesh.material.SetFloat("_DissolveLevel", dissolveLevel);
            jointMesh.material.SetFloat("_DissolveLevel", dissolveLevel);
        }
    }

    public void SetDissolveInBool(bool val)
    {
        dissolveInBool = val;
    }
}
