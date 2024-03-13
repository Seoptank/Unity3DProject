using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float       rotateSpeed = 0.05f;
    private float       rotateVel;
    private float       motionSmoothTime = 0.1f;

    private bool isMoving = false;


    private NavMeshAgent    nav;
    private Animator        ani;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        nav.updateRotation = false;
    }

    private void Update()
    {
        Animation();
        Move();
    }

    public void Animation()
    {
        float speed = nav.velocity.magnitude / nav.speed;
        ani.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }

    public void Move()
    {
        // 마우스 좌클릭
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                nav.SetDestination(hit.point);
                isMoving = true;
            }
        }

        // 이동 중일 때만 회전
        if (isMoving && !nav.pathPending && nav.remainingDistance > nav.stoppingDistance)
        {
            RotateToDestination();
        }
    }

    void RotateToDestination()
    {
        Vector3 direction = (nav.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * nav.angularSpeed);
    }
}

