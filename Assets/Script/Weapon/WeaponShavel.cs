using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShavel : Weapon
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform rotatePivot;

    GameObject[] shavels;

    float showTime;     //���� �ð�(���� ������ �������� ���ӵǴ���)
    float delayTime;    //������ �ð�(���� ������� �ٽ� �Ƴ����� �ɸ��� �ð�)
    bool isShowing;     //����������

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
        //ȸ������ �ʱ�ȭ
        rotatePivot.localRotation = Quaternion.identity;

        //�� ������ ���� �� ������ ���� ȸ�� �� ����
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
