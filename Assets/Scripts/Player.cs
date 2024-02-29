using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public static Player Instance {  get; private set; }

    [SerializeField] float magnetRange;

    SpriteRenderer spriteRenderer;
    Animator anim;
    LayerMask expMask;

    List<Weapon> weapons;           // ��� ������.
    List<Item> inventory;           // ���� �������� ����.

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        weapons = new List<Weapon>();
        inventory = new List<Item>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        expMask = 1 << LayerMask.NameToLayer("Exp");

        hp = int.MaxValue;
        level = 1;
        exp = 0;
        killCount = 0;

        UpdateStatus();
        UpdateUI();
    }
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magnetRange, expMask);
        foreach(Collider2D collider in colliders)
            collider.GetComponent<ExpObject>().ContactPlayer(transform, AddExp);
    }

    public void OnMovement(Vector2 input)
    {
        // �Է°��� �̵������� ��ȯ �� ����.
        Vector3 movement = input;
        Vector3 nextPosition = transform.position + movement * speed * Time.deltaTime;
        transform.position = Background.Instance.InBoundary(nextPosition, spriteRenderer.size);

        if (input.x != 0f)
            spriteRenderer.flipX = input.x < 0f;

        // �Է� ���� ���� �ִϸ��̼� ó��.
        anim.SetBool("isRun", input != Vector2.zero);
    }

    private void AddExp(int amount)
    {
        exp += amount;
        if (exp >= NeedTotalExp(level + 1))
            LevelUp();

        UpdateUI();
    }
    private int NeedTotalExp(int level)
    {
        if (level <= 0)
            return 0;

        return Mathf.RoundToInt(5000f / 11 * (Mathf.Pow(1.11f, level - 1) - 1));
    }
    private void LevelUp()
    {
        level++;
        Item[] randomItems = ItemManager.Instance.GetRandomItem(inventory);
        DrawUI.Instance.ShowDrawUI(randomItems, (select) => {
            SelectItem(randomItems[select]);       
        });
    }
    private void SelectItem(Item selectItem)
    {
        // ���� ������ �������� �����Ѵٸ� ��ü�Ѵ�. ���ٸ� ���� �����Ѵ�.
        int index = inventory.FindIndex(item => item.id == selectItem.id);
        if (index == -1)
        {
            inventory.Add(selectItem);

            // ���ο� ���⸦ �������� ��� �ν��Ͻ� ����.
            if (selectItem is WeaponItem)
            {
                Weapon prefab = ItemManager.Instance.GetWeaponPrefab(selectItem.id);
                Weapon newWeapon = Instantiate(prefab, transform);
                weapons.Add(newWeapon);
            }
        }
        else
        {
            inventory[index] = selectItem;
        }

        UpdateStatus();
    }

    private void UpdateUI()
    {
        TopUI.Instance.UpdateLevel(level);
        TopUI.Instance.UpdateKillCount(killCount);
        
        // ���� exp�� ������ ����ġ�̱� ������ ���� ������ ���� ������ ���.
        float current = exp - NeedTotalExp(level);
        float max = NeedTotalExp(level + 1) - NeedTotalExp(level);
        TopUI.Instance.UpdateExp(current, max);
    }

    private void UpdateStatus()
    {
        ResetIncrease();
        foreach (Item item in inventory)
        {
            if (item is PassiveItem)
            {
                Ability ability = (item as PassiveItem).status;
                AddIncrease(ability);
            }
        }

        base.UpdateStatus();

        // ��� ���� ���� �������ͽ� ������Ʈ.
        foreach(Weapon weapon in weapons)
        {
            Item targetItem = inventory.Find(w => w.id == weapon.id);
            if (targetItem is WeaponItem)
            {
                WeaponItem weaponItem = targetItem as WeaponItem;
                weapon.UpdateWeapon(weaponItem.status, increaseStatus);
            }
        }

        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void TakeDamage()
    {

    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, magnetRange);
    }
}
