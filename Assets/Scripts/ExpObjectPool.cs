using UnityEngine;

public class ExpObjectPool : Singleton<ExpObjectPool>
{
    [SerializeField] ExpObject prefab;

    public ExpObject GetRandomExpObject()
    {
        // TYPE�� ��� ���� ������ ������ �ϳ��� ��Ҹ� ����.
        var types = System.Enum.GetValues(typeof(ExpObject.TYPE)) as ExpObject.TYPE[];
        ExpObject.TYPE type = types[Random.Range(0, types.Length)];

        ExpObject newExpObject = Instantiate(prefab);
        newExpObject.Setup(type);
        return newExpObject;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            ExpObject obj = GetRandomExpObject();
            obj.transform.position = position;
        }
    }
}
