using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    private GameObject  prefabKunai;
    [SerializeField]
    private float       rateTimeKunai;
    [SerializeField]
    private Transform   kunaiSpawnPoint;
    private PoolManager kunaiPoolManager;

    [Header("�÷��̾� ����")]
    [SerializeField]
    private float           moveSpeed; // �÷��̾� �̵��ӵ�

    private VirtualJoystick virtualJoystick;
    private Rigidbody       rigidbody;
    private Animator        animator;
    private void Awake()
    {
        kunaiPoolManager = new PoolManager(prefabKunai);

        virtualJoystick = FindObjectOfType<VirtualJoystick>();

        rigidbody       = GetComponent<Rigidbody>();
        animator        = GetComponent<Animator>();

        StartCoroutine(FireKunai());
    }

    private void OnApplicationQuit()
    {
        kunaiPoolManager.DestroyObjcts();
    }

    private void FixedUpdate()
    {
        float x = virtualJoystick.Hor();
        float z = virtualJoystick.Ver();

        // �÷��̾� ������/ȸ��
        MoveTo(x, z);

        // �÷��̾� �ִϸ��̼�
        PlayerAni();
    }

    private void MoveTo(float x,float z)
    {
        // �÷��̾� �̵� ���� ���
        Vector3 moveDir = new Vector3(x, 0, z).normalized;

        // �������� �̵�
        rigidbody.MovePosition(rigidbody.position + (moveDir * moveSpeed * Time.deltaTime));

        // �÷��̾ ������ ��� ȸ��X
        if (moveDir.magnitude > 0)
        {
            // �÷��̾ �����̴� �������� ȸ����Ŵ
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void PlayerAni()
    {
        if(virtualJoystick.isTouching)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
    }

    private IEnumerator FireKunai()
    {
        while(true)
        {
            yield return new WaitForSeconds(rateTimeKunai);
            GameObject kunai = kunaiPoolManager.ActivePoolItem();
            kunai.transform.position =  kunaiSpawnPoint.position;
            kunai.transform.rotation =  transform.rotation;
            kunai.GetComponent<RangedProjectile>().Setup(kunaiPoolManager);
        }
    }
}

