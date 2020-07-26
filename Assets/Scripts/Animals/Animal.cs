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
    [Header("Hunger")]
    public Text hungerStateText;
    [Range(0, 100)]
    public float hunger = 0f;
    [Range(0, 10)]
    public float hungerRate = 1f; //per second rate at which animal becomes hungry
    public HungerState hungerState;

    [Header("Movement")]
    public float moveForce;
    public float minTimeDirChange = 2f;
    public float maxTimeDirChange = 4f;
    public float maxMoveAngleChange = 30f;

    private float hungryCutoff = 30f; //hunger value above which animal becomes hungry
    private float starvingCutoff = 70f; //hunger value above which animal becomes starving
    private float starvedCutoff = 100f; //hunger value above which animal dies from starvation
    private bool isAlive;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        hunger = 0f;
        isAlive = true;
        StartCoroutine(HandleHunger());
        Wander();
    }

    #region Hunger Methods --------------------
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
    #endregion

    private void Wander()
    {
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        Vector3 moveVector = transform.right * moveForce;
        float randomAngle;
        while (isAlive)
        {
            randomAngle = Random.Range(-maxMoveAngleChange, maxMoveAngleChange);
            Debug.Log("Move Angle: " + randomAngle);
            moveVector = CalculateRotationDir(moveVector, randomAngle);
            rb.velocity = (moveVector);
            yield return new WaitForSeconds(Random.Range(minTimeDirChange, maxTimeDirChange));
        }
    }

    private Vector3 CalculateRotationDir(Vector3 dir, float angle)
    {
        float radAngle = angle * Mathf.Deg2Rad;
        return new Vector3(dir.z * Mathf.Sin(radAngle) + dir.x * Mathf.Cos(radAngle), 0, dir.z * Mathf.Cos(radAngle) - dir.x * Mathf.Sin(radAngle));
    }

    private void Die()
    {
        isAlive = false;
    }

}