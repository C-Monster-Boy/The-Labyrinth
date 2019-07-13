using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helath : MonoBehaviour
{
    public static bool healthCooldown = true;
    public static float  health = 100f;
    public static float healthIncr = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( health < 100 && healthCooldown )
        {
            Recover();
        }
        
    }

   private void Recover()
    {    
        health += healthIncr;
        health = Mathf.Clamp(health, 0f, 100f);
    }

    public static void Damage(float value)
    {
        float temp = health;
        temp -= value;
        temp = Mathf.Clamp(temp, 0f, 100f);
        health = temp;
    }



}
