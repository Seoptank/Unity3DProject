using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(LineRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float           moveSpeed; // 플레이어 이동속도
    [SerializeField]
    private float           maxLineLength;


    [SerializeField]
    private GameObject prefabSwordAura;
    [SerializeField]
    private Transform spawnSwordAura;
    [SerializeField]
    private float swordAuraSpeed = 10.0f;

    private VirtualJoystick virtualJoystick;
    private AttackJoystick  attackJoystick;
    private Rigidbody       rigidbody;
    private Animator        animator;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        virtualJoystick = FindObjectOfType<VirtualJoystick>();
        attackJoystick = FindObjectOfType<AttackJoystick>();

        rigidbody       = GetComponent<Rigidbody>();
        animator        = GetComponent<Animator>();
        lineRenderer    = GetComponent<LineRenderer>();

        // 초기에 라인 렌더러 비활성화
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        float x = virtualJoystick.Hor();
        float z = virtualJoystick.Ver();

        float attackX = attackJoystick.Hor();
        float attackZ = attackJoystick.Ver();

        //===========================================
        // ▼실험용

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        // 플레이어 움직임/회전
        MoveTo(hor, ver);

        if(hor != 0 || ver != 0)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
        // ▲실험용
        //===========================================


        // 플레이어 애니메이션
        //PlayerAni();

        // 라인 렌더러 표시
        ShowLineRenderer(attackX, attackZ);
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

    private void ShowLineRenderer(float x, float z)
    {
        Vector3 moveDir = new Vector3(x, 0, z).normalized;
        float   length = Mathf.Min(moveDir.magnitude * maxLineLength, maxLineLength);
    
        // 터치 중이고, 이동 방향이 있는 경우에만 라인 렌더러 활성화
        if (attackJoystick.isTouching && (x != 0 || z != 0))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position); // 라인 시작점은 플레이어 위치
            lineRenderer.SetPosition(1, transform.position + new Vector3(x, 0, z).normalized * length); // 라인 끝점은 이동 방향으로부터 5 거리만큼
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void FireBullet(float x,float z)
    {
        GameObject swordAura = Instantiate(prefabSwordAura, spawnSwordAura.position, Quaternion.identity);

        Rigidbody rb = swordAura.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(x, 0, z).normalized * swordAuraSpeed;
    }
}

