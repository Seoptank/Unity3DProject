using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform   rectBG;         // Joystick����� RectTransform
    private RectTransform   rectIN;         // Joystick������ ���� �����̴� �κ��� RectTransform
    private Vector2         touch;          // ���̽�ƽ�� �����̴� ����Vector
    private float           radious;        // rectBG�� ������ ��
    public bool             isTouching;     // ������ ������ äũ

    private void Awake()
    {
        rectBG = GetComponent<RectTransform>();
        rectIN = transform.Find("Joystick_IN").GetComponent<RectTransform>();

        radious = rectBG.sizeDelta.x * 0.5f;
    }
    // Drag�̺�Ʈ ���� ��
    public void OnDrag(PointerEventData eventData)
    {
        isTouching = true;

        // (eventData.position - rectBG.anchoredPosition) => ��ġ�� ��ġ - ������ ��ġ => ��,����
        // ���������� ������ ���� -1���� 1������ ���� ����ȭ
        touch = (eventData.position - rectBG.anchoredPosition) / radious;

        // Joystick�� BG�� ������ ������ ������ ���ϰ�
        if (touch.magnitude > 1)
            touch = touch.normalized;
        // Joystick ������
        rectIN.anchoredPosition = touch * radious; 
    }
    // Point�� ���콺�� ������ ��
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    // Point�� ���콺�� ���� ��
    public void OnPointerUp(PointerEventData eventData)
    {
        // ���콺 ���� ����ġ
        rectIN.anchoredPosition = Vector2.zero;
        // ĳ���� �����̴� �� ����ġ
        touch = Vector2.zero;

        isTouching = false;

    }
    public float Hor()
    {
        return touch.x;
    }
    public float Ver()
    {
        return touch.y;
    }
}
