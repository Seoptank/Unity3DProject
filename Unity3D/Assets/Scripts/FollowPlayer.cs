using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform   playerTransform;
    private float       smooting = 0.2f;
    [SerializeField]
    private Vector3     offset;

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
    private void Update()
    {
        // 플레이어를 부드럽게 따라가도록 수정
        // transform.position = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset,smooting);
    }

}
