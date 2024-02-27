using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObject : MonoBehaviour
{
    public enum TYPE
    {
        BRONZE,
        SILVER,
        GOLD,
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    Action<int> getExpEvent;
    int amount;

    public void Setup(TYPE type)
    {
        spriteRenderer.sprite = sprites[(int)type];
        amount = type switch
        {
            TYPE.BRONZE => 1,
            TYPE.SILVER => 3,
            TYPE.GOLD => 5,
        };
    }

    public void ContactPlayer(Transform target,Action<int> getExpEvent)
    {
        gameObject.layer = 0;
        StartCoroutine(IEUpdate(target, getExpEvent));
    }

    private const float DETEACT_POWER = -6f;    //���ʿ� ���ӵǾ��� �� �޴� ��
    private const float GRAVITY = 17f;          //�߷� ��
    IEnumerator IEUpdate(Transform target,Action<int> getExpEvent)
    {
        //���ʿ� Ÿ���� �ݴ� �������� ���͸� ���� ������Ʈ�� �̵�
        float gravity = DETEACT_POWER;
        while(true)
        {
            Vector3 dir = (target.position - transform.position).normalized;    //Ÿ���� ���ϴ� ���� ����
            transform.position += dir * gravity * Time.deltaTime;               //Ÿ�� �߽����� ���� ��(gravity)��ŭ �̵�
            if (Vector3.Distance(transform.position, target.position) <= 0.1f)  //���� �Ÿ��� 0.1���� ���� ���
                break;

            gravity += GRAVITY * Time.deltaTime;                                //�߷� ���ӵ� ���ϱ�
            yield return null;
        }

        getExpEvent?.Invoke(amount);
        Destroy(gameObject);
    }
}
