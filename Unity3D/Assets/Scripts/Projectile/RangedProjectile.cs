using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField]
    private float       speed = 10.0f;
    [SerializeField]
    private float       maxDis = 10.0f;
    [SerializeField]
    private float       deactivateTime = 3.0f;
    private Vector3     startPos;

    private PoolManager poolManager;
    private Rigidbody   rigid;
    
    public void Setup(PoolManager newPool)
    {
        poolManager = newPool;
        rigid = GetComponent<Rigidbody>();
        startPos = transform.position;
        
        StartCoroutine(DeactivateThis());
    }

    private void Update()
    {
        float dis = Vector3.Distance(transform.position, startPos);

        rigid.AddForce(transform.forward * speed);
    }

    private IEnumerator DeactivateThis()
    {
        yield return new WaitForSeconds(deactivateTime);
        poolManager.DeactivePoolItem(this.gameObject);
    }
}
