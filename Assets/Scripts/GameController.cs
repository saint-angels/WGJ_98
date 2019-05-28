using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public event Action<uint> OnFoodUpdate = (h) => { };

    [SerializeField] private float hungerTick = 2f;

    public uint CurrentFood
    {
        get
        {
            return currentFood;
        }
        private set
        {
            currentFood = value;
            OnFoodUpdate(currentFood);
        }
    }

    private uint currentFood = 100;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator HungerRoutine()
    {
        CurrentFood = CurrentFood;
        while (true)
        {
            yield return new WaitForSeconds(hungerTick);

            CurrentFood = (uint)Mathf.Max(0, CurrentFood - 1);
        }
    }

}
