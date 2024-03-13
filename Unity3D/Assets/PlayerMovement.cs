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

    private NavMeshAgent    nav;
    private Animator        ani;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Animation();
        Move();

        if (transform.position == nav.destination)
        {
            Debug.Log("zzz");
        }
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

            if (Physics.Raycast(ray,out hit,Mathf.Infinity))
            {
                if(hit.collider.tag == "Ground")
                {
                    // 움직임
                    nav.SetDestination(hit.point);
                    nav.stoppingDistance = 0;

                    // 회전
                    Quaternion rotToLook = Quaternion.LookRotation(hit.point - transform.position);
                    float rotY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotToLook.eulerAngles.y,
                                                       ref rotateVel, rotateSpeed * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotY, 0);

                    
                }
            }
        }
    }
}

