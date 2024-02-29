[System.Serializable] public struct Ability
{
    public float hp;        // ü��.
    public float power;     // ���ݷ�.
    public float speed;     // �̵� �ӵ�.
    public float cooltime;  // ���� �ӵ�.

    public static Ability operator+(Ability origin, Ability target)
    {
        Ability newAbility = new Ability();
        newAbility.hp = origin.hp + target.hp;
        newAbility.power = origin.power + target.power;
        newAbility.speed = origin.speed + target.speed;
        newAbility.cooltime = origin.cooltime + target.cooltime;
        return newAbility;
    }
}
[System.Serializable] public struct WeaponStatus
{
    public float power;              // ���ݷ�.
    public int projectileCount;      // ����ü ����.
    public float cooltime;           // ��Ÿ��.
    public float continueTime;       // ���ӽð�.

    public static WeaponStatus operator *(WeaponStatus origin, Ability target)
    {
        WeaponStatus newStatus = new WeaponStatus();
        newStatus.power = origin.power * (1 + target.power / 100f);
        newStatus.cooltime = origin.cooltime * (1 + target.cooltime / 100f);
        return newStatus;
    }
}
