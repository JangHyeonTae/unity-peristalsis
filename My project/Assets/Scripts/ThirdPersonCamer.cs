using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamer : MonoBehaviour
{
    public Transform Target;

    void Start()
    {
       
    }
    void LateUpdate()
    {
        if(Target == null)
        {
            return;
        }

        transform.position = Target.position;
        transform.rotation = Target.rotation;

    }
}
