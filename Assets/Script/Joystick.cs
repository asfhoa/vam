using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Joystick : MonoBehaviour
{
    [SerializeField] RectTransform stickParent;
    [SerializeField] RectTransform stickRect;
    [SerializeField] Vector2 deadzone;
    [SerializeField] UnityEvent<Vector2> onMove;

    bool isPress;       //누르는 중인지
    float maxDistance;

    private void Start()
    {
        maxDistance = stickParent.sizeDelta.x / 2f;
    }

    private void Update()
    {
        //조이스틱 내부에서 터치 시 이동을 시작한다
        if (RectTransformUtility.RectangleContainsScreenPoint(stickParent,Input.mousePosition))
        {
            if (Input.GetMouseButtonDown(0))
                isPress = true;
        }

        if (isPress && Input.GetMouseButton(0))
            Move();
        else
            EndMove();
    }

    private void Move()
    {
        //마우스 터치 지점츠로부터 원점까지의 거리가 maxDistance를 넘지 않는 경우
        if (Vector3.Distance(stickParent.position, Input.mousePosition) <= maxDistance)
            stickRect.position = Input.mousePosition;
        else
        {
            //원점에서 터치지점으로 향하는 단위벡터를 계산한 뒤 최대거리(스칼라)를 곱해 위치 고정
            Vector3 direction = (Input.mousePosition - stickParent.position).normalized;
            stickRect.position = stickParent.position + direction * maxDistance;
        }

        //이벤트에 전달할 입력값 계산
        {
            //햔재 vector의 각 축이 maxDistance대비 어떤 배율을 가지는지
            Vector3 direction = stickRect.position - stickParent.position;
            Vector2 input = Vector2.zero;
            input.x = direction.x / maxDistance;
            input.y = direction.y / maxDistance;

            //입력 데드 값 접용
            if (Mathf.Abs(input.x) <= deadzone.x)
                input.x = 0f;
            if (Mathf.Abs(input.y) <= deadzone.y)
                input.y = 0f;

            onMove?.Invoke(input);
        }
    }

    private void EndMove()
    {
        stickRect.localPosition = Vector2.zero;
        isPress = false;
        onMove?.Invoke(Vector2.zero);
    }
}
