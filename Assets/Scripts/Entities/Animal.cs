using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : EntityBase
{
    [Header("Food")]
    [SerializeField] private float maxFullness = 100;
    [SerializeField] private float foodFullness;
    [SerializeField] private float hungerSpeed = 1f;


    [SerializeField] private Gradient hungerGradient;

    void Start()
    {
        foodFullness = maxFullness;
    }

    void Update()
    {
        //Update hunger
        if (foodFullness > 0)
        {
            foodFullness -= hungerSpeed * Time.deltaTime;
            float fulnessNormalized = Mathf.Clamp01(1f - foodFullness / maxFullness);
            mainSprite.color = hungerGradient.Evaluate(fulnessNormalized);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
