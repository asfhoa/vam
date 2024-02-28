using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Status originStatus;
    [SerializeField] float magnetRange;

    Status applyStatus;     //�⺻ ���� + ������ ����

    Animator anim;
    SpriteRenderer spriteRenderer;

    List<Weapon> weapons;
    List<Item> inventory;

    LayerMask layerMask;

    public float maxHp => applyStatus.hp;
    public float power => applyStatus.power;
    public float speed => applyStatus.moveSpeed;
    public float coolTime => applyStatus.coolTime;

    int level;      //����
    int exp;        //����ġ
    int killCount;  //ų ī��Ʈ
    float hp;       //���� hp

    private void Start()
    {
        inventory = new List<Item>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerMask = 1 << LayerMask.NameToLayer("Exp");
        level = 1;
        exp = 0;
        killCount = 0;
        hp = int.MaxValue;

        UpdateUI();
        UpdateStatus();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magnetRange, layerMask);
        foreach (Collider2D collider in colliders)
            collider.gameObject.GetComponent<EXPObject>().ContactPlayer(collider.transform, AddExp);
    }

    public void OnMovement(Vector2 input)
    {
        //�Է°��� �̵������� ��ȯ �� ����
        Vector3 movement = input;
        Vector3 maxPosition = transform.position + movement * speed * Time.deltaTime;
        transform.position = Background.Instance.InBoundary(maxPosition, spriteRenderer.size);

        if (input.x != 0f)
            spriteRenderer.flipX = input.x < 0f;

        //�Է� ���� ���� �ִϸ��̼� ó��
        anim.SetBool("isRun", input != Vector2.zero);
    }

    private int NeedtoTatalExp(int level)
    {
        return Mathf.RoundToInt(5000 / 11 * (Mathf.Pow(1.11f, level - 1) - 1));
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        Debug.Log(this.exp);
        if (this.exp >= NeedtoTatalExp(level))
            LevelUp();

        UpdateUI();
    }

    public void LevelUp()
    {
        level++;
        Item[] randomItems = ItemManager.Instance.GetRandomItem(inventory);
        DrawUI.Instance.ShowDrawUI(randomItems, (select) =>
        {
            Item selectItem = randomItems[select];
            SelectItem(selectItem);
        });
    }

    private void SelectItem(Item selectItem)
    {
        //���� ������ �������� �����Ѵٸ� ������ 1 ������Ų��
        //���ٸ� ���� �����Ѵ�
        int index = inventory.FindIndex(item => item.itemInfo.id == selectItem.itemInfo.id);
        if (index == 1)
        {
            inventory.Add(selectItem);
        }
        else
            inventory[index].level += 1;

        UpdateStatus();
    }

    private void UpdateUI()
    {
        TopUI.Instance.UpdateKillCount(killCount);
        TopUI.Instance.UpdateLevel(level);

        //���� exp�� ������ ����ġ�̱� ������ ���� ������ ���� ������ ���
        float current = exp - NeedtoTatalExp(level);
        float max = NeedtoTatalExp(level + 1) - NeedtoTatalExp(level);
        TopUI.Instance.UpdateExp(current, max);
    }

    private void UpdateStatus()
    {
        applyStatus = originStatus;
        foreach (Item item in inventory)
        {
            if (item.itemInfo is PassiveInfo)
            {
                Status itemStatus = (item.itemInfo as PassiveInfo).originStatus;
                applyStatus += itemStatus;
            }
        }

        foreach(Weapon weapon in weapons)
        {
            //Item targetItem = inventory.Find(w => w.itemInfo.id == weapon.id);
            //weapon.UpdateWeapon()
        }

        hp = Mathf.Clamp(hp, 0, maxHp);
    }
}
