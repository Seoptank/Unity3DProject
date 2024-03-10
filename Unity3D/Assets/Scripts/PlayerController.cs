using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode     jumpKey = KeyCode.Space;
    private Movement3D  movement3D;
    private Animator    ani;
    private void Awake()
    {
        ani         = GetComponent<Animator>();
        movement3D  = GetComponent<Movement3D>();    
    }
    private void Update()
    {
        // x,z축 이동
        float x = Input.GetAxisRaw("Horizontal");   // 방향키 좌/우
        float z = Input.GetAxisRaw("Vertical");     // 방향키 상/하

        ani.SetFloat("x", x);
        ani.SetFloat("z", z);

        movement3D.MoveTo(new Vector3(x, 0, z));

        // y축 이동 (점프)
        if(Input.GetKeyDown(jumpKey))
        {
            movement3D.JumpTo();
        }
    }
}
