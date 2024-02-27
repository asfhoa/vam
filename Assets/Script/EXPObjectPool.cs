using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPObjectPool : Singleton<EXPObjectPool>
{
    [SerializeField] EXPObject prefab;

    public EXPObject GetRandomExpObject()
    {
        //TYPE�� ��� ���� ������ ������ �ϳ��� ��Ҹ� ����
        var types = System.Enum.GetValues(typeof(EXPObject.TYPE)) as EXPObject.TYPE[];
        EXPObject.TYPE type = types[Random.Range(0, types.Length)];

        EXPObject newExpObject = Instantiate(prefab);
        newExpObject.Setup(type);
        return newExpObject;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            EXPObject obj = GetRandomExpObject();
            obj.transform.position = position;
        }
    }
}
