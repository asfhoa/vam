using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    Animator anim;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
