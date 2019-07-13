using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadCrumb : MonoBehaviour
{
    public static int totalBreadcrumbs;

    public Transform breadcrumbDropPoint;
    public GameObject breadcrumb;

    private Text breadcrumbNo;
    // Start is called before the first frame update
    void Start()
    {
        breadcrumbNo = GameObject.Find("BreadcrumbImage").transform.GetChild(0).GetComponent<Text>();
        totalBreadcrumbs = 10;
        breadcrumbNo.text = totalBreadcrumbs.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(KeyBindManager.BREADCRUMB, "Mouse0"))))
        {
            DropBreadcrumb();
        }

        breadcrumbNo.text = totalBreadcrumbs.ToString();
    }

    void DropBreadcrumb()
    {
        if (totalBreadcrumbs > 0)
        {
            GameObject g = Instantiate(breadcrumb, breadcrumbDropPoint.position, Quaternion.identity);
            totalBreadcrumbs--;
        }     
    }
    
    public void IncrementCrumbCount()
    {
        totalBreadcrumbs++;
    }

}
