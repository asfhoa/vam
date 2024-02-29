using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string id;
    public WeaponStatus status;

    public void UpdateWeapon(WeaponStatus originStatus, Unit unit)
    {
        // 전달받은 Item의 레벨에 따른 성장치와 소유자의 스텟을 계산.
        status = unit.ApplyAbility(originStatus);
    }
}
