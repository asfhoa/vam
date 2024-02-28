using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo 
{
    public string id;           //고유 아이디
    public string itemName;     //아이템 이름
    public string description;  //설명
    public Sprite iconSprite;   //아이콘 스프라이트
}

[System.Serializable]
public class PassiveInfo : ItemInfo
{
    public Status originStatus; //기본 능력치
}

[System.Serializable]
public class WeaponInfo : ItemInfo
{
    [Header("Weapon")]
    public Sprite handSprite;            //장비 스프라이트
    public StatusElement power;
    public StatusElement projectile;
    public StatusElement continueTime;
    public StatusElement coolTime;
}

