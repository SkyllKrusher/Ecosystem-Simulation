using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using TMPro;
using UnityEngine.UI;

public enum HungerState
{
    FED,
    HUNGRY,
    STARVING,
    STARVED
}

public enum FoodSearchState
{
    SEARCHING,
    FOUND
}

public class Animal : MonoBehaviour
{
    [Header("Stats")]
    //stats such as self energy, speed, search radius. 10 percent of self energy passes to predator

    [Header("Hunger")]
    public Text hungerStateText;
    [Range(0, 100)]
    public float hunger = 0f;
    [Range(0, 10)]
    public float hungerRate = 1f; //per second rate at which animal becomes hungry
    public HungerState hungerState;

    [Header("Movement")]
    public float moveSpeed;
    public float minTimeDirChange = 2f;
    public float maxTimeDirChange = 4f;
    public float maxMoveAngleChange = 30f;

    [Header("Food Gathering")]
    public FoodSearchState foodSearchState;
    public float searchRadius; //radius of sphere in which to search for food
    public LayerMask foodSearchLayerMask;

    private float hungryCutoff = 30f; //hunger value above which animal becomes hungry
    private float starvingCutoff = 70f; //hunger value above which animal becomes starving
    private float starvedCutoff = 100f; //hunger value above which animal dies from starvation
    private bool isAlive;

    private Vector3 moveDir;

    private Coroutine moveRandomlyCoroutine;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        SearchFood();
    }

    private void FixedUpdate()
    {
        SimpleMoveInDir(moveDir);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Plant") //TODO: for testing rn, update this later
        {
            EatFood(other.gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            moveDir = (other.contacts[0].normal);
        }
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

    #region Movement Methods -----------------------
    private void Wander()
    {
        if (moveRandomlyCoroutine != null)
        {
            StopCoroutine(moveRandomlyCoroutine);
        }
        moveRandomlyCoroutine = StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        moveDir = transform.right;
        float randomAngle;
        while (isAlive && foodSearchState == FoodSearchState.SEARCHING)
        {
            randomAngle = Random.Range(-maxMoveAngleChange, maxMoveAngleChange);
            // Debug.Log("Move Angle: " + randomAngle);
            moveDir = CalculateRotationDir(moveDir, randomAngle);
            // SimpleMoveInDir(moveDir);
            yield return new WaitForSeconds(Random.Range(minTimeDirChange, maxTimeDirChange));
        }
    }

    private void SimpleMoveInDir(Vector3 moveDir)
    {
        if (isAlive)
        {
            rb.velocity = moveDir * moveSpeed;
        }
    }

    private Vector3 CalculateRotationDir(Vector3 dir, float angle)
    {
        float radAngle = angle * Mathf.Deg2Rad;
        return new Vector3(dir.z * Mathf.Sin(radAngle) + dir.x * Mathf.Cos(radAngle), 0, dir.z * Mathf.Cos(radAngle) - dir.x * Mathf.Sin(radAngle));
    }
    #endregion

    #region Food Gathering Methods -----------------------
    private void SearchFood()
    {
        if (isAlive)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius, foodSearchLayerMask);
            if (hitColliders.Length != 0)
            {
                foodSearchState = FoodSearchState.FOUND;
                FollowFood(hitColliders[0].transform.position);
            }
            else
            {
                if (foodSearchState != FoodSearchState.SEARCHING)
                {
                    foodSearchState = FoodSearchState.SEARCHING;
                    Wander();
                }
            }
        }
        //TODO: add if hunger 0 don't search for food but keep wandering
    }

    private void FollowFood(Vector3 foodPosition)
    {
        moveDir = Vector3.Normalize(foodPosition - transform.position);
        // moveDir.y = 0f;

        // Debug.Log("Follow Food Dir: " + moveDir);
        // SimpleMoveInDir(moveDir);
    }

    private void EatFood(GameObject foodObj) //TODO: for testing rn, update this function later
    {
        hunger = 0;
        foodObj.SetActive(false);
    }
    #endregion

    private void Die()
    {
        isAlive = false;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, searchRadius);
    }
}