using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3     camOffset; 
    private Transform   playerTransform;
    private float       smooting = 0.2f;            // �ε巯�� �̵��� ���� ��
    private Vector3     velocity = Vector3.zero;    // SmoothDamp�� ���� �ӵ� ��

    private void Awake()
    {
        if(playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
        else
        {
            return;
        }
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = playerTransform.position + camOffset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smooting);
    }

}
