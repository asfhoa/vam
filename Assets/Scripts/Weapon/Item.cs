using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;     // ������ �̸�.
    public string id;           // ���� ���̵�.
    public string description;  // ����.
    public int level;
    public Sprite iconSprite;   // ������ ��������Ʈ.

    public virtual Item Copy() { return null; }
}

[System.Serializable]
public class PassiveItem : Item
{
    public Ability status; // �⺻ �ɷ�ġ.

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
    public Sprite handSprite;       // ��� ��������Ʈ.
    public WeaponStatus status;     // �ɷ�ġ.

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