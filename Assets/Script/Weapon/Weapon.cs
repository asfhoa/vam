using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected float power;              //공격력
    protected float coolTime;           //투사체 개수
    protected float continueTime;       //쿨타임
    protected int projectileCount;      //지속시간
    public void UpdateWeapon(Item item, Status ownerStatus)
    {
        //전달받은 Item의 레벨에 따른 성장치와 소유자의 스탯을 계산
        int level = item.level;
        WeaponInfo weaponInfo = item.itemInfo as WeaponInfo;

        power = weaponInfo.power.GetValue(level) * (1 + ownerStatus.power / 100f);
        projectileCount=(int)weaponInfo.projectile.GetValue(level);
        coolTime = (int)weaponInfo.coolTime.GetValue(level) * (1 + ownerStatus.coolTime / 100f);
        continueTime = weaponInfo.continueTime.GetValue(level);
    }
}
