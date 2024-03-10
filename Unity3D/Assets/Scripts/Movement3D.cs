using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float       moveSpeed;          // �̵� �ӵ�
    [SerializeField]
    private float       jumpForce;          // �ٴ� ��

    private float       gravity = -9.81f;   // �߷� ���
    private Vector3     moveDir;            // �̵� ����

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // ���߿� �������� �߷� ����
        if(!characterController.isGrounded)
        {
            // �߷°���� �����̱� ������ ���߿� ���ִ��� �÷��̾�� ��� �������Ե�
            moveDir.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    // �ܺο��� ���������� �޾ƿ��� ����
    public void MoveTo(Vector3 dir)
    {
        // �߷��� ����ǰ����� �� ���Ⱚ�� ���ؼ� ������� �ʵ��� ����  
        //moveDir = dir;
        moveDir = new Vector3(dir.x, moveDir.y, dir.z);
    }

    // �ܺο��� ������ �����ϱ� ����
    public void JumpTo()
    {
        if(characterController.isGrounded)
        {
            Debug.Log("����");
            moveDir.y = jumpForce;
        }
    }
}
