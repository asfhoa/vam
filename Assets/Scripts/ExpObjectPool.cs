using UnityEngine;

public class ExpObjectPool : Singleton<ExpObjectPool>
{
    [SerializeField] ExpObject prefab;

    public ExpObject GetRandomExpObject()
    {
        // TYPE의 모든 값을 가져와 랜던한 하나의 요소를 선택.
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
