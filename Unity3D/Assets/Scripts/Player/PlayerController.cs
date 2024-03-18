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

        // 3D 상으로는 x,z축이 지면에서의 이동을 나타냄
        movement.MoveTo(new Vector3(x, 0, z));

        if(Input.GetKeyDown(keyJump))
        {
            movement.JumpTo();
        }
    }
}
