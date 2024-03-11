using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] Ability grow;
    [SerializeField] Transform damagePivot;       //받은 데미지가 출력되는 위치.

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;  //OnTrigger,OnCollision를 사용하기위해서는 rigidbody가 필수로 들어가야 한다
    Player target;

    float delayTime;    //공격 주기

    public override void Setup()
    {
        base.Setup();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        delayTime = cooltime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
            target = player;    //접촉한 물체가 플레이어라면 타겟에 저장
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
            target = null;      //플레이어가 범위에서 나가면 타겟을 초기화
    }

    void Update()
    {
        // 플레이어에게 넉백을 당하면 velocity가 존재하게 된다.
        // 해당 velocity는 일정 시간에 걸쳐 0으로 복귀한다.
        // velocity가 존재하는 한 움직일 수 없다.

        if (!isAlive || isPauseObject)
            return;

        AttackToPlayer();
        Movement();
    }

    private void AttackToPlayer()
    {
        if (delayTime > 0f)
        {
            delayTime = Mathf.Clamp(delayTime - Time.deltaTime, 0.0f, cooltime);
            return;
        }

        //타겟이 있을 경우
        if (target != null)
        {
            target.TakeDamage(power);   //타겟에게 데미지를 준다
            delayTime = cooltime;
        }
    }
    protected virtual void Movement()
    {
        rigid.velocity = Vector2.MoveTowards(rigid.velocity, Vector2.zero, Time.deltaTime * 8f);

        Vector3 destination = Player.Instance.transform.position;       //플레이어의 위치를 저장
        Vector3 dir = (destination - transform.position).normalized;    //자신의 위치와 플레이어의 위치로 방향을 구한다
        spriteRenderer.flipX = dir.x < 0;                               //움직을 방향에 따라서 스프라이트를 뒤집을지 정해진다

        // 넉백이 없을 때 (= 밀림이 없을 때)에만 움직일 수 있다.
        if (rigid.velocity == Vector2.zero)
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    protected override void OnPauseGame(bool isPause)
    {
        base.OnPauseGame(isPause);
        rigid.constraints = isPause ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
    }


    protected override Ability GetIncrease()
    {
        return grow * GameManager.Instance.gameLevel;
    }

    public override void TakeDamage(float power, float knockback = 0)
    {
        base.TakeDamage(power, knockback);
        TopUI.Instance.AppearDamage(damagePivot.position, power);
    }
    public virtual void DeadForce()
    {
        hp = 0;
        Dead();
    }

    protected override void Hit(float knockback)
    {
        if(anim != null)
            anim.SetTrigger("onHit");

        // 뒤로 밀기.
        if(knockback > 0f)
        {
            Vector3 dir = (transform.position - Player.Instance.transform.position).normalized;
            rigid.velocity = dir * knockback;
        }
    }
    protected override void Dead()
    {
        GetComponent<Collider2D>().enabled = false;
        rigid.velocity = Vector2.zero;
        anim.SetTrigger("onDead");
        StartCoroutine(IEDead());

        ExpObject exp = ExpObjectPool.Instance.GetRandomExpObject();
        exp.transform.position = transform.position;

        AudioManager.Instance.PlaySe("dead", 0.5f);
    }
    private IEnumerator IEDead()
    {
        float fadeTime = 1.0f;
        Color color = spriteRenderer.color;
        while(fadeTime > 0.0f)
        {
            fadeTime = Mathf.Clamp(fadeTime - Time.deltaTime, 0.0f, 1.0f);
            color.a = fadeTime;
            spriteRenderer.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }
}

