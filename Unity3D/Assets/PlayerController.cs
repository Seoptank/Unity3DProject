using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator ani;

    private bool isDodge;

    private void Awake()
    {
        ani = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (VertualJoystick.instance.isTouch)
        {
            ani.SetBool("IsMove", true);
            if(VertualJoystick.instance.isDodge)
            {
                ani.SetBool("IsDodge", true);
            }
            else
            {
                ani.SetBool("IsDodge", false);
            }
        }
        else
        {
            ani.SetBool("IsMove", false);
            ani.SetBool("IsDodge", false);
        }
    }
}
