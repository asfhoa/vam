using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Ability originStatus;

    // �⺻ ���� + ������ ����
    private Ability increaseStatus { get; set; }
    private Ability finalStatus { get; set; }

    public float maxHp => finalStatus.hp;
    public float power => finalStatus.power;
    public float speed => finalStatus.speed;
    public float cooltime => finalStatus.cooltime;

    protected float hp;       // ü��
    protected int level;      // ����.
    protected int exp;        // ����ġ.
    protected int killCount;  // ų ī��Ʈ.

    protected virtual void UpdateStatus()
    {
        //���� ���� �������ͽ� ���
        finalStatus = originStatus + increaseStatus;
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    protected void ResetIncrease()
    {
        increaseStatus = new Ability();
    }

    protected void AddIncrease(Ability ability)
    {
        increaseStatus += ability;
    }

    public WeaponStatus ApplyAbility(WeaponStatus target)
    {
        return target * finalStatus;
    }
}
