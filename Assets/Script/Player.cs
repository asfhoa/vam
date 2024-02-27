using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image expBar;
    [SerializeField] GameObject hpBar;
    [SerializeField] float speed;

    Animator anim;
    SpriteRenderer spriteRenderer;

    LayerMask layerMask;
    int level;
    int exp;
    int killCount;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerMask = 1 << LayerMask.NameToLayer("Exp");
        level = 1;
        exp = 0;
        killCount = 0;

        UpdateUI();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f, layerMask);
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
        if(this.exp >= NeedtoTatalExp(level))
        {
            level++;
        }

        UpdateUI();
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
}
