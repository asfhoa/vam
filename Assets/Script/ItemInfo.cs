using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo 
{
    public string id;           //���� ���̵�
    public string itemName;     //������ �̸�
    public string description;  //����
    public Sprite iconSprite;   //������ ��������Ʈ
}

[System.Serializable]
public class PassiveInfo : ItemInfo
{
    public Status originStatus; //�⺻ �ɷ�ġ
}

[System.Serializable]
public class WeaponInfo : ItemInfo
{
    [Header("Weapon")]
    public Sprite handSprite;            //��� ��������Ʈ
    public StatusElement power;
    public StatusElement projectile;
    public StatusElement continueTime;
    public StatusElement coolTime;
}

