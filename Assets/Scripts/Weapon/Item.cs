using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;     // 아이템 이름.
    public string id;           // 고유 아이디.
    public string description;  // 설명.
    public int level;
    public Sprite iconSprite;   // 아이콘 스프라이트.

    public virtual Item Copy() { return null; }
}

[System.Serializable]
public class PassiveItem : Item
{
    public Ability status; // 기본 능력치.

    public override Item Copy()
    {
        PassiveItem newItem = new PassiveItem();
        newItem.itemName = itemName;
        newItem.id = id;
        newItem.description = description;
        newItem.iconSprite = iconSprite;
        newItem.status = status;
        newItem.level = level;
        return newItem;
    }
}

[System.Serializable]
public class WeaponItem : Item
{
    [Header("Weapon")]
    public Sprite handSprite;       // 장비 스프라이트.
    public WeaponStatus status;     // 능력치.

    public override Item Copy()
    {
        WeaponItem newItem = new WeaponItem();
        newItem.itemName = itemName;
        newItem.id = id;
        newItem.description = description;
        newItem.iconSprite = iconSprite;
        newItem.handSprite = handSprite;
        newItem.status = status;
        newItem.level = level;
        return newItem;
    }
}
