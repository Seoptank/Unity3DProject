using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원

public class VertualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static VertualJoystick instance;

    private RectTransform   rectBG;
    [SerializeField]
    private RectTransform   rectIN;

    private Transform       transformPlayer;
    private float           radius;
    [SerializeField]
    public float            speed;
    private float           Sqr = 0f;

    private Vector3 vecMove;
    private Vector2 vecNormal;

    public bool isTouch = false;
    public bool isDodge = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        rectBG = GetComponent<RectTransform>();

        transformPlayer = GameObject.Find("Player").transform;

        // "Joystick"의 반지름
        radius = rectBG.rect.width * 0.5f;
    }

    private void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - rectBG.position.x, vecTouch.y - rectBG.position.y);

        // vec의 값이 radius이상이 되지 않도록 제한
        vec = Vector2.ClampMagnitude(vec, radius);
        rectIN.localPosition = vec;

        // 조이스틱 배경과 조이스틱과의 거리 비율로 이동
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
        // 동작중일 때 
        if(isTouch)
        {
            transformPlayer.position += vecMove;
        }
    }
}
