using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoLifle : Weapon
{
    [SerializeField] Bullet prefab;
    [SerializeField] float fireRate;    // 연사속도.

    LayerMask enemyMask;
    Vector2 camSize;

    private void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        //prefab.gameObject.SetActive(false);
        enemyMask = 1 << LayerMask.NameToLayer("Exp");
        camSize.x = Camera.main.orthographicSize * 2f * (Screen.width / (float)Screen.height);
        camSize.y = camSize.x;
    }

    protected override IEnumerator IEAttack()
    {
        // 총알 개수만큼 프리팹 생성.
        Queue<Bullet> projectiles = new Queue<Bullet>();
        for (int i = 0; i < projectileCount; i++)
        {
            Bullet newBullet = Instantiate(prefab);
            newBullet.Setup(power, penetrate);
            projectiles.Enqueue(newBullet);
        }

        // 일정 시간에 한 번씩 총알을 발사.
        float delayTime = 0f;
        while (projectiles.Count > 0)
        {
            if (!isPauseObject)
            {
                delayTime -= Time.deltaTime;
                if (delayTime <= 0.0f)
                {
                    delayTime = fireRate;
                    Bullet bullet = projectiles.Dequeue();
                    Shoot(bullet);
                }
            }
            yield return null;
        }
    }

    private void Shoot(Bullet bullet)
    {
        float dir = SearchTarget();
        Debug.Log(dir);
        bullet.transform.rotation = Quaternion.AngleAxis(dir - 90f, Vector3.forward);
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
    }

    private float SearchTarget()
    {
        float distance = 0f;
        Vector3 target = Vector3.zero;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, camSize, enemyMask);

        foreach (Collider2D collider in colliders)
            distance = Vector3.Distance(target, collider.transform.position);

        return distance;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireCube(transform.position, camSize);
    }
}
