using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Idle,Move,Attack}
public class PlayerMovement : MonoBehaviour
{
    [Header("����")]
    [SerializeField]
    private GameObject  prefabKunai;
    [SerializeField]
    private float       rateTimeKunai;
    [SerializeField]
    private Transform   kunaiSpawnPoint;
    private bool        isAttack;
    private PoolManager kunaiPoolManager;

    [SerializeField]
    private PlayerState curState;

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
        ChangeState(PlayerState.Idle);
    }

    private void OnApplicationQuit()
    {
        kunaiPoolManager.DestroyObjcts();
    }

    private void FixedUpdate()
    {
        float x = virtualJoystick.Hor();
        float z = virtualJoystick.Ver();

        if(!isAttack)
        {
            // �÷��̾� ������/ȸ��
            MoveTo(x, z);

            if (rigidbody.velocity.magnitude < 0.1f)
            {
                ChangeState(PlayerState.Idle);
            }
        }

        // �÷��̾� �ִϸ��̼�
        PlayerAni();
    }

    private void MoveTo(float x,float z)
    {
        ChangeState(PlayerState.Move);

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
        switch (curState)
        {
            case PlayerState.Idle:
                break;
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsAttack", false);
            case PlayerState.Move:
                animator.SetBool("IsWalk", true);
                animator.SetBool("IsAttack", false);
                break;
            case PlayerState.Attack:
                animator.SetBool("IsWalk", false);
                animator.SetBool("IsAttack", true);
                break;
        }
    }

    private IEnumerator FireKunai()
    {
        while(true)
        {
            isAttack = true;
            ChangeState(PlayerState.Attack);
            GameObject kunai = kunaiPoolManager.ActivePoolItem();
            kunai.transform.position =  kunaiSpawnPoint.position;
            kunai.transform.rotation =  transform.rotation;
            kunai.GetComponent<RangedProjectile>().Setup(kunaiPoolManager);
            kunai.GetComponent<Rigidbody>().velocity = kunaiSpawnPoint.forward * 50;
            yield return new WaitForSeconds(0.5f);
            isAttack = false;
            yield return new WaitForSeconds(rateTimeKunai);
        }
    }

    private void ChangeState(PlayerState newState)
    {
        curState = newState;
    }
}

