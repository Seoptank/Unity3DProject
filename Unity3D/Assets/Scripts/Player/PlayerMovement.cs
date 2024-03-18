using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어 x,z축 움직임 관련 변수")]
    [SerializeField]
    private float       moveSpeed = 3.0f;   //  플레이어의 이동 속도
    private Vector3     moveDir;            //  플레이어의 이동 방향

    [Header("플레이어 y축 움직임 관련 변수")]
    [SerializeField]
    private float       jumpForce = 3.0f;
    [SerializeField]
    private float       groundOffset;
    [SerializeField]
    private LayerMask   layerGround;
    private float       gravity = -9.81f;   // 중력 계수

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // 공중에 떠있는 상태
        if (!isGropunded())
        {
            // y축에 (중력계수 * deltatime) 를 더해 떨어지는 효과 발생
            moveDir.y += gravity * Time.deltaTime;
        }
        else if (moveDir.y < 0)
        {
            moveDir.y = -2f;
        }

        // 플레이어의 실질적 이동
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 dir)
    {
        // PlayerController에서 방향값을 입력받아 PlayerMovement의 moveDir변수에 전달하기 위한 함수
        // moveDir의 y값은 본연의 y값을 받아와 중력을 적용
        moveDir = new Vector3(dir.x, moveDir.y, dir.z);
    }

    public void JumpTo()
    {
        if(characterController.isGrounded)
        {
            moveDir.y = jumpForce;
        }
    }

    private bool isGropunded()
    {
        Vector3 spherePos = new Vector3(transform.position.x, transform.position.y - groundOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, characterController.radius - 0.05f, layerGround))
            return true;
        return false;
    }
}
