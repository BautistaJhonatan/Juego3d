using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemDayAndNight : MonoBehaviour
{
    public float min;
    public float timeSpeed = 3.0f;
    public float grades;
    public Light moon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dia 
        min += timeSpeed * Time.deltaTime;

        if (min >= 1440) 
        { 
            min = 0;
        }
        //
        grades = min/4;
        this.transform.localEulerAngles = new Vector3(grades,-90,0);
        if (grades>=180)
        {
            this.GetComponent<Light>().enabled = false;
            moon.enabled = true;
        }
        else
        {
            this.GetComponent<Light>().enabled = true;
            moon.enabled = false;
        }
    }
}
