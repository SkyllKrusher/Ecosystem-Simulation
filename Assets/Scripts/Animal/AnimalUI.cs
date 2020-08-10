using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Image hungerSliderImage;
    [SerializeField] private Text activityStatus;

    public void SetHungerUI(HungerState hungerState, float hungerValue)
    {
        hungerBar.value = 1f - hungerValue / 100f;
        switch (hungerState)
        {
            case HungerState.FED:
                hungerSliderImage.color = Color.green;
                break;
            case HungerState.HUNGRY:
                hungerSliderImage.color = Color.yellow;
                break;
            case HungerState.STARVING:
                hungerSliderImage.color = Color.red;
                break;
            case HungerState.STARVED:
                hungerSliderImage.color = Color.black;
                break;

        }

    }

    public void SetActivityStatusText(string text)
    {
        activityStatus.text = text;
    }
}
