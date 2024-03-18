using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�÷��̾� x,z�� ������ ���� ����")]
    [SerializeField]
    private float       moveSpeed = 3.0f;   //  �÷��̾��� �̵� �ӵ�
    private Vector3     moveDir;            //  �÷��̾��� �̵� ����

    [Header("�÷��̾� y�� ������ ���� ����")]
    [SerializeField]
    private float       jumpForce = 3.0f;
    [SerializeField]
    private float       groundOffset;
    [SerializeField]
    private LayerMask   layerGround;
    private float       gravity = -9.81f;   // �߷� ���

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // ���߿� ���ִ� ����
        if (!isGropunded())
        {
            // y�࿡ (�߷°�� * deltatime) �� ���� �������� ȿ�� �߻�
            moveDir.y += gravity * Time.deltaTime;
        }
        else if (moveDir.y < 0)
        {
            moveDir.y = -2f;
        }

        // �÷��̾��� ������ �̵�
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 dir)
    {
        // PlayerController���� ���Ⱚ�� �Է¹޾� PlayerMovement�� moveDir������ �����ϱ� ���� �Լ�
        // moveDir�� y���� ������ y���� �޾ƿ� �߷��� ����
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
