using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Ű����, ���콺, ��ġ�� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ����� ����

public class VertualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform   rectBG;
    [SerializeField]
    private RectTransform   rectIN;

    private Transform       transformPlayer;
    private float           radius;
    [SerializeField]
    private float           speed;
    private float           Sqr = 0f;

    private Vector3 vecMove;
    private Vector2 vecNormal;

    private bool isTouch = false;

    private void Awake()
    {
        rectBG = GetComponent<RectTransform>();

        transformPlayer = GameObject.Find("Player").transform;

        // "Joystick"�� ������
        radius = rectBG.rect.width * 0.5f;
    }

    private void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - rectBG.position.x, vecTouch.y - rectBG.position.y);

        // vec�� ���� radius�̻��� ���� �ʵ��� ����
        vec = Vector2.ClampMagnitude(vec, radius);
        rectIN.localPosition = vec;

        // ���̽�ƽ ���� ���̽�ƽ���� �Ÿ� ������ �̵�
        float sqr = (rectBG.position - rectIN.position).sqrMagnitude / (radius * radius);

        Vector2 vecNorm = vec.normalized;

        vecMove = new Vector3(vecNorm.x * speed * Time.deltaTime * sqr, 0f, vecNorm.y * speed * Time.deltaTime * sqr);
        transformPlayer.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNorm.x, vecNorm.y) * Mathf.Rad2Deg, 0f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        isTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectIN.localPosition = Vector2.zero;
        isTouch = false;
    }
    private void Update()
    {
        // �������� �� 
        if(isTouch)
        {
            Debug.Log("zzz");
            transformPlayer.position += vecMove;
        }
    }
}
