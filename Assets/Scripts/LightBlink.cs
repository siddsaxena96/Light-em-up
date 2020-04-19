using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{

    [SerializeField] private Light blinkingLight;
    private float increment = 0.2f;

    void Awake()
    {
        blinkingLight = GetComponent<Light>();
        blinkingLight.intensity = 0;
    }

    void Update()
    {
        blinkingLight.intensity += increment;
        if (blinkingLight.intensity >= 25)
            increment = -0.2f;
        if (blinkingLight.intensity <= 0)
            increment = 0.2f;
    }

    public void IncreaseRange()
    {
        blinkingLight.range = 8;
    }

    public void DecreaseRange()
    {
        blinkingLight.range = 3;
    }
}
