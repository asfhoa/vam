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

    bool isPress;       //������ ������
    float maxDistance;

    private void Start()
    {
        maxDistance = stickParent.sizeDelta.x / 2f;
    }

    private void Update()
    {
        //���̽�ƽ ���ο��� ��ġ �� �̵��� �����Ѵ�
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
        //���콺 ��ġ �������κ��� ���������� �Ÿ��� maxDistance�� ���� �ʴ� ���
        if (Vector3.Distance(stickParent.position, Input.mousePosition) <= maxDistance)
            stickRect.position = Input.mousePosition;
        else
        {
            //�������� ��ġ�������� ���ϴ� �������͸� ����� �� �ִ�Ÿ�(��Į��)�� ���� ��ġ ����
            Vector3 direction = (Input.mousePosition - stickParent.position).normalized;
            stickRect.position = stickParent.position + direction * maxDistance;
        }

        //�̺�Ʈ�� ������ �Է°� ���
        {
            //�h�� vector�� �� ���� maxDistance��� � ������ ��������
            Vector3 direction = stickRect.position - stickParent.position;
            Vector2 input = Vector2.zero;
            input.x = direction.x / maxDistance;
            input.y = direction.y / maxDistance;

            //�Է� ���� �� ����
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
