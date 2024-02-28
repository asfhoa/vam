using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShavel : Weapon
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform rotatePivot;

    GameObject[] shavels;

    float showTime;     //등장 시간(삽이 나오고 언제까지 지속되는지)
    float delayTime;    //딜레이 시간(삽이 사라지고 다시 아노기까지 걸리는 시간)
    bool isShowing;     //등장중인지

    private void Start()
    {
        showTime = continueTime;
        delayTime = coolTime;
    }

    private void Update()
    {
        if(isShowing)
        {
            showTime -= Time.deltaTime;
            if(showTime <= 0f ) 
            {
                showTime = continueTime;
                isShowing = false;
                EndShavel();
            }
        }
        else
        {
            delayTime -= Time.deltaTime;
            if(delayTime <= 0f )
            {
                delayTime = coolTime;
                isShowing = true;
                StartShavel();
            }
        }

        rotatePivot.Rotate(Vector3.forward, 360 * Time.deltaTime);
    }

    private void StartShavel()
    {
        //회전값을 초기화
        rotatePivot.localRotation = Quaternion.identity;

        //삽 프리팹 생성 및 개수에 따른 회전 값 대입
        shavels = new GameObject[projectileCount];
        for(int i=0;i<shavels.Length;i++)
        {
            Quaternion rot = Quaternion.AngleAxis(360f / projectileCount * i, Vector3.forward);
            shavels[i] = Instantiate(prefab, rotatePivot);
            shavels[i].transform.position = rot * Vector3.up;
        }

    }

    private void EndShavel()
    {
        for(int i=0; i<shavels.Length;i++)
            Destroy(shavels[i]);

        shavels = null;
    }
}
