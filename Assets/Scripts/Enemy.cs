using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Ability originStatus;

    SpriteRenderer spriteRenderer;
    Animator anim;

    float hp;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 destination = Player.Instance.transform.position;
        Vector3 dir = (destination - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, destination, originStatus.speed * Time.deltaTime);

        spriteRenderer.flipX = dir.x < 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
