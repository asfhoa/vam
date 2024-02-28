using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

[System.Serializable]
public struct Status
{
    public float hp;            //ü��
    public float power;         //���ݷ�
    public float moveSpeed;     //�̵� �ӵ�
    public float coolTime;      //���� �ӵ�

    public static Status operator+(Status origin,Status target)
    {
        Status newStatus = new Status();
        newStatus.hp = origin.hp + target.hp;
        newStatus.power = origin.power + target.power;
        newStatus.moveSpeed = origin.moveSpeed + target.moveSpeed;
        newStatus.coolTime = origin.coolTime + target.coolTime;
        return newStatus;
    }

    public static Status operator -(Status origin, Status target)
    {
        Status newStatus = new Status();
        newStatus.hp = origin.hp - target.hp;
        newStatus.power = origin.power - target.power;
        newStatus.moveSpeed = origin.moveSpeed - target.moveSpeed;
        newStatus.coolTime = origin.coolTime - target.coolTime;
        return newStatus;
    }
}

[System.Serializable]
public struct StatusElement
{
    public float origin;
    public float grow;

    public float GetValue(int level)
    {
        return origin + (grow * level);
    }
}