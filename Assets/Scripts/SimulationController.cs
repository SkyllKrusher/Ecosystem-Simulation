using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private float simulationSpeed;

    private void Start()
    {
        UpdateSimulationSpeed();
    }

    private void OnValidate()
    {
        UpdateSimulationSpeed();
    }

    private void UpdateSimulationSpeed()
    {
        Time.timeScale = simulationSpeed;
    }
}
