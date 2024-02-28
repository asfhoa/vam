using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] PassiveInfo[] passiveInfos;    //패시브 아이템 인포
    [SerializeField] WeaponInfo[] weaponInfos;      //무기 아이템 인포

    public Item[] GetRandomItem(List<Item>inventory)
    {
        int count = 3;

        //두 종류의 아이템을 리스트에 담은 뒤 섞는다
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

        //count(기본 3)개 만큼 뽑아서 전달
        Item[] selected = new Item[count];
        for (int i = 0; i < count; i++)
        {
            //인벤토리 내부에 해당 아이템이 존재하는지 확인
            Item have = inventory.Find(item => item.itemInfo.id == itemInfos[i].id);

            //랜덤 info를 가진 아이템 생성
            selected[i] = new Item();
            selected[i].itemInfo = itemInfos[i];
            selected[i].level = (have == null) ? 1 : have.level;
        }
        return selected;
    }
}
