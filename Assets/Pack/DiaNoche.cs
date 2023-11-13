using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNoche : MonoBehaviour
{
    //Variable
    private float min, grados;
    public float timeSpeed = 1;
    public Light luna;

    // Update is called once per frame
    void Update()
    {
        //1 dia 24 min
        min += timeSpeed * Time.deltaTime;//1 seg = a 1 min
        if (min >= 1440)//1440 son 24hs
        {
            min = 0;
        }
        //360° / 1440 1° 0.25min
        grados = min / 4;
        this.transform.localEulerAngles = new Vector3(grados, -90f, 0f);
        if (grados >= 180)
        {
            this.GetComponent<Light>().enabled = false;
            luna.enabled = true;
        }
        else
        {
            this.GetComponent<Light>().enabled = true;
            luna.enabled = false;
        }
    }
}
