using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int routine;
    public float cronometro;
    public Animator animator;
    public Quaternion angule;
    public float grades;

    public GameObject target;
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportment_enemy();
    }
    public void Comportment_enemy()
    {
        if (Vector3.Distance(transform.position,target.transform.position)>5)
        {


        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 4)
        {
            routine = Random.Range(0,2);

        }
        switch (routine)
        {
            case 0 :
                animator.SetBool("Walk", false);
                break;
            case 1:
                grades=Random.Range(0, 360);
                angule =Quaternion.Euler(0f, grades, 180f);
                routine++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angule, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                animator.SetBool("Walk", true);
                break;
            }

        }
        else
        {
            if (Vector3.Distance(transform.position,target.transform.position)>1  && !attack)
            {

            var lookPos = target.transform.position-transform.position;
            lookPos.y = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            animator.SetBool("Walk", true);

            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animator.SetBool("Attack",false);
            }
            else
            {
                animator.SetBool("Walk", false);

                animator.SetBool("Attack", true);
                attack = true;
            }
        }
        

    }
    public void Final_Anim()
    {
        animator.SetBool("Attack", false);
        attack = false;
    }
}
