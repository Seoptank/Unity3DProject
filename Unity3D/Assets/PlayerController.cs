using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasAttack01;
    [SerializeField]
    private Image  imageAttacl01;
    private KeyCode keyAttak01 = KeyCode.Q;

    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;
    private void Awake()
    {
        imageAttacl01.enabled = false;
    }
    private void Update()
    {
        ActivateAttack01Indicator();
        AttackAnvas();
    }
    private void ActivateAttack01Indicator()
    {
        if(Input.GetKeyDown(keyAttak01))
        {
            imageAttacl01.enabled = true;
        }
        else if(Input.GetKeyUp(keyAttak01))
        {
            imageAttacl01.enabled = false;
        }
    }
    private void AttackAnvas()
    {
        if(imageAttacl01.enabled)
        {
            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }

            Quaternion ablCanvas = Quaternion.LookRotation(position - transform.position);
            ablCanvas.eulerAngles = new Vector3(0, ablCanvas.eulerAngles.y, ablCanvas.eulerAngles.z);

            canvasAttack01.transform.rotation = Quaternion.Lerp(ablCanvas, canvasAttack01.transform.rotation, 0);
        }
    }
}
