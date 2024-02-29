using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShavel : Weapon
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform rotatePivot;

    GameObject[] shavels;

    float showTime;     // ���� �ð� (���� ������ �������� ���ӵǴ°�?)
    float delayTime;    // ������ �ð� (���� ������� �ٽ� ��������� �ɸ��� �ð�)
    bool isShowing;     // �������ΰ�?

    private void Start()
    {
        showTime = status.continueTime;
        delayTime = status.cooltime;
    }
    private void Update()
    {
        if (isShowing)
        {
            showTime -= Time.deltaTime;
            if (showTime <= 0.0f)
            {
                showTime = status.continueTime;
                isShowing = false;
                EndShavel();
            }
        }
        else
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0.0f)
            {
                delayTime = status.cooltime;
                isShowing = true;
                StartShavel();
            }
        }

        // pivot�� �ʴ� 1������ ���� �Ѵ�.
        rotatePivot.Rotate(Vector3.forward, 360f * Time.deltaTime);
    }

    private void StartShavel()
    {
        // ȸ������ �ʱ�ȭ.
        rotatePivot.localRotation = Quaternion.identity;

        // �� ������ ���� �� ������ ���� ȸ�� �� ����.
        shavels = new GameObject[status.projectileCount];
        for(int i = 0; i<shavels.Length; i++)
        {
            Quaternion rot = Quaternion.AngleAxis(360f / status.projectileCount * i, Vector3.forward);
            shavels[i] = Instantiate(prefab, rotatePivot);
            shavels[i].transform.localPosition = rot * Vector3.up * 1.35f;
            shavels[i].transform.localRotation = rot;
        }
    }
    private void EndShavel()
    {
        for (int i = 0; i < shavels.Length; i++)
            Destroy(shavels[i].gameObject);

        shavels = null;
    }
}
