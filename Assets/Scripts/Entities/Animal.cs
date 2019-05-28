using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : EntityBase
{
    enum AnimalState
    {
        IDLE,
        EATING,
    }

    [Header("Food")]
    [SerializeField] private float maxFullness = 100;
    [SerializeField] private float foodFullness;
    [SerializeField] private float hungerSpeed = 1f;
    [SerializeField] private float eatPower = 10;


    [SerializeField] private Gradient hungerGradient;

    [SerializeField] TMPro.TextMeshProUGUI stateLabel;

    AnimalState State
    {
        get { return state; }
        set
        {
            transform.DOKill(true);
            //StopCoroutine("RandomWalk");
            //StopCoroutine("EatRoutine");
            StopAllCoroutines();
            switch (value)
            {
                case AnimalState.IDLE:
                    StartCoroutine(RandomWalk());
                    break;
                case AnimalState.EATING:
                    break;
            }
            stateLabel.text = state.ToString();
            state = value;
        }
    }

    AnimalState state;

    void Start()
    {
        foodFullness = maxFullness;
        State = AnimalState.IDLE;
    }

    void Update()
    {
        switch (State)
        {
            case AnimalState.IDLE:

                if (foodFullness < 90)
                {
                    Food food = GetNearByFood();
                    if (food != null)
                    {
                        State = AnimalState.EATING;
                        StartCoroutine(EatRoutine(food));
                    }
                }

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

                break;
            case AnimalState.EATING:
                
                break;
        }
    }

    IEnumerator RandomWalk()
    {
        while (true)
        {
            Vector2 randomDirection = Random.insideUnitCircle;
            Vector3 targetPosition = new Vector3(transform.position.x + randomDirection.x, transform.position.y + randomDirection.y, transform.position.z);
            transform.DOJump(targetPosition, .3f, 1, .2f, false);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    IEnumerator EatRoutine(Food nearbyFoodPiece)
    {
        while (foodFullness < maxFullness)
        {
            if (nearbyFoodPiece != null)
            {
                nearbyFoodPiece.Eat(eatPower);
                foodFullness += eatPower;

                transform.DOShakePosition(.5f);
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                //No food nearby, exit routine
                break;
            }
        }
        State = AnimalState.IDLE;
    }

    private Food GetNearByFood()
    {
        foreach (var food in Food.existingFood)
        {
            float dist = Vector3.Distance(food.transform.position, transform.position);
            if (dist <= 1f)
            {
                return food;
            }
        }
        return null;
    }
}
