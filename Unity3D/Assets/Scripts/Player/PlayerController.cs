using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KeyCode         keyJump = KeyCode.Space;

    private PlayerMovement  movement;
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 3D �����δ� x,z���� ���鿡���� �̵��� ��Ÿ��
        movement.MoveTo(new Vector3(x, 0, z));

        if(Input.GetKeyDown(keyJump))
        {
            movement.JumpTo();
        }
    }
}
