using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcsIT : MonoBehaviour
{
    public GameObject[] Meal;
    public GameObject FoodsShow;
    public bool ShowFood;
    public float timeMin;
    public float timeMax;
    public float hungryAdded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collid)
    {
        Eat();
    }

    public void Eat () 
    {
        ShowFood = false;
        //DestroyObject(FoodsShow);
    }

   
}
