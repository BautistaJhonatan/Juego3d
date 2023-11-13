using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    [Range(0,1)]public float lerpValue;
    public float sensivility;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position+offset,lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X")* sensivility,Vector3.up)*offset;

        transform.LookAt(target);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
