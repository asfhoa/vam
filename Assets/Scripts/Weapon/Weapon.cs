using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string id;
    public WeaponStatus status;

    public void UpdateWeapon(WeaponStatus originStatus, Unit unit)
    {
        // ���޹��� Item�� ������ ���� ����ġ�� �������� ������ ���.
        status = unit.ApplyAbility(originStatus);
    }
}
