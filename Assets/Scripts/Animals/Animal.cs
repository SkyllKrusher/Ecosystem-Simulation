using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using TMPro;
using UnityEngine.UI;

public enum HungerState
{
    FED,
    HUNGRY,
    STARVING,
    STARVED
}

public class Animal : MonoBehaviour
{
    [Header("Hunger Stats")]
    public Text hungerStateText;
    [Range(0, 10)]
    public float hunger = 0f;
    [Range(0, 10)]
    public float hungerRate = 1f; //per second rate at which animal becomes hungry
    public HungerState hungerState;

    private float hungryCutoff = 3f; //hunger value above which animal becomes hungry
    private float starvingCutoff = 7f; //hunger value above which animal becomes starving
    private float starvedCutoff = 10f; //hunger value above which animal dies from starvation
    private bool isAlive;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        hunger = 0f;
        isAlive = true;
        StartCoroutine(HandleHunger());
    }

    private IEnumerator HandleHunger()
    {
        ChangeHungerState(HungerState.FED);
        while (isAlive)
        {
            yield return new WaitForSeconds(1f);
            hunger += hungerRate;
            if (hunger < hungryCutoff)
            {
                ChangeHungerState(HungerState.FED);
            }
            else if (hunger < starvingCutoff)
            {
                ChangeHungerState(HungerState.HUNGRY);
            }
            else if (hunger < starvedCutoff)
            {
                ChangeHungerState(HungerState.STARVING);
            }
            else
            {
                ChangeHungerState(HungerState.STARVED);
            }
        }
    }

    private void ChangeHungerState(HungerState newHungerState)
    {
        switch (newHungerState)
        {
            case HungerState.FED:
                BecomeFed();
                break;
            case HungerState.HUNGRY:
                BecomeHungry();
                break;
            case HungerState.STARVING:
                BecomeStarving();
                break;
            case HungerState.STARVED:
                BecomeStarved();
                break;
        }
    }

    private void BecomeFed()
    {
        hungerState = HungerState.FED;
        hungerStateText.text = "Fed";
    }

    private void BecomeHungry()
    {
        hungerState = HungerState.HUNGRY;
        hungerStateText.text = "Hungry";
    }

    private void BecomeStarving()
    {
        hungerState = HungerState.STARVING;
        hungerStateText.text = "Starving";
    }

    private void BecomeStarved()
    {
        hungerState = HungerState.STARVED;
        hungerStateText.text = "Starved";
        Die();
    }

    private void Die()
    {
        isAlive = false;
    }

}