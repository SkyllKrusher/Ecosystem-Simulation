using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private float simulationSpeed;
    [SerializeField] private Text simulationSpeedText;
    [SerializeField] private Button speedUpBtn;
    [SerializeField] public Button slowDownBtn;
    [SerializeField] public Button playPauseBtn;
    [SerializeField] public Button resetCamBtn;
    [SerializeField] public CameraController cameraController;

    // private void Start()
    // {
    //     UpdateSimulationSpeed();
    // }

    private void Awake()
    {
        speedUpBtn.onClick.AddListener(OnSpeedUpClick);
        slowDownBtn.onClick.AddListener(OnSlowDownClick);
        resetCamBtn.onClick.AddListener(OnResetCamClick);
        playPauseBtn.onClick.AddListener(OnPlayPauseClick);
    }

    private void OnValidate()
    {
        UpdateSimulationSpeed();
    }

    private void OnSpeedUpClick()
    {
        simulationSpeed += 0.25f;
        UpdateSimulationSpeed();
        if (simulationSpeed == 10f)
        {
            slowDownBtn.interactable = false;
        }
    }

    private void OnSlowDownClick()
    {
        simulationSpeed -= 0.25f;
        UpdateSimulationSpeed();
        if (simulationSpeed == 0.25f)
        {
            slowDownBtn.interactable = false;
        }
    }


    private void OnResetCamClick()
    {
        cameraController.ResetCam();
        cameraController.StopFollowing();
    }

    private void OnPlayPauseClick()
    {
        Time.timeScale = simulationSpeed - Time.timeScale;
    }

    private void UpdateSimulationSpeed()
    {
        simulationSpeedText.text = "Speed: " + simulationSpeed.ToString("#.00");
        Time.timeScale = simulationSpeed;
    }
}
