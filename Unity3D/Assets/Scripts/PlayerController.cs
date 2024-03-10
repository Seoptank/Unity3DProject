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
        // x,z�� �̵�
        float x = Input.GetAxisRaw("Horizontal");   // ����Ű ��/��
        float z = Input.GetAxisRaw("Vertical");     // ����Ű ��/��

        ani.SetFloat("x", x);
        ani.SetFloat("z", z);

        movement3D.MoveTo(new Vector3(x, 0, z));

        // y�� �̵� (����)
        if(Input.GetKeyDown(jumpKey))
        {
            movement3D.JumpTo();
        }
    }
}
