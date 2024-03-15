using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform   rectBG;         // Joystick배경의 RectTransform
    private RectTransform   rectIN;         // Joystick내부의 실제 움직이는 부분의 RectTransform
    private Vector2         touch;          // 조이스틱의 움직이는 방향Vector
    private float           radious;        // rectBG의 반지름 값
    public bool             isTouching;     // 누르는 중인지 채크

    private void Awake()
    {
        rectBG = GetComponent<RectTransform>();
        rectIN = transform.Find("Joystick_IN").GetComponent<RectTransform>();

        radious = rectBG.sizeDelta.x * 0.5f;
    }
    // Drag이벤트 중일 때
    public void OnDrag(PointerEventData eventData)
    {
        isTouching = true;

        // (eventData.position - rectBG.anchoredPosition) => 터치한 위치 - 기존의 위치 => 즉,방향
        // 반지름으로 나눠서 값을 -1부터 1까지의 수로 정규화
        touch = (eventData.position - rectBG.anchoredPosition) / radious;

        // Joystick이 BG의 반지름 밖으로 나가지 못하게
        if (touch.magnitude > 1)
            touch = touch.normalized;
        // Joystick 움직임
        rectIN.anchoredPosition = touch * radious; 
    }
    // Point를 마우스로 눌렀을 때
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    // Point를 마우스로 땠을 때
    public void OnPointerUp(PointerEventData eventData)
    {
        // 마우스 때면 원위치
        rectIN.anchoredPosition = Vector2.zero;
        // 캐릭터 움직이는 값 원위치
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
