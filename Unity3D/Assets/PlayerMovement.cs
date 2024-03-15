using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(LineRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float           moveSpeed; // �÷��̾� �̵��ӵ�
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

        // �ʱ⿡ ���� ������ ��Ȱ��ȭ
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        float x = virtualJoystick.Hor();
        float z = virtualJoystick.Ver();

        float attackX = attackJoystick.Hor();
        float attackZ = attackJoystick.Ver();

        //===========================================
        // ������

        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        // �÷��̾� ������/ȸ��
        MoveTo(hor, ver);

        if(hor != 0 || ver != 0)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }
        // ������
        //===========================================


        // �÷��̾� �ִϸ��̼�
        //PlayerAni();

        // ���� ������ ǥ��
        ShowLineRenderer(attackX, attackZ);
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

    private void ShowLineRenderer(float x, float z)
    {
        Vector3 moveDir = new Vector3(x, 0, z).normalized;
        float   length = Mathf.Min(moveDir.magnitude * maxLineLength, maxLineLength);
    
        // ��ġ ���̰�, �̵� ������ �ִ� ��쿡�� ���� ������ Ȱ��ȭ
        if (attackJoystick.isTouching && (x != 0 || z != 0))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position); // ���� �������� �÷��̾� ��ġ
            lineRenderer.SetPosition(1, transform.position + new Vector3(x, 0, z).normalized * length); // ���� ������ �̵� �������κ��� 5 �Ÿ���ŭ
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

