using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("공격")]
    [SerializeField]
    private GameObject  prefabKunai;
    [SerializeField]
    private float       rateTimeKunai;
    [SerializeField]
    private Transform   kunaiSpawnPoint;
    private PoolManager kunaiPoolManager;

    [Header("플레이어 스텟")]
    [SerializeField]
    private float           moveSpeed; // 플레이어 이동속도

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

        // 플레이어 움직임/회전
        MoveTo(x, z);

        // 플레이어 애니메이션
        PlayerAni();
    }

    private void MoveTo(float x,float z)
    {
        // 플레이어 이동 방향 계산
        Vector3 moveDir = new Vector3(x, 0, z).normalized;

        // 방향으로 이동
        rigidbody.MovePosition(rigidbody.position + (moveDir * moveSpeed * Time.deltaTime));

        // 플레이어가 정지한 경우 회전X
        if (moveDir.magnitude > 0)
        {
            // 플레이어를 움직이는 방향으로 회전시킴
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

