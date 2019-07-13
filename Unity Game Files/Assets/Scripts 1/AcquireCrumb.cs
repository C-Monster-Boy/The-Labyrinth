using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquireCrumb : MonoBehaviour
{
    private PlayerMovement p;
    
    // Start is called before the first frame update
    void Start()
    {
       
        p = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() && transform.position.y < - 0.4f )
        {
            p.GetComponent<BreadCrumb>().IncrementCrumbCount();
            Destroy(gameObject);
        }
    }
}
