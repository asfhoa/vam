using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] Ability originStatus;

    // 기본 스텟 + 아이템 스텟
    private Ability increaseStatus { get; set; }
    private Ability finalStatus { get; set; }

    public float maxHp => finalStatus.hp;
    public float power => finalStatus.power;
    public float speed => finalStatus.speed;
    public float cooltime => finalStatus.cooltime;

    protected float hp;       // 체력
    protected int level;      // 레벨.
    protected int exp;        // 경험치.
    protected int killCount;  // 킬 카운트.

    protected virtual void UpdateStatus()
    {
        //실제 적용 스테이터스 계산
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
