using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssaultRifle : Weapon
{
    [SerializeField] Bullet prefab;
    [SerializeField] float fireRate;    // ����ӵ�.

    protected override IEnumerator IEAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
