using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : EntityBase
{
    public static List<Food> existingFood = new List<Food>();

    public float maxFood = 100;
    [SerializeField] private float currentValue;

    public void Eat(float eatAmount)
    {
        currentValue -= eatAmount;

        if (currentValue >= 0)
        {
            float valueNormalized = currentValue / maxFood;
            transform.localScale = new Vector3(valueNormalized, valueNormalized, valueNormalized);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        existingFood.Add(this);
        currentValue = maxFood;
        Eat(0);
    }

    private void OnDestroy()
    {
        existingFood.Remove(this);
    }
}
