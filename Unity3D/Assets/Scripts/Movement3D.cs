using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float       moveSpeed;          // 이동 속도
    [SerializeField]
    private float       jumpForce;          // 뛰는 힘

    private float       gravity = -9.81f;   // 중력 계수
    private Vector3     moveDir;            // 이동 방향

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // 공중에 떠있으면 중력 적용
        if(!characterController.isGrounded)
        {
            // 중력계수가 음수이기 때문에 공중에 떠있는한 플레이어는 계속 떨어지게됨
            moveDir.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    // 외부에서 방향정보를 받아오기 위함
    public void MoveTo(Vector3 dir)
    {
        // 중력이 적용되고있을 때 방향값에 의해서 변경되지 않도록 수정  
        //moveDir = dir;
        moveDir = new Vector3(dir.x, moveDir.y, dir.z);
    }

    // 외부에서 점프를 통제하기 위함
    public void JumpTo()
    {
        if(characterController.isGrounded)
        {
            Debug.Log("점프");
            moveDir.y = jumpForce;
        }
    }
}
