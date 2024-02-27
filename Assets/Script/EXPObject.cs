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

    private const float DETEACT_POWER = -6f;    //최초에 접속되었을 때 받는 힘
    private const float GRAVITY = 17f;          //중력 값
    IEnumerator IEUpdate(Transform target,Action<int> getExpEvent)
    {
        //최초에 타겟의 반대 방향으로 벡터를 구해 오브젝트를 이동
        float gravity = DETEACT_POWER;
        while(true)
        {
            Vector3 dir = (target.position - transform.position).normalized;    //타겟을 향하는 단위 벡터
            transform.position += dir * gravity * Time.deltaTime;               //타겟 중심으로 현재 힘(gravity)만큼 이동
            if (Vector3.Distance(transform.position, target.position) <= 0.1f)  //남은 거리가 0.1보다 적을 경우
                break;

            gravity += GRAVITY * Time.deltaTime;                                //중력 가속도 더하기
            yield return null;
        }

        getExpEvent?.Invoke(amount);
        Destroy(gameObject);
    }
}
