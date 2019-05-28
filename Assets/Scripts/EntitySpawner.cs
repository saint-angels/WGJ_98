using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public EntityBase entityPrefab;

    public float spawnRadius = 1f;
    public int entitiesNumber = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < entitiesNumber; i++)
        {
            EntityBase newEntity = Instantiate(entityPrefab, transform);
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
            newEntity.transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
