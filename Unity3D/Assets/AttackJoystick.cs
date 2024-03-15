using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform   rectBG;         // Joystick����� RectTransform
    private RectTransform   rectIN;         // Joystick������ ���� �����̴� �κ��� RectTransform
    private Vector2         touch;          // ���̽�ƽ�� �����̴� ����Vector
    private float           radious;        // rectBG�� ������ ��
    public bool             isTouching;     // ������ ������ äũ

    private PlayerMovement player;
    private void Awake()
    {
        rectBG = GetComponent<RectTransform>();
        rectIN = transform.Find("Attack_IN").GetComponent<RectTransform>();

        player = FindObjectOfType<PlayerMovement>();

        radious = rectBG.sizeDelta.x * 0.5f;
    }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.FireBullet(touch.x, touch.y);
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