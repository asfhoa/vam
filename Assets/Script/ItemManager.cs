using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] PassiveInfo[] passiveInfos;    //�нú� ������ ����
    [SerializeField] WeaponInfo[] weaponInfos;      //���� ������ ����

    public Item[] GetRandomItem(List<Item>inventory)
    {
        int count = 3;

        //�� ������ �������� ����Ʈ�� ���� �� ���´�
        List<ItemInfo> itemInfos = new List<ItemInfo>();
        itemInfos.AddRange(passiveInfos);
        itemInfos.AddRange(weaponInfos);

        for (int i = 0; i < itemInfos.Count; i++)
        {
            int ran1 = Random.Range(0, itemInfos.Count);
            int ran2 = Random.Range(0, itemInfos.Count);

            ItemInfo temp = itemInfos[ran1];
            itemInfos[ran1] = itemInfos[ran2];
            itemInfos[ran2] = temp;
        }

        //count(�⺻ 3)�� ��ŭ �̾Ƽ� ����
        Item[] selected = new Item[count];
        for (int i = 0; i < count; i++)
        {
            //�κ��丮 ���ο� �ش� �������� �����ϴ��� Ȯ��
            Item have = inventory.Find(item => item.itemInfo.id == itemInfos[i].id);

            //���� info�� ���� ������ ����
            selected[i] = new Item();
            selected[i].itemInfo = itemInfos[i];
            selected[i].level = (have == null) ? 1 : have.level;
        }
        return selected;
    }
}
