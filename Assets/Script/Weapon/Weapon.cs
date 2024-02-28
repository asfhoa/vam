using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected float power;              //���ݷ�
    protected float coolTime;           //����ü ����
    protected float continueTime;       //��Ÿ��
    protected int projectileCount;      //���ӽð�
    public void UpdateWeapon(Item item, Status ownerStatus)
    {
        //���޹��� Item�� ������ ���� ����ġ�� �������� ������ ���
        int level = item.level;
        WeaponInfo weaponInfo = item.itemInfo as WeaponInfo;

        power = weaponInfo.power.GetValue(level) * (1 + ownerStatus.power / 100f);
        projectileCount=(int)weaponInfo.projectile.GetValue(level);
        coolTime = (int)weaponInfo.coolTime.GetValue(level) * (1 + ownerStatus.coolTime / 100f);
        continueTime = weaponInfo.continueTime.GetValue(level);
    }
}
